using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class MainMenuScript : MonoBehaviour
{

    public static bool gameOpen = false;
    [SerializeField] private GameObject transitionPanel;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private GameObject landingPage , mainMenu , levelMenu, gunSelectMenu , DailyMissionPanel , SettingPanel, gunUpgradeMenu , missionMenu , targetMenu , quitMenu;

    private float timer;
    private void Awake()
    {
        Time.timeScale = 1f;
        if(gameOpen)
        {
            SwitchScreen(mainMenu);
        }
        else
        {
            SwitchScreen(landingPage);
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
    public void ActiveCurrentScreen(GameObject obj)
    {
        transitionPanel.SetActive(true);
        transitionAnimator.Play("In");
        StartCoroutine(AnimationFunction(SwitchScreen, "In" , obj));
    }
    private void SwitchScreen(GameObject obj)
    {
        DailyMissionPanel.SetActive(false);
        SettingPanel.SetActive(false);
        landingPage.SetActive(false);
        mainMenu.SetActive(false);
        levelMenu.SetActive(false);
        gunSelectMenu.SetActive(false);
        gunUpgradeMenu.SetActive(false);
        missionMenu.SetActive(false);
        targetMenu.SetActive(false);
        quitMenu.SetActive(false);
        
        obj.SetActive(true);
        transitionAnimator.Play("Out");
        StartCoroutine(AnimationFunction(DisablePanel, "Out", transitionPanel));
    }
    private void DisablePanel(GameObject obj)
    {
        obj.SetActive(false);
    }
    IEnumerator AnimationFunction(Action<GameObject> action, string animName , GameObject obj)
    {
        do
        {
            yield return null;
        } while (transitionAnimator.GetCurrentAnimatorStateInfo(0).IsName(animName));
        action(obj);
        StopCoroutine("AnimationFunction");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
