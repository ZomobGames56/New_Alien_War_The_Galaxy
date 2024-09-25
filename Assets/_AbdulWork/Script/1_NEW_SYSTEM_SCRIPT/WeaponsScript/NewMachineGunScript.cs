using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewMachineGunScript : NewWeaponScript
{
    private float Timer;
    private bool shooting , bulletFired;

    public override void Shoot()
    {
        if(!bulletFired)
        {
            bulletFired = !bulletFired;
            Automatic();
        }
        else
        {
            bulletFired = !bulletFired;
        }

    }
    private void Automatic()
    {
        muzzleFlash.Play();
        //LevelTaskManager.BulletFired();
        animator.Play("Fire", index);
        if (playerTargetScript.GetTargetObject() != null)
        {
            levelTaskManager.ComboIncrement();
            AlienClass script = playerTargetScript.GetTargetObject().GetComponentInParent<AlienClass>();
            script.BulletHit(damage);
            //script.ShowBloodEffect();
            levelUIManager.IncrementKill();
            levelTaskManager.WeaponKillThorughMachineGun();
        }
        else
        {
            levelTaskManager.ResetCombo();
        }
        currentBullet--;
        totalBullet--;
        if(totalBullet ==0 )
        {
            WeaponDrop();
        }else if(currentBullet == 0 )
        {
            WeaponReload();
        }
        UpdateUI();
    }
    public override void TriggerReleased()
    {
        //playerAnimator.SetBool("Shooting", false);
    }
}
