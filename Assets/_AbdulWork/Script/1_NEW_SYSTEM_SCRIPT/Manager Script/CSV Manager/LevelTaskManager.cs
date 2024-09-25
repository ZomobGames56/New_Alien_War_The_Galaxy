using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTaskManager : MonoBehaviour
{
    private bool survivalBool;
    private int levelIndex;
    private Tasks currentTasks;
    private List<bool> taskBoolList = new List<bool>();
    private int objectPicked, runThrough, closeCalled, combo, multiKilled, objectDestroyed, pistolKill, shotgunKill, machinegunKill, rifleKill , meleeKill;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private List<TextMeshProUGUI> tasksName;
    [SerializeField] private LevelUIManager levelUIManager;
    [SerializeField] private MissionManager newMissionManager;
    [SerializeField] private TaskJSONManipulator taskJSONManipulator;
    private void Start()
    {
        levelManager = GetComponent<LevelManager>();
        newMissionManager = GetComponent<MissionManager>();
        taskJSONManipulator = GetComponent<TaskJSONManipulator>();
        if (PlayerPrefs.HasKey(MissionManager.MissionIndex))
        {
            levelIndex = PlayerPrefs.GetInt(MissionManager.MissionIndex);
        }
        else
        {
            levelIndex = 0;
        }
        UpdateUI();
    }
    private void UpdateUI()
    {
        currentTasks = taskJSONManipulator.GetTasksForMissionIndex(levelIndex);
        for (int i = 0; i < currentTasks.taskList.Count; i++)
        {
            tasksName[i].text = currentTasks.taskList[i].Name;
            if (currentTasks.taskList[i].Status)
            {
                tasksName[i].fontStyle = TMPro.FontStyles.Strikethrough;
            }
        }
    }
    private void OnEnable()
    {
        levelManager.LevelPaused += LevelManager_LevelPaused;
        levelUIManager.LevelCompleted += LevelUIManager_LevelCompleted;
    }
    private void OnDisable()
    {
        levelManager.LevelPaused -= LevelManager_LevelPaused;
        levelUIManager.LevelCompleted -= LevelUIManager_LevelCompleted;
    }
    private void LevelManager_LevelPaused(object sender, System.EventArgs e)
    {
        CheckForTasks();
    }
    private void LevelUIManager_LevelCompleted(object sender, System.EventArgs e)
    {
        survivalBool = true;
        CheckForTasks();
    }
    private void CheckForTasks()
    {
        taskBoolList.Clear();
        for (int i = 0; i < currentTasks.taskList.Count; i++)
        {
            taskBoolList.Add(currentTasks.taskList[i].Status);
        }
        for (int i = 0; i < currentTasks.taskList.Count; i++)
        {
            SwitchFunctions(i);
        }
    }
    private void SwitchFunctions(int index)
    {
        switch (currentTasks.taskList[index].Index)
        {
            case 0:
                {
                    Survive(index);
                    break;
                }
            case 1:
                {
                    Kills(index);
                    break;
                }
            case 2:
                {
                    ObjectPick(index);
                    break;
                }
            case 3:
                {
                    RunThrough(index);
                    break;
                }
            case 4:
                {
                    CloseCall(index);
                    break;
                }
            case 5:
                {
                    PisotlKill(index);
                    break;
                }
            case 6:
                {
                    ShotgunKill(index);
                    break;
                }
            case 7:
                {
                    MachineGunKill(index);
                    break;
                }
            case 8:
                {
                    RifleKill(index);
                    break;
                }
            case 9:
                {
                    MeleeKill(index);
                    break;
                }
            case 10:
                {
                    Combo(index);
                    break;
                }
            case 11:
                {
                    MultiKill(index);
                    break;
                }
            case 12:
                {
                    ObjectDestroy(index);
                    break;
                }

        }
    }
    //Task Indexes
    // Survive 0
    // ObjectPick 1
    // Kills 2
    // RunThoriugh 3
    // CloseCall 4
    // PistolKill 5
    // ShotgunKill 6
    // MachineGunKill 7
    // RifleKill 8
    // MeleeKill 9
    // Combo 10
    // MiltiKill 11
    // ObjectDestory 12

    //Task Completion Detection Function
    //Checking whether player survived or not is checked in levelComplete Event no need to implement again
    private void Survive(int taskIndex)
    {
        //print("Survive");
        if (survivalBool)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
            //TaskChange(index);
        }
    }
    //Done
    private void ObjectPick(int taskIndex)
    {
        //print("Object pick");
        if (objectPicked >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
            //TaskChange(index);
        }
    }
    //Done
    private void Kills(int taskIndex)
    {
        //print("Kills");
        if (levelUIManager.GetKill() >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void RunThrough(int taskIndex)
    {
        print("Run Through");
        if (runThrough >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }

    }
    //Done
    private void CloseCall(int taskIndex)
    {
        print("Close Call");
        if (closeCalled >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void PisotlKill(int taskIndex)
    {
        print("Weapon Kill");
        if (pistolKill >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void ShotgunKill(int taskIndex)
    {
        print("Weapon Kill");
        if (shotgunKill >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void MachineGunKill(int taskIndex)
    {
        print("Weapon Kill");
        if (machinegunKill >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void RifleKill(int taskIndex)
    {
        print("Weapon Kill");
        if (rifleKill >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void MeleeKill(int taskIndex)
    {
        print("Weapon Kill");
        if (meleeKill >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void Combo(int taskIndex)
    {
        print("Combo");
        if (combo >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void MultiKill(int taskIndex)
    {
        print("MultiKill");
        if (multiKilled >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Done
    private void ObjectDestroy(int taskIndex)
    {
        print("Object Destory");
        if (objectDestroyed >= currentTasks.taskList[taskIndex].Value)
        {
            if (currentTasks.mainMissionIndex == taskIndex)
            {
                newMissionManager.UnlockNext();
            }
            taskBoolList[taskIndex] = true;
            taskJSONManipulator.ChangeTasks(currentTasks.missionIndex, taskBoolList);
            UpdateUI();
        }
    }
    //Aditional Tasks which can be used if needed
    /*    private void Accuracy(int index)
    {
        if (bulletsFired > 0 && survivalBool)
        {
            if ((levelUIManager.GetKill() * 100) / bulletsFired >= int.Parse(taskValue[index].ToString()))
            {
                TaskChange(index);
            }
        }
        else if (survivalBool && bulletsFired == 0)
        {
            //TaskChange(index);
        }
    }*/
    /*    private void NoKill(int index)
            {
                if(levelUIManager.GetKill() == 0 && levelUIManager.GetDistance()<=0)
                {
                    TaskChange(index);
                }
    }*/
    /*    private void TaskChange(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        Task1Change();
                        break;
                    }
                case 1:
                    {
                        Task2Change();
                        break;
                    }
                case 2:
                    {
                        Task3Change();
                        break;
                    }
            }
        }*/
    /*  private void FirstBullet(int index)
        {
            if (bulletsFired == 15 && levelUIManager.GetKill() >= int.Parse(taskValue[index].ToString()))
            {
                TaskChange(index);
            }
        }*/

    //Funtions
    public void PickObject()
    {
        objectPicked++;
    }
    public void RunThroughObjects()
    {
        runThrough++;
    }
    //Done
    public void IncrementCloseCall()
    {
        closeCalled++;
    }
    //Done
    public void weaponKillthorughPistol()
    {
        pistolKill++;
    }
    //Done
    public void weaponKillThroughShotgun()
    {
        shotgunKill++;
    }
    //Done
    public void WeaponKillThorughMachineGun()
    {
        machinegunKill++;
    }
    //Done
    public void WeaponKillThroughRifle()
    {
        rifleKill++;
    }
    public void MeleeKills()
    {
        meleeKill++;
    }
    //Done
    public void ComboIncrement()
    {
        combo++;
    }
    //Done
    public void ResetCombo()
    {
        combo = 0;
    }
    //Done
    public void MultiKill()
    {
        multiKilled++;
    }
    public void ObjectDestroyed()
    {
        objectDestroyed++;
    }
}