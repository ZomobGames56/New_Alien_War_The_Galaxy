using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLoadingScript : MonoBehaviour
{
    private int index;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image slider;
    float progress;

    private void Start()
    {
        loadingScreen.SetActive(false);
    }
    public void PlayBtn()
    {
        index = PlayerPrefs.GetInt(MissionManager.MissionIndex)+1;
        StartCoroutine(LoadSceneAsync());
    }
    IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            loadingScreen.SetActive(true);
            progress = Mathf.Clamp01(operation.progress / .9f);
            slider.fillAmount = progress;
            yield return null;
        }
    }
}
