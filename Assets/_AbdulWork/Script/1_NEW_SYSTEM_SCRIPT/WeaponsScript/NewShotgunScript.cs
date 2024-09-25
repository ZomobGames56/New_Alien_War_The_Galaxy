using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NewShotGunScript : NewWeaponScript
{
    private int killsPerBullet;
    private bool bulletFired = false;
    public override void Shoot()
    {
        if (!bulletFired)
        {
            killsPerBullet = 0;
            //m_AudioSource.clip = weapon_SO.audioClip;
            //m_AudioSource.Play();
            //muzzleFlash.Play();
            //animator.SetBool("Shooting", true);
            animator.Play("Fire", index);
            muzzleFlash.Play();
            currentBullet--;
            totalBullet--;
            if (playerTargetScript.GetAllTargetObjects() != null)
            {
                List<Collider> targetColliders = playerTargetScript.GetAllTargetObjects();
                foreach (Collider collider in targetColliders)
                {
                    killsPerBullet++;
                    AlienClass script = collider.gameObject.GetComponentInParent<AlienClass>();
                    levelTaskManager.ComboIncrement();
                    script.BulletHit(weapon.damage);
                    //script.SetState(AlienScript.AlienState.Dead);
                    //script.ShowBloodEffect();
                    levelUIManager.IncrementKill();
                    levelTaskManager.weaponKillthorughPistol();
                }
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
            if(killsPerBullet >=3)
            {
                levelTaskManager.MultiKill();
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
        bulletFired = false;
    }
}
