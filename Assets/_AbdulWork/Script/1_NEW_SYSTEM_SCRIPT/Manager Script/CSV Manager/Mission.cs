using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mission
{
    public int missionIndex;
    public int planetNo;
    public int missionNo;
    public bool unlocked;

    public Mission(int missionIndex, int planetNo, int missionNo, int distance, bool unlocked)
    {
        this.missionIndex = missionIndex;
        this.planetNo = planetNo;
        this.missionNo = missionNo;
        this.unlocked = unlocked;
    }
}
