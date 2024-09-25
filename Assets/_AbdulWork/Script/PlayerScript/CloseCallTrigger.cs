using UnityEngine;

public class CloseCallTrigger : MonoBehaviour
{
    [SerializeField] private LevelTaskManager levelTaskManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            levelTaskManager.IncrementCloseCall();
        }
    }
}
