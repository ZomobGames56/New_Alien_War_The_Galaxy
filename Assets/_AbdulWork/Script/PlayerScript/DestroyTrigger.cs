using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [SerializeField] private NewAlienSpawnerScript alienSpawnerScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            alienSpawnerScript.BackToList(other.gameObject);
        }
    }
}
