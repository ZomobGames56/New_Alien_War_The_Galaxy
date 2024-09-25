using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MissionManager : MonoBehaviour
{
    [SerializeField] private TaskManager taskManager;
    public const string MissionIndex = "MissionIndex";
    public const string SelectedPlanet = "SelectedPlanet";
    [SerializeField] private List<GameObject> planetMaps = new List<GameObject>();
    private List<Button> missionButtons = new List<Button>();
    private List<Mission> missionList = new List<Mission>();
    private int currentLevels;
    private int unlockedLevels;
    private int selectedPlanet;
    private int unlockPlanets;

    [SerializeField] private MissionJSONManipulator jsonManipulator;
    private void Awake()
    {
        if(PlayerPrefs.HasKey(SelectedPlanet))
        {
            selectedPlanet = PlayerPrefs.GetInt(SelectedPlanet);
        }
        else
        {
            selectedPlanet = 0;
        }
        taskManager = GetComponent <TaskManager>(); 
        jsonManipulator = GetComponent<MissionJSONManipulator>();
    }
    public void Start()
    {
        currentLevels = PlayerPrefs.GetInt(MissionManager.MissionIndex);
        //missionList = jsonManipulator.GetMissionList();
        RefreshData();
    }
    public void PlaySelected()
    {
        int i = PlayerPrefs.GetInt(SelectedPlanet);
        MissionPanelActive();
    }
    public void SetSelectedPlanet(int index)
    {
        selectedPlanet = index;
        PlayerPrefs.SetInt(SelectedPlanet, selectedPlanet);
        Debug.Log(selectedPlanet);
        MissionPanelActive();
    }
    public void MissionPanelActive()
    {
        selectedPlanet = PlayerPrefs.GetInt(SelectedPlanet);
        //Deactivating All maps 
        foreach(GameObject go in planetMaps)
        {
            go.SetActive(false);
        }
        //Activating Only Selected Map
        planetMaps[selectedPlanet].SetActive(true);
        //Getting all the buttons in the selected map
        missionButtons = planetMaps[selectedPlanet].GetComponentsInChildren<Button>().ToList();
        //Deactivating all the buttons
        foreach(Button button in missionButtons)
        {
            button.gameObject.SetActive(false);
        }
        //Value of the number of mission Unlocked in the current map
        int planetMissions = GetMaxUnlockLevelOfCurrentPlanet(selectedPlanet , missionButtons);
        //Activating only buttons for those levels which have been unlocked        
        for(int i =0; i< planetMissions; i++)
        {
            missionButtons[i].gameObject.SetActive(true);
        }

    }
    //Done
    public int GetCurrentLevel()
    {
        return currentLevels;
    }
    //Done
    public void UnlockNext()
    {
        jsonManipulator.UnlockNextMission(currentLevels);
        RefreshData();
    }
    //Done
    public int GetMaxUnlockLevelOfCurrentPlanet(int planetIndex , List<Button> mapsButton)
    {
        int buttonIndex = 0;
        int planetMissions = 0;
        foreach (Mission mission in missionList)
        {
            if (mission.planetNo == selectedPlanet && mission.unlocked)
            {
                planetMissions = mission.missionNo;
                mapsButton[buttonIndex].gameObject.SetActive(true);
                mapsButton[buttonIndex].onClick.AddListener(() => {
                    PlayerPrefs.SetInt(MissionIndex, mission.missionIndex);
                    taskManager.GetMissionUI();
                });
                buttonIndex++;
            }
        }
        return planetMissions;
    }
    //Done
    public int GetMaxUnlockLevel(int planetIndex)
    {
        RefreshData();
        foreach (Mission mission in missionList)
        {
            if (mission.unlocked)
            {
                unlockedLevels = mission.missionNo;
            }
        }
        return unlockedLevels;
    }
    //Done
    public int GetMaxUnlockPlanet()
    {
        RefreshData();
        foreach (Mission mission in missionList)
        {
            if (mission.unlocked)
            {
                unlockPlanets = mission.planetNo;
            }
        }
        return unlockPlanets;
    }
    //Done
    public void RefreshData()
    {
        missionList = jsonManipulator.GetMissionList();
    }
    //Done
    public Mission GetCurrentMission(int missionIndex)
    {
        foreach(Mission missions in missionList)
        {
            if(missions.missionIndex == missionIndex)
            {
                return missions;
            }
        }
        return null;
    }
}

