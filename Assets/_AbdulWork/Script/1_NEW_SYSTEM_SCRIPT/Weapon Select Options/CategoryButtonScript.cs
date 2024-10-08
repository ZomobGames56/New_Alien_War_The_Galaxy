using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CategoryButtonScript : MonoBehaviour , IPointerDownHandler, IPointerUpHandler
{
    public event EventHandler clearVerticalEvent;
    [SerializeField] private Animator anim;
    [SerializeField] private Weapon.WeaponCategory category;
    [SerializeField] private WeaponMenuHandler weaponMenuHandler;
    private bool highlighted;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!highlighted)
        {
            clearVerticalEvent?.Invoke(this, EventArgs.Empty);
            anim.SetTrigger("Pressed");
            weaponMenuHandler.switchCategory(category);
            highlighted = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {

    }
    public void highlightButton()
    {
        if (!highlighted)
        {
            clearVerticalEvent?.Invoke(this, EventArgs.Empty);
            anim.SetTrigger("Pressed");
            weaponMenuHandler.switchCategory(category);
            highlighted = true;
        }
    }
    public void clearAnimations()
    {
        highlighted = false;
        anim.SetTrigger("Normal");
    }
}
