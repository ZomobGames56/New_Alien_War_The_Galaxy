using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMenuPanel : MonoBehaviour
{
    private Weapon.WeaponCategory category;
    private Weapon weapon;
    private List<int> weaponIndexs = new List<int>();
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    [SerializeField] private List<WeaponMenuHandler> weaponMenuHandler;
    private void Start()
    {
        SetWeapon(Weapon.WeaponCategory.Pistol);
    }
    public void SetWeapon(Weapon.WeaponCategory _category)
    {
        weaponIndexs = weaponJSONHandler.GetCategoryIndex(_category);
        int loopIndex = weaponMenuHandler.Count;
        if (weaponIndexs.Count <= weaponMenuHandler.Count)
        {
            loopIndex = weaponIndexs.Count;
        }
        for (int i = 0; i < loopIndex; i++)
        {
            weapon = weaponJSONHandler.GetWeaponClass(weaponIndexs[i]);
            Debug.LogError("Set Weapon need to be fixed");
            //weaponMenuHandler[i].SetWeapon(weapon);
        }
        category = _category;
    }
    public void ChangeCategory(int i)
    {
        switch(i)
        {
            case 1:
                {
                    category = Weapon.WeaponCategory.Pistol;
                    break;
                }
            case 2:
                {
                    category = Weapon.WeaponCategory.Shotgun;
                    break;
                }
            case 3:
                {
                    category = Weapon.WeaponCategory.MachineGun;
                    break;
                }
            case 4: {
                    category = Weapon.WeaponCategory.Rifle;
                    break;
                }
        }
        SetWeapon(category);
    }
    public void ReloadUI()
    {
        SetWeapon(category);
    }
}

