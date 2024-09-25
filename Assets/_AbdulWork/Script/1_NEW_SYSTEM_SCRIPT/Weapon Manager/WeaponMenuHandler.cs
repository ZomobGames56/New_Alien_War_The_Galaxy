using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponMenuHandler : MonoBehaviour
{
    //Temp values for slider for games
    private float maxDamage = 300f, maxRange = 300f, maxFireRate = 50f, maxMazgine = 100f; 
    private int maxLevel = 5;
    [SerializeField] private Text unlockLevelText;
    [SerializeField] private WeaponUpgradeMenu weaponUpgradeMenu;
    [SerializeField] private GameObject Lock, Upgrade , maxUpgraded;
    [SerializeField] private WeaponMenuPanel weaponMenuPanel;
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponName, weaponLevel;
    [SerializeField] private TextMeshProUGUI[] weaponSpecTexts;
    [SerializeField] private TextMeshProUGUI[] weaponValueTexts;
    [SerializeField] private Slider[] slider;
    //[SerializeField] private Slider[] weaponSlider;

    private Weapon weapon;
    public void SetWeapon(Weapon _weapon)
    {
        weapon = _weapon;
        SetUI();
    }
    private void SetUI()
    {
        unlockLevelText.text = ("Unlock at Level " + (weapon.index+1)).ToString();
        weaponImage.sprite = weapon.image;
        weaponName.text = weapon.name;
        weaponLevel.text = ("Level " + weapon.level).ToString();
        weaponSpecTexts[0].text = "Damage";
        weaponValueTexts[0].text = weapon.damage.ToString();
        slider[0].value = weapon.damage / maxDamage;
        weaponSpecTexts[1].text = "Range";
        weaponValueTexts[1].text = weapon.range.ToString();
        slider[1].value = weapon.range / maxRange;
        weaponSpecTexts[2].text = "FireRate";
        weaponValueTexts[2].text = weapon.fireRate.ToString();
        slider[2].value = weapon.fireRate / maxFireRate;
        weaponSpecTexts[3].text = "Mazgine";
        weaponValueTexts[3].text = weapon.mazgine.ToString();
        slider[3].value = weapon.mazgine / maxMazgine;
        if (weapon.isAvailable)
        {
            if (weapon.level >= maxLevel)
            {
                SwitchButtons(maxUpgraded);
            }
            else
            {
                SwitchButtons(Upgrade);
            }
        }
        else
        {
            SwitchButtons(Lock);
        }

    }
    private void SwitchButtons(GameObject obj)
    {
        Upgrade.SetActive(false);
        Lock.SetActive(false);
        maxUpgraded.SetActive(false);

        obj.SetActive(true);
    }
    public void UpgradeButton()
    {
        weaponUpgradeMenu.LoadDetail(weapon.index);
    }
/*    public void UnlockWeapon()
    {
        weaponJSONHandler.UnlockWeapon(weapon.index);
        weaponMenuPanel.ReloadUI();
    }*/
}
