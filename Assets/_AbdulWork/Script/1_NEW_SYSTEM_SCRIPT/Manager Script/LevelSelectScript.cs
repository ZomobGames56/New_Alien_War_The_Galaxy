using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectScript : MonoBehaviour
{
    private GameObject[] levelButtons;
    [SerializeField] private GameObject parentButton;
    [SerializeField] private MissionManager missionManager;

    // Start is called before the first frame update
    void Start()
    {
        int childCount = parentButton.transform.childCount;
        levelButtons = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            levelButtons[i] = parentButton.transform.GetChild(i).gameObject;
        }
        //Debug.Log("Maximun unlocked mision" + missionManager.GetMaxUnlockLevel(1));
        //for (int i = 0; i < missionManager.GetMaxUnlockLevel(1); i++)
        //{
        //    SetLevelButtonActive(i);
        //}
    }
    //private void SetLevelButtonActive(int index)
    //{
    //    levelButtons[index].transform.GetChild(0).gameObject.SetActive(true);
    //    levelButtons[index].GetComponent<Button>().interactable = true;
    //}
}