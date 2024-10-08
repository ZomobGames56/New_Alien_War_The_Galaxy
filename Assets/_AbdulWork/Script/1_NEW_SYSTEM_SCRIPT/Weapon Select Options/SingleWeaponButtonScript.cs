using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SingleWeaponButtonScript : MonoBehaviour , IPointerDownHandler
{
    public event EventHandler clearHorizontalEvent;

    private WeaponMenuHandler weaponMenuHandler;
    private Weapon thisWeapon;
    [SerializeField] private Animator anim;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private GameObject weaponLock;
    private bool highLighted;
    public void OnEnable()
    {
        if(highLighted)
        {
            anim.SetTrigger("Pressed");
        }
    }
    public void SetWeaponMenu(WeaponMenuHandler weaponMenu)
    {
        weaponMenuHandler = weaponMenu;
    }
    public void OnPointerDown(PointerEventData ped)
    {
        if(!highLighted)
        {
            clearHorizontalEvent?.Invoke(this, EventArgs.Empty);
            anim.SetTrigger("Pressed");
            highLighted = true;
            weaponMenuHandler.SetMainWeapon(thisWeapon);
        }

    }
    public void ButtonPressed()
    {
        if (!highLighted)
        {
            clearHorizontalEvent?.Invoke(this, EventArgs.Empty);
            anim.SetTrigger("Pressed");
            highLighted = true;
            weaponMenuHandler.SetMainWeapon(thisWeapon);
        }
    }
    public void highlightButton()
    {
        anim.SetTrigger("Pressed");
    }
    public void clearAnimations()
    {
        highLighted = false;
        anim.SetTrigger("Normal");
    }
    public void SetWeapon(Weapon weapon)
    {
        if(highLighted)
        {
            weaponMenuHandler.SetMainWeapon(thisWeapon);
        }
        thisWeapon = weapon;
        weaponImage.sprite = weapon.image;
        weaponName.text = weapon.name;
        if(weapon.isUnlocked)
        {
            weaponLock.SetActive(false);
        }
        else
        {
            weaponLock.SetActive(true);
        }
    }
}
