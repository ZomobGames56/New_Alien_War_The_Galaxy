using UnityEngine;
using UnityEngine.UI;
public class PlanetSwitchScripts : MonoBehaviour
{
    private int planetNo, maxUnlockPlanets;
    [SerializeField] private Button selectButton;
    [SerializeField] private GameObject[] Planets;
    [SerializeField] private GameObject[] lockImage;
    [SerializeField] private MissionManager missionManager;
    void Start()
    {
        planetNo = 0;
        maxUnlockPlanets = missionManager.GetMaxUnlockPlanet();
        if (Planets.Length != 3)
        {
            Debug.LogError("Please assign exactly three images to the array.");
        }
        SelectButton();
        selectButton.onClick.AddListener(() => {
            missionManager.SetSelectedPlanet(planetNo);
        });
    }
    private void SelectButton()
    {
        if (planetNo <= maxUnlockPlanets)
        {
            selectButton.interactable = true;
            lockImage[1].SetActive(false);
        }
        else
        {
            selectButton.interactable = false;
            lockImage[1].SetActive(true);
        }
        Debug.Log(planetNo + " planet No");
        LockPrevious();
        LockForward();
    }
    void LockForward()
    {
        int NextPlanetNo = planetNo + 1;
        if(NextPlanetNo > Planets.Length-1)
        {
            NextPlanetNo = 0;
        }
        if (NextPlanetNo <= maxUnlockPlanets)
        {
            lockImage[2].SetActive(false);
        }
        else
            lockImage[2].SetActive(true);
    }
    void LockPrevious()
    {
        int PreviousPlanetNo = planetNo - 1;
        if(PreviousPlanetNo < 0)
        {
            PreviousPlanetNo = Planets.Length-1;
        }
        if (PreviousPlanetNo <= maxUnlockPlanets)
        {
            lockImage[0].SetActive(false);
        }
        else
        {
            lockImage[0].SetActive(true);
        }
    }
    public void InterchangeForward()
    {
        Sprite temp = Planets[0].GetComponent<Image>().sprite;
        for (int i = 0 ; i < Planets.Length - 1; i++)
        {
            Planets[i].GetComponent<Image>().sprite = Planets[i +1].GetComponent<Image>().sprite;
        }
        Planets[Planets.Length-1].GetComponent<Image>().sprite = temp;
        planetNo++;
        if (planetNo > Planets.Length-1)
        {
            planetNo = 0;
        }
        SelectButton();
    }
    public void InterchangeBackward()
    {
        Sprite temp = Planets[Planets.Length - 1].GetComponent<Image>().sprite;
        for (int i = Planets.Length - 1; i >= 1; i--)
        {
            Planets[i].GetComponent<Image>().sprite = Planets[i - 1].GetComponent<Image>().sprite;
        }
        Planets[0].GetComponent<Image>().sprite = temp;
        planetNo--;
        if(planetNo<0)
        {
            planetNo = Planets.Length - 1;
        }
        SelectButton();
    }
}
