using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerTargetScript : MonoBehaviour
{
    //// Old Method Collider trigger
    //// List to store all objects currently triggering the collider
    //private List<GameObject> triggeringObjects = new List<GameObject>();

    ////void OnTriggerEnter(Collider other)
    ////{
    ////    if (other.gameObject.tag == "Enemy")
    ////    {
    ////        // Add the object to the list if it's not already there
    ////        if (!triggeringObjects.Contains(other.gameObject))
    ////        {
    ////            triggeringObjects.Add(other.gameObject);
    ////        }
    ////    }
    ////}

    ////void OnTriggerExit(Collider other)
    ////{
    ////    if (other.gameObject.tag == "Enemy")
    ////    {
    ////        // Remove the object from the list
    ////        if (triggeringObjects.Contains(other.gameObject))
    ////        {
    ////            triggeringObjects.Remove(other.gameObject);
    ////        }
    ////    }
    ////}
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.tag == "Enemy")
    //    {
    //        if (!triggeringObjects.Contains(other.gameObject))
    //        {
    //            triggeringObjects.Add(other.gameObject);
    //        }
    //    }
    //    Debug.Log(other.gameObject.name + " " + other.gameObject.tag);
    //}
    //public GameObject GetTargetObject()
    //{

    //    // Return the first object in the list (or any other logic to select the object)
    //    if (triggeringObjects.Count > 0)
    //    {
    //        GameObject temp;
    //        for (int i = triggeringObjects.Count - 1; i > 0; i--)
    //        {
    //            temp = triggeringObjects[i];
    //            if (triggeringObjects[i].transform.position.z<triggeringObjects[i-1].transform.position.z)
    //            {
    //                triggeringObjects[i] = triggeringObjects[i-1];
    //                triggeringObjects[i-1] = temp;
    //            }
    //        }
    //        temp = triggeringObjects[0];
    //        triggeringObjects.Clear();
    //        return temp;
    //    }
    //    return null;
    //}


    ////New Method Capsule Cast
    // Capsule parameters
    private Vector3 point1; // The starting point of the capsule
    private Vector3 point2; // The ending point of the capsule
    public float radius = 1f; // The radius of the capsule
    private float maxDistance = 30f; // The maximum distance for the cast
    [SerializeField] private LayerMask collisionMask; // The layers to detect collisions
    private GameObject TargetObject;
    void OnDrawGizmos()
    {
        // Draw the capsule in the scene view for visualization
        if (point1 != null && point2 != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(point1, radius);
            Gizmos.DrawWireSphere(point2, radius);
            Gizmos.DrawLine(point1 + Vector3.up * radius, point2 + Vector3.up * radius);
            Gizmos.DrawLine(point1 - Vector3.up * radius, point2 - Vector3.up * radius);
            Gizmos.DrawLine(point1 + Vector3.right * radius, point2 + Vector3.right * radius);
            Gizmos.DrawLine(point1 - Vector3.right * radius, point2 - Vector3.right * radius);
        }
    }
    public GameObject GetTargetObject()
    {
        point1 = transform.position;
        point2 = transform.position + transform.forward * 0.1f;
        // Define the direction of the cast
        Vector3 direction = transform.forward;

        // Perform the capsule cast
        if (Physics.CapsuleCast(point1, point2, radius, direction, out RaycastHit hitInfo, maxDistance, collisionMask))
        {
            TargetObject = hitInfo.collider.gameObject;
        }
        else
        {
            TargetObject = null;
        }
        return TargetObject;
    }
    public List<Collider> GetAllTargetObjects()
    {
        point1 = transform.position;
        point2 = transform.position + transform.forward * maxDistance;
        Collider[] hitColliders = Physics.OverlapCapsule(point1, point2, radius, collisionMask);
        List<Collider> targetObjects = new List<Collider>();
        foreach (Collider collider in hitColliders)
        {
            if(collider.GetComponent<AlienClass>() != null)
            {
                targetObjects.Add(collider);
            }
        }
        return targetObjects;
    }
    public void SetWeaponRange(int Range)
    {
        maxDistance = Range * 10;
    }
}
