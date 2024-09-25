using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WeaponUpgradeMenu : MonoBehaviour
{
    //temp values these need to be changed
    private float maxDamage = 300f, maxRange = 300f, maxFireRate = 50f, maxMazgine = 100f;
    private int cost;
    private int maxLevel = 5;
    private Weapon weapon;
    [SerializeField] private CurrencyScript currencyScript;
    [SerializeField] private GameObject maxUpgraded, upgradeMenu, notEnoughMoney, upgradeButton;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponLevel, weaponName , upgradeCost;
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    [SerializeField] private TextMeshProUGUI[] specsName;
    [SerializeField] private TextMeshProUGUI[] specsValue;
    [SerializeField] private Slider[] specsSlider;
    [SerializeField] private WeaponMenuPanel weaponMenuPanel;
    public void LoadDetail(int index)
    {
        weapon = weaponJSONHandler.GetWeaponClass(index);
        cost = (index) * 50 + 200;
        cost += (int)((cost * 0.25f) * weapon.level);
        weaponImage.sprite = weapon.image;
        weaponName.text = weapon.name;
        weaponLevel.text = ("Level " + weapon.level).ToString();
        specsName[0].text = "Damage";
        specsValue[0].text = weapon.damage.ToString();
        specsSlider[0].value = weapon.damage / maxDamage;
        specsName[1].text = "Range";
        specsValue[1].text = weapon.range.ToString();
        specsSlider[1].value = weapon.range / maxRange;
        specsName[2].text = "FireRate";
        specsValue[2].text = weapon.fireRate.ToString();
        specsSlider[2].value = weapon.fireRate / maxFireRate;
        specsName[3].text = "Mazgine";
        specsValue[3].text = weapon.mazgine.ToString();
        specsSlider[3].value = weapon.mazgine / maxMazgine;
        upgradeCost.text = cost.ToString();
        if (weapon.level >= maxLevel)
        {
            upgradeMenu.SetActive(false);
            maxUpgraded.SetActive(true);
        }
        else
        {
            upgradeMenu.SetActive(true);
            maxUpgraded.SetActive(false);
            if (currencyScript.GetCoin() < cost)
            {
                upgradeButton.SetActive(false);
                notEnoughMoney.SetActive(true);
            }
            else
            {
                upgradeButton.SetActive(true);
                notEnoughMoney.SetActive(false);

            }
        }
    }
    public void UpgradePressed()
    {
        currencyScript.RemoveCoin(cost);
        weaponJSONHandler.UpgradeWeapon(weapon.index);
        weaponMenuPanel.ReloadUI();
        LoadDetail(weapon.index);
    }
}
