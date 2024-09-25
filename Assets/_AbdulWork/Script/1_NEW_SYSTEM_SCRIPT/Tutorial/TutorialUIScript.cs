using UnityEngine;

public class TutorialUIScript : MonoBehaviour
{

    [SerializeField] private GameObject movementTutorial , fireTutorial , pauseButton , fireButton , gunChange;
    private int index;
    public void EnableFire()
    {
        if (index == 0)
        {
            pauseButton.SetActive(false);
            fireButton.SetActive(false);
            gunChange.SetActive(false);
            Time.timeScale = 0.1f;
            fireTutorial.SetActive(true);
        }
    }
    public void EnableMovement()
    {
        pauseButton.SetActive(false);
        fireButton.SetActive(false);
        gunChange.SetActive(false);
        Time.timeScale = 0.1f;
        movementTutorial.SetActive(true);
    }
    public void DisableFire()
    {
        pauseButton.SetActive(true);
        fireButton.SetActive(true);
        gunChange.SetActive(true);
        Time.timeScale = 1;
        fireTutorial.SetActive(false);
        index++;
        PlayerPrefs.SetInt("Tutorial" , index);
    }
    public void DisableMovement()
    {
        pauseButton.SetActive(true);
        fireButton.SetActive(true);
        gunChange.SetActive(true);
        Time.timeScale = 1;
        movementTutorial.SetActive(false);
    }
    public void Start()
    {
        fireTutorial.SetActive(false);
        movementTutorial.SetActive(false);
        index = 0;
        index = PlayerPrefs.GetInt("Tutorial");
        if(index == 0)
        {
            EnableMovement();
        }

    }
}
