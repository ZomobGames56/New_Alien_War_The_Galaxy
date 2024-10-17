using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] private Transform player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.GetComponent<AlienClass>().Grabbed();
            PlayerMovementScirpt playerMovementScript = player.GetComponent<PlayerMovementScirpt>();
            playerMovementScript.FreeToGrabbed(other.gameObject);
        }
    }
}
