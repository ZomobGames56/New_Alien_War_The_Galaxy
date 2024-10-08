using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
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


    //UI Button Logic
    /*
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
        for (int i =0; i < currentWeaponList.Count ; i++)\
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
    }*/
    
    //Default Values
    private float maxDamage = 300f, maxRange = 300f, maxFireRate = 50f, maxMazgine = 100f , maxLevel = 5 , basePrice = 50;

    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private GameObject weaponButtonHolder;
    [SerializeField] private GameObject weaponButtonPrefab;
    [SerializeField] private CurrencyScript currencyScript;
    [SerializeField] private WeaponJSONHandler weaponJSONScript;
    [SerializeField] private List<CategoryButtonScript> categoriesButtons = new List<CategoryButtonScript>();
    
    
    [Header("Main Wepaon parts")]
    [SerializeField] private Image mainWeaponImage;
    [SerializeField] private WeaponSpecScript rangeSpecScript;
    [SerializeField] private WeaponSpecScript damageSpecScript;
    [SerializeField] private WeaponSpecScript magzineSpecScript;
    [SerializeField] private WeaponSpecScript fireRateSpecScript;
    [SerializeField] private TextMeshProUGUI mainWeaponName , mainWeaponLevel;
    
    
    [Header("Buttons")]
    [SerializeField] private GameObject lockButton, buynowButton, upgradeButton, maxUpgradedButton;
    
    
    [Header("currency")]
    [SerializeField] private GameObject pricePanel;
    [SerializeField] private TextMeshProUGUI priceText;
    private int price;
    private int weaponIndex;
    private List<SingleWeaponButtonScript> weaponButtons = new List<SingleWeaponButtonScript>();
    private List<Weapon> allWeapons = new List<Weapon>() , currentWeaponList = new List<Weapon>();
    private void Start()
    {
        readAllWeapon();
        for(int i = 0; i < allWeapons.Count; i++)
        {
            GameObject temp= Instantiate(weaponButtonPrefab, weaponButtonHolder.transform);
            temp.transform.localScale = Vector3.one;
            weaponButtons.Add(temp.GetComponent<SingleWeaponButtonScript>());
            weaponButtons[weaponButtons.Count - 1].SetWeaponMenu(this);
        }
        ClearAllButtonsAnimations();
        //weaponButtons[0].highlightButton();
        categoriesButtons[0].highlightButton();
        //switchCategory(Weapon.WeaponCategory.Pistol);
        foreach(CategoryButtonScript categoryscript in categoriesButtons)
        {
            categoryscript.clearVerticalEvent += Categoryscript_clearVerticalEvent;
        }
        foreach(SingleWeaponButtonScript singleWeaponButton in weaponButtons)
        {
            singleWeaponButton.clearHorizontalEvent += SingleWeaponButton_clearHorizontalEvent;
        }
    }

    private void SingleWeaponButton_clearHorizontalEvent(object sender, System.EventArgs e)
    {
        ClearWeaponButtons();
    }

    private void Categoryscript_clearVerticalEvent(object sender, System.EventArgs e)
    {
        ClearCategory();
    }

    public void switchCategory(Weapon.WeaponCategory _category)
    {
        currentWeaponList.Clear();
        foreach(Weapon weapon in allWeapons)
        {
            if(weapon.category == _category )
            {
                currentWeaponList.Add(weapon);
            }
        }
        ClearWeaponButtons();
        weaponButtons[0].gameObject.SetActive(true);
        weaponButtons[0].SetWeapon(currentWeaponList[0]);
        weaponButtons[0].ButtonPressed();
        RefreshButtons();
    }
    public void SetMainWeapon(Weapon weapon)
    {
        price = (int)((weapon.index - currentWeaponList[0].index + 1)* basePrice * weapon.level * (int)currentWeaponList[0].category);
        weaponIndex = weapon.index;
        mainWeaponName.text = weapon.name;
        mainWeaponImage.sprite = weapon.image;
        mainWeaponLevel.text = "Level " + weapon.level;
        rangeSpecScript.SetSpecsValue("Range" , (weapon.range+"/"+maxRange), weapon.range/maxRange);
        damageSpecScript.SetSpecsValue("Damage" , (weapon.damage + "/" + maxDamage), weapon.damage / maxDamage);
        magzineSpecScript.SetSpecsValue("Magzine" , (weapon.mazgine + "/" + maxMazgine), weapon.mazgine / maxMazgine);
        fireRateSpecScript.SetSpecsValue("Fire Rate" , (weapon.fireRate + "/" + maxFireRate), weapon.fireRate / maxFireRate);

        if(!weapon.isUnlocked)
        {
            Switchbuttons(lockButton);
            pricePanel.SetActive(false);
            lockButton.GetComponentInChildren<Text>().text = "Unlock at level " + (weapon.index+1);
        }
        else if(!weapon.isAvailable)
        {
            pricePanel.SetActive(true);
            Switchbuttons(buynowButton);
            priceText.text = price.ToString();
        }
        else if(weapon.isAvailable)
        {
            if(weapon.level<maxLevel)
            {
                pricePanel.SetActive(true);
                Switchbuttons(upgradeButton);
                priceText.text = price.ToString();
            }
            else
            {
                pricePanel.SetActive(false);
                Switchbuttons(maxUpgradedButton);
            }
        }
    }
    private void RefreshButtons()
    {
        foreach(SingleWeaponButtonScript weaponbutton in  weaponButtons)
        {
            weaponbutton.gameObject.SetActive(false);
        }
        for (int i = 0; i< currentWeaponList.Count; i++)
        {
            weaponButtons[i].gameObject.SetActive(true);
            weaponButtons[i].SetWeapon(currentWeaponList[i]);
        }
        weaponButtonHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(500 * currentWeaponList.Count, weaponButtonHolder.GetComponent<RectTransform>().sizeDelta.y);
        scrollbar.value = 0;
        if((weaponButtonHolder.GetComponent<RectTransform>().sizeDelta.x - 1600)<0)
        {
            weaponButtonHolder.GetComponent<RectTransform>().localPosition = new Vector2(0, weaponButtonHolder.GetComponent<RectTransform>().localPosition.y);
        }
        else
        {
            weaponButtonHolder.GetComponent<RectTransform>().localPosition = new Vector2((weaponButtonHolder.GetComponent<RectTransform>().sizeDelta.x - 1600) / 2 , weaponButtonHolder.GetComponent<RectTransform>().localPosition.y);
        }
    }
    private void Switchbuttons(GameObject activeButton)
    {
        lockButton.SetActive(false);
        buynowButton.SetActive(false);
        upgradeButton.SetActive(false);
        maxUpgradedButton.SetActive(false);

        activeButton.SetActive(true);
    }
    public void UpgradeWeapon()
    {
        if (price <= currencyScript.GetCoin())
        {
            currencyScript.RemoveCoin(price);
            weaponJSONScript.UpgradeWeapon(weaponIndex);
            readAllWeapon();
            Weapon.WeaponCategory category = currentWeaponList[0].category;
            currentWeaponList.Clear();
            foreach (Weapon weapon in allWeapons)
            {
                if (weapon.category == category)
                {
                    currentWeaponList.Add(weapon);
                }
            }
            RefreshButtons();
        }
    }
    public void PurchaseWeapon()
    {
        if (price <= currencyScript.GetCoin())
        {
            currencyScript.RemoveCoin(price);
            weaponJSONScript.PurchaseWeapon(weaponIndex);
            readAllWeapon();
            Weapon.WeaponCategory category = currentWeaponList[0].category;
            currentWeaponList.Clear();
            foreach (Weapon weapon in allWeapons)
            {
                if (weapon.category == category)
                {
                    currentWeaponList.Add(weapon);
                }
            }
            RefreshButtons();
        }
    }
    private void readAllWeapon()
    {
        allWeapons = weaponJSONScript.GetAllWeaponList();
    }
    private void ClearAllButtonsAnimations()
    {
        ClearCategory();
        ClearWeaponButtons();

    }
    private void ClearCategory()
    {
        foreach (CategoryButtonScript categoryButton in categoriesButtons)
        {
            categoryButton.clearAnimations();
        }
    }
    private void ClearWeaponButtons()
    {
        foreach (SingleWeaponButtonScript weaponButton in weaponButtons)
        {
            weaponButton.clearAnimations();
        }
    }
}
