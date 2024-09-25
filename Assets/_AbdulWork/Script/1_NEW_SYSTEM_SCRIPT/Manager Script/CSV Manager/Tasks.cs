using System;
using System.Collections.Generic;

using UnityEngine;
[Serializable]
public class Task
{
    public int Index;
    public int Value;
    public bool Status;
    public string Name;
    public Task(int index, int value, bool status, string name)
    {
        Index = index;
        Value = value;
        Status = status;
        Name = name;
    }
}
[Serializable]
public class Tasks
{

    public int missionIndex;
    public int mainMissionIndex;
    public List<Task> taskList = new List<Task>();

    public Tasks(int missionIndex, int mainMissionIndex , List<Task> taskList)
    {
        this.missionIndex = missionIndex;
        this.mainMissionIndex = mainMissionIndex;
        this.taskList = taskList;

    }
    /* public int missionIndex;
    public int task1Index;
    public bool task1Value;
    public bool task1Status;
    public string task1Name;
    public int task2Index;
    public int task2Value;
    public bool task2Status;
    public string task2Name;
    public int task3Index;
    public int task3Value;
    public bool task3Status;
    public string task3Name;
    public int task4Index;
    public int task4Value;
    public bool task4Status;
    public string task4Name;
    public int task5Index;
    public int task5Value;
    public bool task5Status;
    public string task5Name;
    public Tasks(int missionIndex,int task1Index,bool task1Value,bool task1Status,string task1Name,int task2Index,int task2Value,bool task2Status,string task2Name,int task3Index,int task3Value,bool task3Status,string task3Name,int task4Index,int task4Value,bool task4Status,string task4Name,int task5Index,int task5Value,bool task5Status,string task5Name)
    {
    this.missionIndex = missionIndex;
    this.task1Index = task1Index;
    this.task1Value = task1Value;
    this.task1Status = task1Status;
    this.task1Name = task1Name;
    this.task2Index = task2Index;
    this.task2Value = task2Value;
    this.task2Status = task2Status;
    this.task2Name = task2Name;
    this.task3Index = task3Index;
    this.task3Value = task3Value;
    this.task3Status = task3Status;
    this.task3Name = task3Name;
    this.task4Index = task4Index;
    this.task4Value = task4Value;
    this.task4Status = task4Status;
    this.task4Name = task4Name;
    this.task5Index = task5Index;
    this.task5Value = task5Value;
    this.task5Status = task5Status;
    this.task5Name = task5Name;
    }*/
}

