using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] private Transform player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Vector3 dir = player.transform.position - other.transform.position;
            other.transform.position = player.transform.position - dir.normalized;
            other.GetComponent<AlienClass>().Grabbed();
            PlayerMovementScirpt playerMovementScript = player.GetComponent<PlayerMovementScirpt>();
            playerMovementScript.FreeToGrabbed(other.gameObject);
        }
    }
}
