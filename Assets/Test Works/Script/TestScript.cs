using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Renderer mat;
    private float speed = 5, normalSpeed = 5;
    public enum State
    {
        Move,
        Stay
    }
    public State state;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetState(State _state)
    {
        state = _state;
    }
    // Update is called once per frame
    void Update()
    {
        if(state == State.Move)
        {
            mat.material.color = Color.green;
            transform.Translate(new Vector3(0, 0, - speed * Time.deltaTime));
        }
        else if (state == State.Stay)
        {
            mat.material.color = Color.red;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if(other.tag == "Enemy")
        {
            if (transform.position.z < other.transform.position.z)
            {
                other.GetComponent<TestScript>().SetState(State.Stay);
            }
        }

    }
    private void OnTriggerStay(Collider other)
    {
        print(other.name);
        if (other.tag == "Enemy")
        {
            if (transform.position.z < other.transform.position.z)
            {
                other.GetComponent<TestScript>().SetState(State.Stay);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<TestScript>().SetState(State.Move);
        }
    }
}
