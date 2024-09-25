using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public static bool gameOpen = false;
    [SerializeField] private GameObject landingPage , mainMenu , levelMenu, gunSelectMenu , gunUpgradeSelectMenu, gunUpgradeMenu , missionMenu , targetMenu , quitMenu;

    private float timer;
    private void Awake()
    {
        Time.timeScale = 1f;
        if(gameOpen)
        {
            ActiveCurrentScreen(mainMenu);
        }
        else
        {
            ActiveCurrentScreen(landingPage);
        }
        timer = 0;
    }
    public void Update()
    {
        if(!gameOpen)
        {
            timer += Time.deltaTime;
            if(timer>1)
            {
                gameOpen = true;
                ActiveCurrentScreen(mainMenu);
            }
        }
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void ActiveCurrentScreen(GameObject gameObject)
    {
        gunUpgradeSelectMenu.SetActive(false);
        landingPage.SetActive(false);
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        gunSelectMenu.SetActive(false);
        gunUpgradeMenu.SetActive(false);
        missionMenu.SetActive(false);
        targetMenu.SetActive(false);
        quitMenu.SetActive(false);
        gameObject.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
