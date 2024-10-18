using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
public class WeaponSelectScript : MonoBehaviour
{
    private int weapon1Index = -1, weapon2Index = -1;
    private bool weapon1Available, weapon2Available;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private GameObject playButton, selectButton;
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    [SerializeField] private WeaponPanel mainWeaponPanel, backWeaponPanel;
    [SerializeField] private GameObject weaponButtonPrefab , weaponHolderPrefab;
    [SerializeField] private List<WeaponSelectButton> weaponSelectButtons = new List<WeaponSelectButton>();
    [SerializeField] private List<Weapon> allWeapons = new List<Weapon>(), availableWeapons = new List<Weapon>();
    public void Start()
    {
        readALLWeapons();
        SwitchButton(selectButton);
        foreach (Weapon weapon in allWeapons) 
        {
            if(weapon.isAvailable && weapon.isUnlocked)
            {
                availableWeapons.Add(weapon);
            }
        }
        for(int i = 0; i<availableWeapons.Count; i++)
        {
            GameObject temp = Instantiate(weaponButtonPrefab, weaponHolderPrefab.transform);
            weaponSelectButtons.Add(temp.GetComponent<WeaponSelectButton>());
        }
        for(int i = 0; i < availableWeapons.Count; i++)
        {
            weaponSelectButtons[i].SetWeaponSelectAndWeapon(this , availableWeapons[i]);
            weaponSelectButtons[i].clearButtonAnimations += WeaponSelectScript_clearButtonAnimations;
        }
        Vector2 sizeDelta = new Vector2(availableWeapons.Count * 500, weaponHolderPrefab.GetComponent<RectTransform>().sizeDelta.y);
        weaponHolderPrefab.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    private void WeaponSelectScript_clearButtonAnimations(object sender, System.EventArgs e)
    {
        foreach(WeaponSelectButton weaponButton in weaponSelectButtons)
        {
            weaponButton.ClearAnimation();
        }
    }

    public bool HasSpace()
    {
        if(weapon1Index == -1)
        {
            return true;
        }
        else if(weapon2Index == -1)
        {
            return true;
        }
        return false;
    }
    private void readALLWeapons()
    {
        allWeapons = weaponJSONHandler.GetAllWeaponList();
    }
    public void SelectButtonPressed()
    {
        foreach(WeaponSelectButton weaponButton in weaponSelectButtons)
        {
            weaponButton.SelectButton();
        }
    }
    private void SwitchButton(GameObject button)
    {
        playButton.SetActive(false);
        selectButton.SetActive(false);

        button.SetActive(true);
    }
    public string SetWeapon(Weapon weapon)
    {
        if(weapon1Index == -1)
        {
            weapon1Index = weapon.index;
            mainWeaponPanel.SelectWeapon(weapon);
            return "MAIN";
        }
        else if(weapon2Index == -1)
        {
            weapon2Index = weapon.index;
            backWeaponPanel.SelectWeapon(weapon);
            SwitchButton(playButton);
            return "BACK UP";
        }
        return null;
    }
    public void RemoveSelectedWeapon(Weapon weapon)
    {
        if(weapon1Index == weapon.index)
        {
            weapon1Index = -1;
            mainWeaponPanel.SelectWeapon(null);
        }
        else if(weapon2Index == weapon.index)
        {
            weapon2Index = -1;
            backWeaponPanel.SelectWeapon(null);
        }
        SwitchButton(selectButton);
    }
    public void OnDisable()
    {
        scrollbar.value = 0;
        weapon1Index = -1;
        weapon2Index = -1;
        mainWeaponPanel.SelectWeapon(null);
        backWeaponPanel.SelectWeapon(null);
        SwitchButton(selectButton);
        foreach(WeaponSelectButton weaponButtonScript in weaponSelectButtons)
        {
            weaponButtonScript.ResetButton();
        }
    }
/*    public void RemoveLastWeapon()
    {
        if(weapon2Available)
        {
            weapon2Index = -1;
            backWeaponPanel.SelectWeapon(null);
        }
        else
        {
            weapon1Index = -1;
            mainWeaponPanel.SelectWeapon(null);
        }
    }*/
    public void GameStart()
    {
        PlayerPrefs.SetInt("Weapon1Index", weapon1Index);
        PlayerPrefs.SetInt("Weapon2Index", weapon2Index);
    }
}
