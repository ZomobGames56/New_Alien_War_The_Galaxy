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
    private Image gunImage;
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
                temp.GetComponent<Button>().onClick.AddListener(() => {SetWeapon(weapon);});
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
    }
}
