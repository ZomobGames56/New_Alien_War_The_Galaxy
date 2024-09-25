using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static LevelTaskManager;
using JetBrains.Annotations;
public class TaskManager : MonoBehaviour
{
    private int missionIndex;
    private enum TaskEnums
    {
        Survive,
        Kills,
        Combo,
        FirstBullet,
        Accuracy,
        CloseCall,
        NoKill
    }
    //Level Index the current level going on
    private int levelIndex;
    private List<Task> missionTasks = new List<Task>();
    private TaskEnums[] taskenum = new TaskEnums[5];
    private Mission currentMission;
    [SerializeField] private List<TextMeshProUGUI> taskText = new List<TextMeshProUGUI>();
    [SerializeField] private MissionManager newMissionManager;
    [SerializeField] private TaskJSONManipulator taskJSONManipulator;
    private void Start()
    {
        if (PlayerPrefs.HasKey(MissionManager.MissionIndex))
        {
            missionIndex = PlayerPrefs.GetInt(MissionManager.MissionIndex);
            currentMission = newMissionManager.GetCurrentMission(missionIndex);
        }
        else
        {
            missionIndex = 0;
        }
        newMissionManager = GetComponent<MissionManager>();
        taskJSONManipulator = GetComponent<TaskJSONManipulator>();
    }
    public void GetMissionUI()
    {
        missionTasks.Clear();
        if(PlayerPrefs.HasKey(MissionManager.MissionIndex))
        {
            missionIndex = PlayerPrefs.GetInt(MissionManager.MissionIndex);
        }
        else
        {
            missionIndex = 0;
        }
        currentMission = newMissionManager.GetCurrentMission(missionIndex);
        Tasks currentTasks= taskJSONManipulator.GetTasksForMissionIndex(missionIndex);
        for (int i = 0; i < currentTasks.taskList.Count; i++)
        {
            missionTasks.Add(currentTasks.taskList[i]);
        }
        UpdateUI();
    }
    public void UpdateUI()
    {
        if(taskText.Count == missionTasks.Count)
        {
            for (int i = 0; i < missionTasks.Count; i++)
            {
                taskText[i].text = missionTasks[i].Name;
                if (missionTasks[i].Status)
                {
                    taskText[i].fontStyle = FontStyles.Strikethrough;
                }
            }
        }
        else
        {
            Debug.LogError("Text and mission misMatch check the count the text file count are " + taskText.Count + " and mission count are " + missionTasks.Count);
        }
    }
}
