using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public event EventHandler LevelPaused;
    private bool spawned;
    private float offset;
    private GameObject currentTerrain;
    [SerializeField] private LevelUIManager levelUIManager;
    [SerializeField] private float scaleWidth, scaleLength;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject terrainObject;
    [SerializeField] private PlayerMovementScirpt playerMovementScript;
    [SerializeField] private GameObject inGameUI,pauseUI ,gamePauseUI , gameOverUI , gameEndUI;
    [SerializeField] GameObject[] terrains = new GameObject[4];
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1f;
        offset = scaleWidth / 4;
        terrains[0] = (terrainObject);
        terrains[1] = (Instantiate(terrainObject, terrainObject.transform.position + new Vector3(scaleWidth, 0, 0), Quaternion.identity));
        terrains[1].transform.localScale = new Vector3(-terrains[1].transform.localScale.x , terrains[1].transform.localScale.y , terrains[1].transform.localScale.z); 
        terrains[2] = (Instantiate(terrainObject, terrainObject.transform.position + new Vector3(0, 0, scaleLength), Quaternion.identity));
        terrains[2].transform.localScale = new Vector3(terrains[2].transform.localScale.x , terrains[2].transform.localScale.y , -terrains[2].transform.localScale.z); 
        terrains[3] = (Instantiate(terrainObject, terrainObject.transform.position + new Vector3(scaleWidth, 0, scaleLength), Quaternion.identity));
        terrains[3].transform.localScale = new Vector3(-terrains[3].transform.localScale.x , terrains[3].transform.localScale.y , -terrains[3].transform.localScale.z); 
        currentTerrain = terrains[0];
        //player = playerMovementScript.gameObject.transform;
    }
    private void OnEnable()
    {
        playerMovementScript.gameOverEvent += PlayerMovementScript_gameOverEvent;
        levelUIManager.LevelCompleted += LevelUIManager_LevelCompleted;
    }
    private void OnDisable()
    {
        playerMovementScript.gameOverEvent -= PlayerMovementScript_gameOverEvent;
        levelUIManager.LevelCompleted -= LevelUIManager_LevelCompleted;
    }
    private void LevelUIManager_LevelCompleted(object sender, EventArgs e)
    {
        GameEnd();
    }
    private void PlayerMovementScript_gameOverEvent(object sender, System.EventArgs e)
    {
        print("Player died");
        Time.timeScale = 0f;
        Switch(gameOverUI);
    }
    // Update is called once per frame
    void Update()
    {
        if(player.position.z > currentTerrain.transform.position.z + scaleLength/2 + offset / 2)
        {
            SwitchForward();
            terrains[2].transform.position = new Vector3(currentTerrain.transform.position.x ,0,currentTerrain.transform.position.z + scaleLength);
            terrains[3].transform.position = new Vector3(terrains[1].transform.position.x, 0, terrains[1].transform.position.z + scaleLength);
        }
        if (player.position.x > currentTerrain.transform.position.x + scaleWidth/3 - offset)
        {
            if (player.position.x > currentTerrain.transform.position.x + scaleWidth)
            {
                SwitchSide();
            }
            terrains[1].transform.position = new Vector3(currentTerrain.transform.position.x + scaleLength, 0, currentTerrain.transform.position.z);
            terrains[3].transform.position = new Vector3(terrains[2].transform.position.x + scaleLength, 0, terrains[2].transform.position.z);
        }
        if (player.position.x < currentTerrain.transform.position.x - scaleWidth / 3 + offset)
        {
            if (player.position.x < currentTerrain.transform.position.x - scaleWidth)
            {
                SwitchSide();
            }
            terrains[1].transform.position = new Vector3(currentTerrain.transform.position.x - scaleLength, 0, currentTerrain.transform.position.z);
            terrains[3].transform.position = new Vector3(terrains[2].transform.position.x - scaleLength, 0, terrains[2].transform.position.z);
        }
    }
    private void SwitchSide()
    {
        GameObject temp = terrains[0];
        terrains[0] = terrains[1];
        terrains[1] = temp;
        temp = terrains[2];
        terrains[2] = terrains[3];
        terrains[3] = temp;
        currentTerrain = terrains[0];
    }
    private void SwitchForward()
    {
        GameObject temp = terrains[0];
        terrains[0] = terrains[2];
        terrains[2] = temp;
        temp = terrains[1];
        terrains[1] = terrains[3];
        terrains[3] = temp;
        currentTerrain = terrains[0];
    }
    public void GamePause()
    {
        LevelPaused?.Invoke(this, EventArgs.Empty);
        Time.timeScale = 0f;
        Switch(gamePauseUI);
    }
    public void GameResume()
    {
        Time.timeScale = 1f;
        Switch(inGameUI);
    }
    public void GameRestart() {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    private void GameEnd()
    {
        Time.timeScale = 0f;
        Switch(gameEndUI);
    }
    private void Switch(GameObject activeObject)
    {
        if(activeObject != inGameUI)
        {
            pauseUI.SetActive(true);
        }else
        {
            pauseUI.SetActive(false);
        }
        inGameUI.SetActive(false);
        gamePauseUI.SetActive(false);
        gameOverUI.SetActive(false);
        gameEndUI.SetActive(false);

        activeObject.SetActive(true);
    }
}
