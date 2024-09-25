using System;
using System.Collections;
using UnityEngine;

public class NewMeleeWeaponScript : MonoBehaviour
{
    public event EventHandler DropMelee;

    private bool hitting , released , firstFrame;
    private int currentValue = 10, index;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private PlayerWeaponScript playerWeaponScript;
    private void Start()
    {
        released = true;
        currentValue = 10;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;

    }
    private void OnEnable()
    {
        currentValue = 10;
        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        index = playerWeaponScript.GetMeleeWeaponIndex();
        playerWeaponScript.shootEvent += PlayerWeaponScript_shootEvent;
        playerWeaponScript.fireButtonReleased += PlayerWeaponScript_fireButtonReleased;
    }
    private void OnDisable()
    {
        playerWeaponScript.fireButtonReleased -= PlayerWeaponScript_fireButtonReleased;
        playerWeaponScript.shootEvent -= PlayerWeaponScript_shootEvent;
    }
    private void PlayerWeaponScript_fireButtonReleased(object sender, EventArgs e)
    {
        m_Animator.SetBool("Attack", false);
        released = true;
    }
    private void PlayerWeaponScript_shootEvent(object sender, System.EventArgs e)
    {
        Shoot();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && hitting && !m_Animator.GetCurrentAnimatorStateInfo(index).IsName("Away"))
        {
            if(m_Animator.GetCurrentAnimatorStateInfo(index).IsName("Attack1"))
            {
                other.GetComponent<AlienClass>().MeleeHit(0); // 0 for left and 1 for right
            }
            else
            {
                other.GetComponent<AlienClass>().MeleeHit(1);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && hitting && !m_Animator.GetCurrentAnimatorStateInfo(index).IsName("Away"))
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(index).IsName("Attack1"))
            {
                other.GetComponent<AlienClass>().MeleeHit(0); // 0 for left and 1 for right
            }
            else
            {
                other.GetComponent<AlienClass>().MeleeHit(1);
            }
        }
    }
    private void Shoot()
    {
        hitting = true;
        if(hitting && released && m_Animator.GetCurrentAnimatorStateInfo(index).IsName("Run"))
        {
            released = false;
            firstFrame = true;
            currentValue--;
            m_Animator.SetBool("Attack", true);
            m_Animator.Play("Attack1" , index);
            StartCoroutine(AnimationRunner("Attack1"));
        }
    }
    IEnumerator AnimationRunner(string animName)
    {
        while (firstFrame||m_Animator.GetCurrentAnimatorStateInfo(index).IsName(animName))
        {
            firstFrame = false;
            yield return null;
        }

        if (!released && currentValue > 0)
        {
            //print(currentValue);
            currentValue--;
            if (animName == "Attack1")
            {
                firstFrame = true;
                m_Animator.Play("Attack2", index);
                StartCoroutine(AnimationRunner("Attack2"));
            }
            else
            {
                firstFrame = true;
                m_Animator.Play("Attack1", index);
                StartCoroutine(AnimationRunner("Attack1"));
            }
        }
        else if (currentValue == 0)
        {
            currentValue--;
            if (animName == "Attack1")
            {
                firstFrame = true;
                StartCoroutine(AnimationRunner("Attack1Settle"));
            }
            else
            {
                firstFrame = true;
                StartCoroutine(AnimationRunner("Attack2Settle"));
            }

        }
        else if (currentValue < 0)
        {

            hitting = false;
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            DropMelee?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            hitting = false;
            StopCoroutine(AnimationRunner(animName));
        }
    }
}
