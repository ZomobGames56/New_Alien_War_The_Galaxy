using System;
using UnityEngine;

public class NewPistolScript : NewWeaponScript
{
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
                script.BulletHit(damage);
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
            //playerAnimator.SetBool("Shooting" , false);
        }
        bulletFired = true;
    }
    public override void TriggerReleased()
    {
        bulletFired= false;
    }
}
