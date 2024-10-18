using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien4 : AlienClass
{
    private bool normal = false;
    protected override void Undetected()
    {
        transform.position += transform.forward * speed/3 * Time.deltaTime;
    }
    protected override void Detected()
    {
        if (Mathf.Abs((player.transform.position.x - this.transform.position.x)) > 2 && !normal)
        {
            if (player.transform.position.x > transform.position.x)
            {
                transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                animator.Play("DetectedRight");
            }
            else
            {
                animator.Play("DetectedLeft");
                transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime;
            }
        }
        else
        {
            normal = true;
            animator.Play("Detected");
            transform.position += transform.forward * speed * Time.deltaTime;
            // Get the direction to the target

        }
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;  // Optional: Ignore Y-axis rotation (keeps the object upright)

        // Check if the direction is not zero to avoid errors
        if (direction != Vector3.zero)
        {
            // Calculate the target rotation using LookRotation
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            // Smoothly rotate towards the target rotation using Slerp
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        }
    }
    /*protected override void Detected()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Alerted"))
        {
            if (Mathf.Abs((player.transform.position.x - this.transform.position.x)) > 2 && !normal)
            {
                if (player.transform.position.x > transform.position.x)
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("DetectedRight"))
                    {
                        animator.Play("DetectedRight");
                    }
                    transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                }
                else
                {
                    if (!animator.GetCurrentAnimatorStateInfo(0).IsName("DetectedLeft"))
                    {
                        animator.Play("DetectedLeft");
                    }
                    transform.position -= new Vector3(1, 0, 0) * speed * Time.deltaTime;
                }
            }
            else
            {
                normal = true;
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Detected"))
                {
                    animator.Play("Detected");
                }
                transform.position += transform.forward * speed * Time.deltaTime;
                // Get the direction to the target

            }
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0;  // Optional: Ignore Y-axis rotation (keeps the object upright)

            // Check if the direction is not zero to avoid errors
            if (direction != Vector3.zero)
            {
                // Calculate the target rotation using LookRotation
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate towards the target rotation using Slerp
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            };
        }
    }*/
}
