using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Animations.Rigging;
using System.Linq;
using System.Runtime.CompilerServices;
public class WeaponMenuHandler : MonoBehaviour
{
    /*// Old Script just for refference
    //Temp values for slider for games
    private int maxLevel = 5;
    [SerializeField] private Text unlockLevelText;
    [SerializeField] private WeaponUpgradeMenu weaponUpgradeMenu;
    [SerializeField] private GameObject Lock, Upgrade , maxUpgraded;
    [SerializeField] private WeaponMenuPanel weaponMenuPanel;
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    [SerializeField] private Image weaponImage;
    [SerializeField] private TextMeshProUGUI weaponName, weaponLevel;
    [SerializeField] private TextMeshProUGUI[] weaponSpecTexts;
    [SerializeField] private TextMeshProUGUI[] weaponValueTexts;
    [SerializeField] private Slider[] slider;
    //[SerializeField] private Slider[] weaponSlider;

    private Weapon weapon;
    public void SetWeapon(Weapon _weapon)
    {
        weapon = _weapon;
        SetUI();
    }
    private void SetUI()
    {
        unlockLevelText.text = ("Unlock at Level " + (weapon.index+1)).ToString();
        weaponImage.sprite = weapon.image;
        weaponName.text = weapon.name;
        weaponLevel.text = ("Level " + weapon.level).ToString();
        weaponSpecTexts[0].text = "Damage";
        weaponValueTexts[0].text = weapon.damage.ToString();
        slider[0].value = weapon.damage / maxDamage;
        weaponSpecTexts[1].text = "Range";
        weaponValueTexts[1].text = weapon.range.ToString();
        slider[1].value = weapon.range / maxRange;
        weaponSpecTexts[2].text = "FireRate";
        weaponValueTexts[2].text = weapon.fireRate.ToString();
        slider[2].value = weapon.fireRate / maxFireRate;
        weaponSpecTexts[3].text = "Mazgine";
        weaponValueTexts[3].text = weapon.mazgine.ToString();
        slider[3].value = weapon.mazgine / maxMazgine;
        if (weapon.isAvailable)
        {
            if (weapon.level >= maxLevel)
            {
                SwitchButtons(maxUpgraded);
            }
            else
            {
                SwitchButtons(Upgrade);
            }
        }
        else
        {
            SwitchButtons(Lock);
        }

    }
    private void SwitchButtons(GameObject obj)
    {
        Upgrade.SetActive(false);
        Lock.SetActive(false);
        maxUpgraded.SetActive(false);

        obj.SetActive(true);
    }
    public void UpgradeButton()
    {
        weaponUpgradeMenu.LoadDetail(weapon.index);
    }
    public void UnlockWeapon()
    {
        weaponJSONHandler.UnlockWeapon(weapon.index);
        weaponMenuPanel.ReloadUI();
    }*/
    
    
    //Default Values
    private float maxDamage = 300f, maxRange = 300f, maxFireRate = 50f, maxMazgine = 100f , maxLevel = 5 , basePrice = 50; 
    //new script
    private Slider[] sliders;
    private int  currentWeaponIndex , categoryIndex , price;
    private Weapon.WeaponCategory category;
    private List<GameObject> spawnerButtons = new List<GameObject>();
    private List<Weapon> allweapons = new List<Weapon>(), currentWeaponList = new List<Weapon>();


