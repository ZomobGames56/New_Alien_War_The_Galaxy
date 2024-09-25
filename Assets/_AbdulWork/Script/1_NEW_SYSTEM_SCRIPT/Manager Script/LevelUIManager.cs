using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class LevelUIManager : MonoBehaviour
{
    public event EventHandler LevelCompleted;
    private float targetDistance, distance;
    private int kills ;
    private float currentPosition;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private Transform player;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MissionManager missionManager;
    private void Start()
    {
        missionManager = GetComponent<MissionManager>();
        levelManager = GetComponent<LevelManager>();
        Debug.LogWarning("Inplement Level Detail properly");
        //Temp Code remove and add proper funtionality
        currentPosition = player.position.z;
        kills = 0;
        //targetDistance = missionManager.GetTargetDistance(missionIndex);
        targetDistance = 750;
    }
    private void Update()
    {
        distance = targetDistance - player.position.z + currentPosition;
        killText.text = kills.ToString();
        distanceText.text = ((int)(distance) + "ft").ToString();
        if(distance<0)
        {
            LevelCompleted?.Invoke(this , EventArgs.Empty);
        }
    }
    public void IncrementKill()
    {
         kills++;
    }
    public int GetKill()
    {
        return kills;
    }
    public float GetDistance()
    {
        return distance;
    }
}
