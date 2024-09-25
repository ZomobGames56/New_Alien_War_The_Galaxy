using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.VFX;
public abstract class NewWeaponScript : MonoBehaviour
{
    public event EventHandler updateUI;
    public event EventHandler DropWeaponEvent;

    private bool firstFrame;
    private float timeDelayed;
    protected Weapon weapon;
    protected LevelUIManager levelUIManager;
    protected GameObject player;
    [SerializeField] protected PlayerWeaponScript playerWeaponScript;
    [SerializeField] protected int index;
    [SerializeField] protected Animator animator;
    [SerializeField] protected VisualEffect muzzleFlash;
    [SerializeField] protected LevelTaskManager levelTaskManager;
    [SerializeField] protected PlayerTargetScript playerTargetScript; 
    [SerializeField] protected int currentBullet, maxBullet, totalBullet , damage; 
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeaponScript = player.GetComponent<PlayerWeaponScript>();
        levelUIManager = FindObjectOfType<LevelUIManager>();
        muzzleFlash = GetComponentInChildren<VisualEffect>();
    }
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeaponScript = player.GetComponent<PlayerWeaponScript>();
        playerWeaponScript.shootEvent += PlayerWeaponScript_shootEvent;
        playerWeaponScript.fireButtonReleased += PlayerWeaponScript_fireButtonReleased;
        if(currentBullet == 0 && totalBullet != 0)
        {
            animator.Play("Out", index);
            firstFrame = true;
            StartCoroutine(StateAnimationDone("Out", 1));
        }
    }
    protected IEnumerator StateAnimationDone(string animationName , int stateIndex)
    {
        while(animator.GetCurrentAnimatorStateInfo(index).IsName(animationName) || firstFrame)
        {
            firstFrame = false;
            yield return null;
        }
        switch(stateIndex)
        {
            case 0:
                {
                    ReloadCompleted();
                    break;
                }
            case 1:
                {
                    WeaponReload();
                    break;
                }
        }
        StopCoroutine("StateAnimationDone");
    }
    private void OnDisable()
    {
        playerWeaponScript.shootEvent -= PlayerWeaponScript_shootEvent;
        playerWeaponScript.fireButtonReleased -= PlayerWeaponScript_fireButtonReleased;
    }
    private void PlayerWeaponScript_fireButtonReleased(object sender, System.EventArgs e)
    {
        TriggerReleased();
    }
    private void PlayerWeaponScript_shootEvent(object sender, System.EventArgs e)
    {
        if (currentBullet != 0 && animator.GetCurrentAnimatorStateInfo(index).IsName("Run"))
        {
            Shoot();
        }
    }
    public void SetTarget(PlayerTargetScript target)
    {
        playerTargetScript = target;
    }
    public virtual void TriggerReleased()
    {

    }
    public abstract void Shoot();
    public void AddBullets(int bullets)
    {
        totalBullet += bullets;
        updateUI?.Invoke(this.gameObject , EventArgs.Empty);
    }
    public int GetCurrentBullet()
    {
        return currentBullet;
    }
    public int GetMaxBullet()
    {
        return maxBullet;
    }
    public int GetTotalBullet()
    {
        return totalBullet;
    }
    protected void WeaponDrop()
    {
        DropWeaponEvent?.Invoke(this, EventArgs.Empty);
    }
    protected void ReloadCompleted()
    {
        if (animator.GetCurrentAnimatorStateInfo(index).IsName("Run"))
        {
            if (totalBullet > maxBullet)
            {
                currentBullet = maxBullet;
            }
            else
            {
                currentBullet = totalBullet;
            }
            updateUI?.Invoke(this, EventArgs.Empty);
        }

    }
    protected void WeaponReload()
    {
        animator.Play("Reload", index);
        firstFrame = true;
        StartCoroutine(StateAnimationDone("Reload", 0));
    }
    public void SetWeapon(Weapon pickweapon)
    {
        weapon = pickweapon;
        damage = weapon.damage;
        maxBullet = weapon.mazgine;
        totalBullet = maxBullet / 2;
        currentBullet = totalBullet;
        index = weapon.index;
    }
    protected void UpdateUI()
    {
        updateUI?.Invoke(this.gameObject, EventArgs.Empty);
    }
}
