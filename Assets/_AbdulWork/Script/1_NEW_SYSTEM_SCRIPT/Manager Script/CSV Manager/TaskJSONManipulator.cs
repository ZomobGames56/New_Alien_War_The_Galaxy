using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

public class TaskJSONManipulator : MonoBehaviour
{
    [SerializeField] private List<Tasks> tasks = new List<Tasks>();
    private string taskFilePath;
    void Awake()
    {
        taskFilePath = Application.persistentDataPath + "/taskJSON.json";
        if (File.Exists(taskFilePath))
        {
        }
        else
        {
            SaveMissionsToJson(tasks);
        }
        tasks = LoadInfoFromJson(taskFilePath);
    }
    private void SaveMissionsToJson(List<Tasks> tasks)
    {
        string json = JsonUtility.ToJson(new TaskListWrapper(tasks), true);
        File.WriteAllText(taskFilePath, json);
    }
    private List<Tasks> LoadInfoFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            TaskListWrapper TaskListWrapper = JsonUtility.FromJson<TaskListWrapper>(json);
            return TaskListWrapper.tasks;
        }
        else
        {
            return new List<Tasks>();
        }
    }
    public void UpdateTasksList()
    {
        tasks = LoadInfoFromJson(taskFilePath);
    }
    private class TaskListWrapper
    {
        public List<Tasks> tasks;

        public TaskListWrapper(List<Tasks> tasks)
        {
            this.tasks = tasks;
        }
    }
    public Tasks GetTasksForMissionIndex(int index)
    {
        foreach (Tasks task in tasks)
        {
            if(task.missionIndex == index)
            {
                return task;
            }
        }
        return null;
    }
    public void ChangeTasks(int taskIndex , List<bool> taskStatus)
    {
        if (tasks[taskIndex].missionIndex == taskIndex)
        {
            for(int i =0; i < tasks[taskIndex].taskList.Count; i++)
            {
                tasks[taskIndex].taskList[i].Status = taskStatus[i];
            }
        }
        SaveMissionsToJson(tasks);
        UpdateTasksList();
    }
}