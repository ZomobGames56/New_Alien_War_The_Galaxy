using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WeaponPanel : MonoBehaviour
{
    [SerializeField] private bool isAvailable;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponName, weaponLevel;
    [SerializeField] private GameObject lockPanel, emptyPanel, weaponPanel;
    private void Start()
    {
        SelectWeapon(null);
    }
    public void SelectWeapon(Weapon weapon)
    {
        if(isAvailable)
        {
            if (weapon == null)
            {
                SwitchPanel(emptyPanel);
            }
            else
            {
                SetPanel(weapon);
                SwitchPanel(weaponPanel);
            }
        }
        else
        {
            SwitchPanel(lockPanel);
        }
    }
    private void SetPanel(Weapon weapon)
    {
        weaponImage.sprite = weapon.image;
        weaponName.text = weapon.name;
        weaponLevel.text = "LVL " + weapon.level;
    }
    private void SwitchPanel(GameObject obj)
    {
        lockPanel.SetActive(false);
        emptyPanel.SetActive(false);
        weaponPanel.SetActive(false);

        obj.SetActive(true);
    }
}
