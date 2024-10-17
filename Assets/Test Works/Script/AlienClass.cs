using System;
using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public abstract class AlienClass : MonoBehaviour
{
    public event EventHandler PlayerKilled;
    public event EventHandler PlayerDying;
    protected enum AlienState
    {
        Undetected,
        DectectedPlayer,
        Grabbed,
        Killed,
        Stop
    }
    protected int health;
    protected Rigidbody rb;
    protected AlienState state;
    protected GameObject player;
    protected string AnimationName;
    protected new CapsuleCollider collider;
    [SerializeField] protected float speed = 5 , normalspeed = 5;
    [SerializeField] protected Animator animator;
    protected PlayerMovementScirpt playerMovementScript;
    [SerializeField] protected int initalHealth , HealthMultiplier;
    [SerializeField] protected NewAlienSpawnerScript newAlienSpawnerScript;
    [SerializeField] protected VisualEffect bulletHitEffect, meleeHitEffect, knifeHitEffect;


    //Test Code 
    public Renderer mat;
    private void Start()
    {
        speed = normalspeed;
        animator.Play("Undetected");
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        player = GameObject.FindGameObjectWithTag("Player");
       // playerMovementScript = player.GetComponent<PlayerMovementScirpt>();
        Reset();
    }
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        Reset();
        state = AlienState.DectectedPlayer;
    }
    protected void Update()
    {
        switch(state)
        {
            case AlienState.Undetected:
                {
                    Undetected();
                    break;
                }
            case AlienState.DectectedPlayer:
                {
                    Detected();
                    break;
                }
            case AlienState.Grabbed:
                {
                    break;
                }
            case AlienState.Killed:
                {
                    break;
                }
            case AlienState.Stop:
                {
                    break;
                }
        }
    }
    public void ReduceSpeedAlien()
    {
        animator.Play("Idle");
        speed = 0;
    }
    public void StartAlien()
    {
        animator.Play("Detected");
        speed = normalspeed;
    }
    protected void Reset()
    {
        speed = normalspeed;
        rb.useGravity = true;
        collider.enabled = true;
        state = AlienState.Undetected;
        health = (int)(initalHealth * HealthMultiplier);
    }
    public void Grabbed()
    {
        AnimationName = "Grabbed";
        state = AlienState.Grabbed;
        animator.Play(AnimationName);
        StartCoroutine(AnimationFunction(Bitting, AnimationName));
    }
    private void Bitting()
    {
        AnimationName = "Bite";
        state = AlienState.Grabbed;
        animator.Play(AnimationName);
        PlayerDying?.Invoke(this , EventArgs.Empty);
        StartCoroutine(AnimationFunction(Killed, AnimationName));
    }
    private void Killed()
    {
        print("Kill Player");
        PlayerKilled?.Invoke(this, EventArgs.Empty);
        BackToList();
    }
    public void KnifeHit()
    {
        knifeHitEffect.Play();
    }
    public void KnifeKilled()
    {
        rb.useGravity = false;
        collider.enabled = false;
        AnimationName = "KnifeHit";
        animator.Play(AnimationName);
        StartCoroutine(AnimationFunction(BackToList, AnimationName));
    }
    private void BackToList()
    {
        newAlienSpawnerScript.BackToList(this.gameObject);
    }
    protected abstract void Detected();
    protected abstract void Undetected();
    public void BulletHit( int damage)
    {
        health -= damage;
        bulletHitEffect.Play();
        if(health>0)
        {
            AnimationName = "BulletHit";
            animator.Play(AnimationName);
            state = AlienState.DectectedPlayer;
        }
        else
        {
            rb.useGravity = false;
            collider.enabled = false;
            AnimationName = "BulletDeath";
            animator.Play(AnimationName);
            state = AlienState.Killed;
            StartCoroutine(AnimationFunction(BackToList, AnimationName));
        }
    }
    public void MeleeHit(int direction)
    {
        switch(direction)
        {
            case 0:
                {
                    AnimationName = "MeleeLeft";
                    break;
                }
            case 1:
                {
                    AnimationName = "MeleeRight";
                    break;
                }
        }
        meleeHitEffect.Play();
        rb.useGravity = false;
        collider.enabled = false;
        state = AlienState.Grabbed;
        animator.Play(AnimationName);
        StartCoroutine(AnimationFunction(BackToList, AnimationName));
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DetectPlayer")
        {
            AnimationName = "Alerted";
            animator.Play(AnimationName);
            state = AlienState.DectectedPlayer;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if(transform.position.z < other.transform.position.z)
            {
                other.GetComponent<AlienClass>().ReduceSpeedAlien();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
                other.GetComponent<AlienClass>().StartAlien();
        }
    }
    IEnumerator AnimationFunction(Action action , string animName)
    {
        do
        {
            yield return null;
        } while (animator.GetCurrentAnimatorStateInfo(0).IsName(animName));
        action();
        StopCoroutine("AnimationFunction");
    }
    public void SetAlienSpawner(NewAlienSpawnerScript spawnerScript)
    {
        newAlienSpawnerScript = spawnerScript;
    }
}
