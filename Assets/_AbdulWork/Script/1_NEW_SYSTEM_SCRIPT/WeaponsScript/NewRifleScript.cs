using System;
using UnityEngine;

public class NewRifleScript : NewWeaponScript
{
    private void OnDisable()
    {
        print("Weapon Disable");
    }
    //Bool used to escape the first Frame of animator
    private bool bulletFired = false;
    public override void Shoot()
    {
        if (!bulletFired)
        {
        //m_AudioSource.clip = weapon_SO.audioClip;
        //m_AudioSource.Play();
        //muzzleFlash.Play();
        //animator.SetBool("Shooting", true);
        animator.Play("Fire", index);
        muzzleFlash.Play();
        currentBullet--;
        totalBullet--;
        if (playerTargetScript.GetTargetObject() != null)
        {
            levelTaskManager.ComboIncrement();
            AlienClass script = playerTargetScript.GetTargetObject().GetComponentInParent<AlienClass>();
            script.BulletHit(weapon.damage);
            levelUIManager.IncrementKill();
            levelTaskManager.weaponKillthorughPistol();
        }
        else
        {
            levelTaskManager.ResetCombo();
        }
        //LevelTaskManager.BulletFired();
        UpdateUI();
        if (totalBullet == 0)
        {
            WeaponDrop();
        }
        else if (currentBullet == 0)
        {
            WeaponReload();
        }
        }
        else
        {
            bulletFired= false;
            //playerAnimator.SetBool("Shooting" , false);
        }
    }
    public override void TriggerReleased()
    {
        bulletFired= false;
    }
}