    //serialized Variables
    [SerializeField] private GameObject[] Specs;
    [SerializeField] private GameObject GunPanel;
    [SerializeField] private Image mainWepaonImage;
    [SerializeField] private CurrencyScript currencyScript;
    [SerializeField] private RectTransform gunHolderTransform;
    [SerializeField] private Button nextButton, previousButton;
    [SerializeField] private RectTransform slider, sliderHandle;
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    [SerializeField] private GameObject gunHolderPanel , goldInfo;
    [SerializeField] private GameObject H_gunButtonPrefab , NH_gunButtonPrefab;
    [SerializeField] private TextMeshProUGUI mainWeaponName , levelInfo , priceInfo;
    [SerializeField] private GameObject upgradeButton, unlockButton, lockButton , maxUpgraded;
    private void Start()
    {
        previousButton.gameObject.SetActive(false);
        gunHolderTransform = gunHolderPanel.GetComponent<RectTransform>();
        sliders = new Slider[Specs.Length];
        for(int i = 0; i< Specs.Length; i++)
        {
            sliders[i] = GetComponent<Slider>();
        }
        RefreshWeaponList();
        for(int i = 3; i<=allweapons.Count; i++)
        {
            spawnerButtons.Add(Instantiate(NH_gunButtonPrefab, gunHolderTransform));

        }
        SwitchWeaponType(0);
    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
        }
        if(Input.GetKey(KeyCode.Q))
        {
        }
    }
    public void Forward()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex == currentWeaponList.Count-1)
        {
            nextButton.gameObject.SetActive(false);
        }
        previousButton.gameObject.SetActive(true);
        H_gunButtonPrefab.transform.SetSiblingIndex(currentWeaponIndex);
        gunHolderTransform.anchoredPosition = gunHolderTransform.anchoredPosition +new Vector2(-540,0);
        RefreshDataAndValues();
    }
    public void Back()
    {
        currentWeaponIndex--; 
        if(currentWeaponIndex == 0)
        {
            previousButton.gameObject.SetActive(false);
        }
        nextButton.gameObject.SetActive(true);
        H_gunButtonPrefab.transform.SetSiblingIndex(currentWeaponIndex);
        gunHolderTransform.anchoredPosition = gunHolderTransform.anchoredPosition + new Vector2(540,0);
        RefreshDataAndValues();
    }
    public void SwitchWeaponType(int index)
    {
        categoryIndex = index + 1;
        currentWeaponList.Clear();
        category= Weapon.WeaponCategory.Pistol;
        switch(index)
        {
            case 0:
                {
                    category = Weapon.WeaponCategory.Pistol;
                    break;
                }
            case 1:
                {
                    category = Weapon.WeaponCategory.Shotgun;
                    break;
                }
            case 2:
                {
                    category = Weapon.WeaponCategory.MachineGun;
                    break;
                }
            case 3:
                {
                    category = Weapon.WeaponCategory.Rifle;
                    break;
                }
        }
        foreach(Weapon weapon in allweapons)
        {
            if(weapon.category == category)
            {
                currentWeaponList.Add(weapon);
            }
        }
        currentWeaponIndex = 0;
        H_gunButtonPrefab.transform.SetSiblingIndex(0);
        gunHolderTransform.anchoredPosition = new Vector2(0, 20);
        previousButton.gameObject.SetActive(false);
        if(currentWeaponList.Count > 1)
        {
            nextButton.gameObject.SetActive(true);
        }
        foreach(GameObject Objects in spawnerButtons)
        {
            Objects.GetComponent<RectTransform>().localScale = Vector3.one;
            Objects.SetActive(false);
        }
        for(int i =0; i<= currentWeaponList.Count-3; i++)
        {
            spawnerButtons[i].gameObject.SetActive(true);
        }
        RefreshDataAndValues();
    }
    private void RefreshDataAndValues()
    {
        SetSlider();
        RefreshWeaponList();
        if (!currentWeaponList[currentWeaponIndex].isUnlocked)
        {
            SwitchButtons(lockButton);
            goldInfo.SetActive(false);
            lockButton.GetComponentInChildren<Text>().text = "Unlock at level " + (currentWeaponList[currentWeaponIndex].index + 1);
        }
        else if (!currentWeaponList[currentWeaponIndex].isAvailable)
        {
            SwitchButtons(unlockButton);
            goldInfo.SetActive(true);
        }
        else if (currentWeaponList[currentWeaponIndex].level < maxLevel)
        {
            SwitchButtons(upgradeButton);
            goldInfo.SetActive(true);
        }
        else
        {
            SwitchButtons(maxUpgraded);
            goldInfo.SetActive(false);
        }
        levelInfo.text = "Level " + currentWeaponList[currentWeaponIndex].level;
        Specs[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Damage";
        Specs[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Range";
        Specs[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "FirRate";
        Specs[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Magzine";
        Specs[0].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentWeaponList[currentWeaponIndex].damage+"/"+maxDamage;
        Specs[1].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentWeaponList[currentWeaponIndex].range+"/"+maxRange;
        Specs[2].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentWeaponList[currentWeaponIndex].fireRate+"/"+maxFireRate;
        Specs[3].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = currentWeaponList[currentWeaponIndex].mazgine+"/"+maxMazgine;
        Specs[0].transform.GetChild(2).GetComponent<Slider>().value = currentWeaponList[currentWeaponIndex].damage / maxDamage;
        Specs[1].transform.GetChild(2).GetComponent<Slider>().value = currentWeaponList[currentWeaponIndex].range / maxRange;
        Specs[2].transform.GetChild(2).GetComponent<Slider>().value = currentWeaponList[currentWeaponIndex].fireRate / maxFireRate;
        Specs[3].transform.GetChild(2).GetComponent<Slider>().value = currentWeaponList[currentWeaponIndex].mazgine / maxMazgine;
        price = (int)(categoryIndex * basePrice * currentWeaponList[currentWeaponIndex].level * (currentWeaponIndex + 1));
        if (!currentWeaponList[currentWeaponIndex].isAvailable)
        {
            price = (int)(price * .75f);
        }

        priceInfo.text = price.ToString();
        mainWeaponName.text = currentWeaponList[currentWeaponIndex].name;
        mainWepaonImage.sprite = currentWeaponList[currentWeaponIndex].image;
        //int length = gunHolderPanel.GetComponentsInChildren<WeaponButtonScript>().Length;
        WeaponButtonScript[] script = gunHolderPanel.GetComponentsInChildren<WeaponButtonScript>();
        for (int i =0; i < currentWeaponList.Count ; i++)
        {
            script[i].SetWeaponDetails(currentWeaponList[i].image, currentWeaponList[i].name , currentWeaponList[i].isAvailable);
        }
    }
    private void SetSlider()
    {
        Vector2 sizeDelta = sliderHandle.sizeDelta;
        sizeDelta.x = slider.sizeDelta.x/currentWeaponList.Count;
        sliderHandle.sizeDelta = sizeDelta;
        sliderHandle.localPosition =new Vector2((- slider.sizeDelta.x)/2 + sliderHandle.sizeDelta.x/2 + sliderHandle.sizeDelta.x * currentWeaponIndex,sliderHandle.localPosition.y);
    }
    private void SwitchButtons(GameObject currentObject)
    {
        upgradeButton.SetActive(false);
        unlockButton.SetActive(false);
        lockButton.SetActive(false);
        maxUpgraded.SetActive(false);

        currentObject.SetActive(true);
    }
    private void RefreshWeaponList()
    {
        currentWeaponList.Clear();
        allweapons = weaponJSONHandler.GetAllWeaponList();
        foreach (Weapon weapon in allweapons)
        {
            if (weapon.category == category)
            {
                currentWeaponList.Add(weapon);
            }
        }
    }
    public void UnlockWeapon()
    {
        weaponJSONHandler.UnlockWeapon(currentWeaponList[currentWeaponIndex].index);
        RefreshDataAndValues();
    }
    public void PurchaseWeapon()
    {
        if(price<=currencyScript.GetCoin())
        {
            weaponJSONHandler.PurchaseWeapon(currentWeaponList[currentWeaponIndex].index);
            currencyScript.RemoveCoin(price);
            RefreshDataAndValues();
        }
        else
        {
            print("Not enough money");
        }
    }
    public void UpgradeWeapon()
    {
        if (price <= currencyScript.GetCoin())
        {
            weaponJSONHandler.UpgradeWeapon(currentWeaponList[currentWeaponIndex].index);
            currencyScript.RemoveCoin(price);
            RefreshDataAndValues();
        }
        else
        {
            print("Not enough money");
        }
    }
}
