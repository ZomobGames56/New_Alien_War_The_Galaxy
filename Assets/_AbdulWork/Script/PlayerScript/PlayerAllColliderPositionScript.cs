using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAllColliderPositionScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> allColiders = new List<GameObject>();
    [SerializeField] private List<Vector3> offset = new List<Vector3>();
    private void Start()
    {
        if(allColiders.Count != offset.Count)
        {
            Debug.LogError(" Colliders and their offset values are misMatch");
        }
    }
    void Update()
    {
        for(int i = 0; i < allColiders.Count; i++)
        {
            allColiders[i].transform.position = transform.position + offset[i];
        }
    }
}
