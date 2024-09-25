using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien2 : AlienClass
{
    protected override void Undetected()
    {
        transform.position += transform.forward * speed/3 * Time.deltaTime;
    }
    protected override void Detected()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Alerted"))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            // Get the direction to the target
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
    }
}
