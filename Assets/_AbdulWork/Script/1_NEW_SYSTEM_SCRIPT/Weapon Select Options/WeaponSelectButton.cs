using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSelectButton : MonoBehaviour , IPointerClickHandler
{
    public event EventHandler clearButtonAnimations;
    private bool highlighted , selected = false;
    [SerializeField] private Animator anim;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private TextMeshProUGUI weaponType;
    [SerializeField] private TextMeshProUGUI weaponLevel;
    private WeaponSelectScript weaponSelectScript;
    private Weapon currentWeapon;
    public void OnPointerClick(PointerEventData ped)
    {
        if (!selected)
        {
            clearButtonAnimations?.Invoke(this, EventArgs.Empty);
            if (weaponSelectScript.HasSpace())
            {
                highlighted = true;
                anim.SetTrigger("Highlighted");
                weaponSelectScript.SelectButtonPressed();
            }
        }else if(!highlighted) 
        {
            selected = false;
            clearButtonAnimations?.Invoke(this, EventArgs.Empty);
            weaponSelectScript.RemoveSelectedWeapon(currentWeapon);
        }
    }
    public void ClearAnimation()
    {
        if(!selected)
        {
            highlighted = false;
            anim.SetTrigger("Normal");
        }
    }
    public void SetWeaponSelectAndWeapon(WeaponSelectScript script , Weapon weapon)
    {
        currentWeapon = weapon;
        weaponSelectScript = script;
        weaponName.text = weapon.name;
        weaponImage.sprite = weapon.image;
        weaponLevel.text = "LVL" + weapon.level;
    }
    public void SelectButton()
    {
        if(highlighted)
        {
            selected = true;
            highlighted = false;
            anim.SetTrigger("Pressed");
            weaponType.text = weaponSelectScript.SetWeapon(currentWeapon);
        }
    }
    public void ResetButton()
    {
        highlighted = false;
        selected = false;
    }
}
