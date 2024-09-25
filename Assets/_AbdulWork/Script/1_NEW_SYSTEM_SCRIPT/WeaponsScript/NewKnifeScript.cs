using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewKnifeScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    // Update is called once per frame
    void Update()
    {
    }
    public void KnifeAttack()
    {
        animator.SetTrigger("Attack");
    }
}
