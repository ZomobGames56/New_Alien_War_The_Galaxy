using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Linq;
public class WeaponSelectScript : MonoBehaviour
{
    //OLD METHOD
    /*    private Image gunImage;
        private Weapon MainWeapon, BackUpWeapon;
        private TextMeshProUGUI gunName, gunLevel;
        private List<Weapon> allWeapons = new List<Weapon>();
        private List<GameObject> weaponButtons = new List<GameObject>();

        //Use a Prefab to Instantiate
        [SerializeField] private GameObject weaponSelectButton;
        [SerializeField] private GameObject weaponButtonHolder;
        [SerializeField] private WeaponJSONHandler weaponJSONHandler;
        [SerializeField] private WeaponPanel mainWeaponPanel , backWeaponPanel;

        private void Start()
        {
            GetandSetButtons();
        }
        public void GetandSetButtons()
        {
            foreach(Transform t in weaponButtonHolder.transform)
            {
                Destroy(t.gameObject);
            }
            allWeapons = weaponJSONHandler.GetAllWeaponList();
            foreach(Weapon weapon in allWeapons)
            {
                if(weapon.isAvailable && weapon.category != Weapon.WeaponCategory.Melee)
                {

                    GameObject temp = Instantiate(weaponSelectButton, weaponButtonHolder.transform);
                    weaponButtons.Add(temp);
                    gunImage = temp.transform.GetChild(0).GetComponentInChildren<Image>();
                    List<TextMeshProUGUI> tests = temp.GetComponentsInChildren<TextMeshProUGUI>().ToList();
                    gunName = tests[0];
                    gunLevel = tests[1];
                    gunName.text = weapon.name;
                    gunLevel.text = "LVL" + weapon.level;
                    gunImage.sprite = weapon.image;
                    //temp.GetComponent<Button>().onClick.AddListener(() => {SetWeapon(weapon);});
                    RectTransform tempRect = weaponButtonHolder.GetComponent<RectTransform>();
                    tempRect.sizeDelta = tempRect.sizeDelta + new Vector2(600,0);
                }
            }
        }
        private void SetWeapon(Weapon currentWeapon)
        {
            if(MainWeapon == null && BackUpWeapon == null)
            {
                MainWeapon = currentWeapon;
            }
            else if(MainWeapon == null && BackUpWeapon != currentWeapon)
            {
                MainWeapon = currentWeapon;
            }
            else if(MainWeapon != currentWeapon && BackUpWeapon ==null)
            {
                BackUpWeapon = currentWeapon;
            }else if(MainWeapon == currentWeapon)
            {
                MainWeapon = null;
            }else if(BackUpWeapon == currentWeapon)
            {
                BackUpWeapon = null;
            }
            WeaponPanel();
        }
        private void WeaponPanel()
        {
            mainWeaponPanel.SelectWeapon(MainWeapon);
            backWeaponPanel.SelectWeapon(BackUpWeapon);
        }
        public void GameStart()
        {
            PlayerPrefs.DeleteKey("Weapon1Index");
            PlayerPrefs.DeleteKey("Weapon2Index");
            if(MainWeapon != null)
            {
                PlayerPrefs.SetInt("Weapon1Index", MainWeapon.index);
            }
            if(BackUpWeapon != null)
            {
                PlayerPrefs.SetInt("Weapon2Index", BackUpWeapon.index);
            }
        }*/

    //NEW METHOD
    private int weapon1Index = -1, weapon2Index = -1;
    private bool weapon1Available, weapon2Available;
    [SerializeField] private Scrollbar scrollbar;
    [SerializeField] private GameObject playButton, selectButton;
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    [SerializeField] private WeaponPanel mainWeaponPanel, backWeaponPanel;
    [SerializeField] private GameObject weaponButtonPrefab , weaponHolderPrefab;
    [SerializeField] private List<WeaponSelectButton> weaponSelectButtons = new List<WeaponSelectButton>();
    [SerializeField] private List<Weapon> allWeapons = new List<Weapon>(), availableWeapons = new List<Weapon>();
    public void Start()
    {
        readALLWeapons();
        SwitchButton(selectButton);
        foreach (Weapon weapon in allWeapons) 
        {
            if(weapon.isAvailable && weapon.isUnlocked)
            {
                availableWeapons.Add(weapon);
            }
        }
        for(int i = 0; i<availableWeapons.Count; i++)
        {
            GameObject temp = Instantiate(weaponButtonPrefab, weaponHolderPrefab.transform);
            weaponSelectButtons.Add(temp.GetComponent<WeaponSelectButton>());
        }
        for(int i = 0; i < availableWeapons.Count; i++)
        {
            weaponSelectButtons[i].SetWeaponSelectAndWeapon(this , availableWeapons[i]);
            weaponSelectButtons[i].clearButtonAnimations += WeaponSelectScript_clearButtonAnimations;
        }
        Vector2 sizeDelta = new Vector2(availableWeapons.Count * 500, weaponHolderPrefab.GetComponent<RectTransform>().sizeDelta.y);
        weaponHolderPrefab.GetComponent<RectTransform>().sizeDelta = sizeDelta;
    }

    private void WeaponSelectScript_clearButtonAnimations(object sender, System.EventArgs e)
    {
        foreach(WeaponSelectButton weaponButton in weaponSelectButtons)
        {
            weaponButton.ClearAnimation();
        }
    }

    public bool HasSpace()
    {
        if(weapon1Index == -1)
        {
            return true;
        }
        else if(weapon2Index == -1)
        {
            return true;
        }
        return false;
    }
    private void readALLWeapons()
    {
        allWeapons = weaponJSONHandler.GetAllWeaponList();
    }
    public void SelectButtonPressed()
    {
        foreach(WeaponSelectButton weaponButton in weaponSelectButtons)
        {
            weaponButton.SelectButton();
        }
    }
    private void SwitchButton(GameObject button)
    {
        playButton.SetActive(false);
        selectButton.SetActive(false);

        button.SetActive(true);
    }
    public string SetWeapon(Weapon weapon)
    {
        if(weapon1Index == -1)
        {
            weapon1Index = weapon.index;
            mainWeaponPanel.SelectWeapon(weapon);
            return "MAIN";
        }
        else if(weapon2Index == -1)
        {
            weapon2Index = weapon.index;
            backWeaponPanel.SelectWeapon(weapon);
            SwitchButton(playButton);
            return "Back Up";
        }
        return null;
    }
    public void RemoveSelectedWeapon(Weapon weapon)
    {
        if(weapon1Index == weapon.index)
        {
            weapon1Index = -1;
            mainWeaponPanel.SelectWeapon(null);
        }
        else if(weapon2Index == weapon.index)
        {
            weapon2Index = -1;
            backWeaponPanel.SelectWeapon(null);
        }
        SwitchButton(selectButton);
    }
    public void OnDisable()
    {
        scrollbar.value = 0;
        weapon1Index = -1;
        weapon2Index = -1;
        mainWeaponPanel.SelectWeapon(null);
        backWeaponPanel.SelectWeapon(null);
        SwitchButton(selectButton);
        foreach(WeaponSelectButton weaponButtonScript in weaponSelectButtons)
        {
            weaponButtonScript.ResetButton();
        }
    }
    public void RemoveLastWeapon()
    {
        if(weapon2Available)
        {
            weapon2Index = -1;
            backWeaponPanel.SelectWeapon(null);
        }
        else
        {
            weapon1Index = -1;
            mainWeaponPanel.SelectWeapon(null);
        }
    }
}
