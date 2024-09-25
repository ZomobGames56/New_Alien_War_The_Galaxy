using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MissionJSONManipulator : MonoBehaviour
{
    private string missionFilePath;
    public List<Mission> missions = new List<Mission>();
    private List<Mission> loadedMissions = new List<Mission>();
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    void Awake()
    {
        missionFilePath = Application.persistentDataPath + "/missionJSON.json";
        if (File.Exists(missionFilePath))
        {
        }
        else
        {
            SaveMissionsToJson(missions);
        }
        loadedMissions = LoadInfoFromJson(missionFilePath);
    }
    private void SaveMissionsToJson(List<Mission> missions)
    {
        string json = JsonUtility.ToJson(new MissionListWrapper(missions), true);
        File.WriteAllText(missionFilePath, json);
    }
    private List<Mission> LoadInfoFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            MissionListWrapper MissionListWrapper = JsonUtility.FromJson<MissionListWrapper>(json);
            return MissionListWrapper.missions;
        }
        else
        {
            return new List<Mission>();
        }
    }
    public List<Mission> GetMissionList()
    {
        loadedMissions = LoadInfoFromJson(missionFilePath);
        return loadedMissions;
    }

    public void SetMissionList(List<Mission> missionList)
    {
        SaveMissionsToJson(missionList);
    }
    public void UnlockNextMission(int index)
    {
        List<Mission> missions = loadedMissions;
        foreach (Mission mission in missions)
        {
            if (mission.missionIndex == index + 1)
            {
                mission.unlocked = true;
            }
        }
        SaveMissionsToJson(missions);
        loadedMissions = LoadInfoFromJson(missionFilePath);
        weaponJSONHandler.UnlockWeapon(index);
    }
    //public int GetMaxUnlockPlanet()
    //{
    //    int planetIndex = 0;
    //    List<Mission> missions = loadedMissions;
    //    foreach (Mission mission in missions)
    //    {
    //        if (mission.unlocked == true)
    //        {
    //            planetIndex = mission.planetNo;
    //        }
    //    }
    //    return planetIndex;
    //}
    //public int GetMaxUnlockMissionOfSelectedPlanet(int planetIndex)
    //{
    //    int missionIndex = 0;
    //    List<Mission> missions = loadedMissions;
    //    foreach (Mission mission in missions)
    //    {
    //        if (mission.planetNo == planetIndex)
    //        {
    //            if (mission.unlocked == true)
    //            {
    //                missionIndex = mission.missionNo;
    //            }
    //        }
    //    }
    //    return planetIndex;
    //}
    //public Mission GetMission(int missionIndex)
    //{
    //    foreach (Mission mission in loadedMissions)
    //    {

    //        if (missionIndex == mission.missionIndex)
    //        {
    //            return mission;
    //        }
    //    }
    //    return null;
    //}
    private class MissionListWrapper
    {
        public List<Mission> missions;

        public MissionListWrapper(List<Mission> missions)
        {
            this.missions = missions;
        }
    }
}
