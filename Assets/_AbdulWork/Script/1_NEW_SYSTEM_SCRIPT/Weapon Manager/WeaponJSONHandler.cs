using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class WeaponJSONHandler : MonoBehaviour
{
    [Header("'Warning' - Attach the images in their index order and always")]
    [SerializeField] private List<Sprite> weaponImages = new List<Sprite>();
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    private int maxUpgrade = 5;
    private string weaponFilePath;
    private List<Weapon> loadedWeapons;
    void Awake()
    {
        weaponFilePath = Path.Combine(Application.persistentDataPath, "weapons.json");
        if (File.Exists(weaponFilePath))
        {
            //print("Weapons Game Data Loaded");
        }
        else
        {
            //print("No Data Found Loading predefine Data");
            //weapons = new List<Weapon>
            //{
            //    new Weapon(0, image , "Pistol", Weapon.WeaponCategory.Pistol , 10, 50, 1, 9, Weapon.WeaponType.Single, false, 1),
            //    new Weapon(1, image , "ShotGun", Weapon.WeaponCategory.Shotgun, 60, 30, 30, 6, Weapon.WeaponType.ShotGun, false, 30),
            //    new Weapon(2, image , "MachineGun", Weapon.WeaponCategory.MachineGun, 25, 80, 35, 30, Weapon.WeaponType.Automatic, false, 1),
            //    new Weapon(3, image , "Rifle", Weapon.WeaponCategory.Rifle, 40, 100, 20, 30, Weapon.WeaponType.Burst, false, 1),
            //};
            SaveWeaponsToJson(weapons);
        }
        loadedWeapons = LoadInfoFromJson(weaponFilePath);
        if(weaponImages.Count > 0 )
        {
            if(weapons.Count == weaponImages.Count)
            {
                for (int i = 0; i < loadedWeapons.Count; i++)
                {
                    loadedWeapons[i].image = weaponImages[i];
                }
                SaveWeaponsToJson(loadedWeapons);
            }
            else
            {
                Debug.LogError("The Sprite and weapon count mismatch.");
            }
        }
/*        // Log the loaded weapons to the console
        foreach (Weapon weapon in loadedWeapons)
        {
            print($"Name: {weapon.name}, Damage: {weapon.damage}, Range: {weapon.range}, Fire Rate: {weapon.fireRate}, Mazgine: {weapon.mazgine} , Type: {weapon.type} , isAvailable: {weapon.isAvailable}" );
        }*/
    }
    private void SaveWeaponsToJson(List<Weapon> weapons)
    {
        string json = JsonUtility.ToJson(new WeaponListWrapper(weapons), true);
        File.WriteAllText(weaponFilePath, json);
    }
    private List<Weapon> LoadInfoFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            WeaponListWrapper WeaponListWrapper = JsonUtility.FromJson<WeaponListWrapper>(json);
            return WeaponListWrapper.weapons;
        }
        else
        {
            return new List<Weapon>();
        }
    }  
/*    public void UnlockWeapon(int index)
    {
        List<Weapon> weapons = new List<Weapon>();
        foreach(Weapon weapon in loadedWeapons)
        {
            if(weapon.index == index)
            {
                Weapon newWeapon = weapon;
                newWeapon.isAvailable = true;
                weapons.Add(newWeapon);
            }
            else
            {
                weapons.Add(weapon);
            }
        }
        SaveWeaponsToJson(weapons);
        loadedWeapons = LoadInfoFromJson(weaponFilePath);
        foreach (Weapon weapon in loadedWeapons)
        {
            print($"Name: {weapon.name}, Damage: {weapon.damage}, Range: {weapon.range}, Fire Rate: {weapon.fireRate}, Mazgine: {weapon.mazgine} , Type: {weapon.type} , isAvailable: {weapon.isAvailable} , level : {weapon.level}");
        }
    }*/
    public void UnlockWeapon(int index)
    {
        List<Weapon> weapons = loadedWeapons;
        foreach (Weapon weapon in weapons)
        {
            if (weapon.index == index)
            {
                weapon.isAvailable = true;
            }
        }
        SaveWeaponsToJson(weapons);
        loadedWeapons = LoadInfoFromJson(weaponFilePath);
    }
    public Weapon GetWeaponClass(int index)
    {
        foreach (Weapon weapon in loadedWeapons)
        {
            if(weapon.index == index)
            {
                return weapon;
            }
        }
        return null;
    }
    public List<Weapon> GetAllWeaponList()
    {
        return loadedWeapons;
    }
    //Use only incase removing weaponSO did not work
    //public void SetWeaponsSO()
    //{
    //    int count;
    //    if (weapons.Count > weaponsSO.Count)
    //    {
    //        count = weapons.Count;
    //    }
    //    else
    //    {
    //        count = weaponsSO.Count;
    //    }
    //    for( int i = 0; i < count; i++ )
    //    {
    //        weaponsSO[i].WeaponName = weapons[i].name;
    //        weaponsSO[i].audioClip = weapons[i].audioClip;
    //        weaponsSO[i].WeaponUISprite = weapons[i].image;
    //        weaponsSO[i].Range = weapons[i].range;
    //        weaponsSO[i].FireRate = weapons[i].fireRate;
    //        weaponsSO[i].MaxBullet = weapons[i].mazgine;
    //        weaponsSO[i].Damage = weapons[i].damage;
    //        weaponsSO[i].Index = weapons[i].index;
    //        if (weapons[i].type == Weapon.WeaponType.Automatic)
    //        {
    //            weaponsSO[i].isAutomatic =true;
    //        }
    //        if(weapons[i].category == Weapon.WeaponCategory.Shotgun)
    //        {
    //            weaponsSO[i].isShotgun = true;
    //        }
    //    }
    //}
    public List<int> GetCategoryIndex(Weapon.WeaponCategory _category)
    {
        List<int> categoryindexs = new List<int>();
        foreach (Weapon weapon in loadedWeapons)
        {
            if(weapon.category == _category)
            {
                categoryindexs.Add(weapon.index);
            }
        }
        categoryindexs.Sort();
        return categoryindexs;
    }
    public void UpgradeWeapon(int index)
    {
        foreach (Weapon weapon in loadedWeapons)
        {
            if (weapon.index == index)
            {
                if (weapon.level < maxUpgrade)
                {
                    weapon.range = weapon.range + Mathf.CeilToInt(weapon.range *.1f);
                    weapon.damage = weapon.damage + Mathf.CeilToInt(weapon.damage *.1f);
                    weapon.fireRate = weapon.fireRate + Mathf.CeilToInt(weapon.fireRate *.1f);
                    weapon.mazgine = weapon.mazgine + Mathf.CeilToInt(weapon.mazgine * .1f);
                    weapon.level++;
                }
            }
        }
        SaveWeaponsToJson(loadedWeapons);
        loadedWeapons = LoadInfoFromJson(weaponFilePath);
    }
    //List Wrapper to Save and Load File Only
    private class WeaponListWrapper
    {
        public List<Weapon> weapons;

        public WeaponListWrapper(List<Weapon> weapons)
        {
            this.weapons = weapons;
        }
    }
}
