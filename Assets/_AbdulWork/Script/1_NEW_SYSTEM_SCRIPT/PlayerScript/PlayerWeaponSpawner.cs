using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerWeaponSpawner : MonoBehaviour
{

    private int mainWeaponIndex, backWeaponIndex;
    private Weapon mainWeaponClass, backWeaponClass;
    public List<GameObject> weapons = new List<GameObject>();


    [SerializeField] private Button switchButton;
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    public void Start()
    {
        if(PlayerPrefs.HasKey("Weapon1Index"))
        {
            mainWeaponIndex = PlayerPrefs.GetInt("Weapon1Index");
            weapons[mainWeaponIndex].SetActive(true);
        }
        else
        {
            mainWeaponIndex=-1;
        }
        if(PlayerPrefs.HasKey("Weapon2Index"))
        {
            backWeaponIndex = PlayerPrefs.GetInt("Weapon2Index");
        }
        else
        {
            mainWeaponIndex = -1;
        }
        switchButton.onClick.AddListener(SwitchButtonPressed);
    }
    private void OnDisable()
    {
        switchButton.onClick.RemoveListener(SwitchButtonPressed);
    }
    private void SwitchButtonPressed()
    {
        Debug.Log("SwitchButton pressed");
        Debug.Log(mainWeaponIndex);
        weapons[mainWeaponIndex].SetActive(false);
        int temp = mainWeaponIndex;
        mainWeaponIndex = backWeaponIndex;
        backWeaponIndex = temp;
        weapons[mainWeaponIndex].SetActive(true);
        Debug.Log(mainWeaponIndex);
        UpdateUI();
    }
    private void UpdateUI()
    {
        mainWeaponClass = weaponJSONHandler.GetWeaponClass(mainWeaponIndex);
        backWeaponClass = weaponJSONHandler.GetWeaponClass(backWeaponIndex);
    }
    public Weapon GetMainWeaponClass()
    {
        return mainWeaponClass;
    }
    public Weapon GetBackWeaponClass()
    {
        return backWeaponClass;
    }
    public void DropWeapon()
    {

    }
    public void PickWeapon()
    {

    }


    //private Animator animator;
    //private List<Weapon> weapons = new List<Weapon>();
    //[SerializeField] private WeaponJSONHandler weaponJSONHandler;
    //[SerializeField] private Button switchButton, meleeButton;
    //[SerializeField] private PlayerMovementScirpt playerMovementScript;
    //[SerializeField] private PlayerTargetScript targetScript;
    //[SerializeField] private GameObject weaponHolder , Knife;
    //private GameObject mainWeapon, backWeapon;
    //public List<GameObject> Weapon;
    // Start is called before the first frame update
    /*void Awake()
    {
        playerMovementScript = GetComponent<PlayerMovementScirpt>();
        animator = GetComponent<Animator>();
        for (int i = 1; i <= Weapon.Count; i++)
        {
            if (Weapon[i - 1].GetComponent<NewWeaponScript>().GetWeaponSO().Index == PlayerPrefs.GetInt("Weapon1Index"))
            {
                mainWeapon = Instantiate(Weapon[i - 1]);
                EnabledWeapon(mainWeapon);
                animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetWeaponSO().Index, 1);
            }
            if (Weapon[i - 1].GetComponent<NewWeaponScript>().GetWeaponSO().Index == PlayerPrefs.GetInt("Weapon2Index"))
            {
                backWeapon = Instantiate(Weapon[i - 1]);
                EnabledWeapon(backWeapon);

            }
        }
    }*/
    /*private void Start()
    {
        updateUI?.Invoke(this, EventArgs.Empty);
        weapons = weaponJSONHandler.GetAllWeaponList();
        if (PlayerPrefs.HasKey("Weapon1Index"))
        {
            int index = PlayerPrefs.GetInt("Weapon1Index");
            foreach (Weapon weapon in weapons)
            {
                if (weapon.index == index)
                {
                    mainWeapon = Instantiate(Weapon[index]);
                    EnabledWeapon(mainWeapon);
                    animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetIndex(), 1);
                }
            }
        }
        if (PlayerPrefs.HasKey("Weapon2Index"))
        {
            int index = PlayerPrefs.GetInt("Weapon2Index");
            foreach (Weapon weapon in weapons)
            {
                if (weapon.index == index)
                {
                    backWeapon = Instantiate(Weapon[index]);
                    EnabledWeapon(backWeapon);
                    backWeapon.SetActive(false);
                }
            }
        }

    }*/
    /*private void OnEnable()
    {
        meleeButton.onClick.AddListener(Melee);
        switchButton.onClick.AddListener(SwitchWeapon);
        playerMovementScript.playerStateChanged += PlayerMovementScript_onStateChanged;
    }*/
    /*private void PlayerMovementScript_onStateChanged(PlayerMovementScirpt.PlayerState obj)
    {
        if(obj == PlayerMovementScirpt.PlayerState.Moving)
        {
            if (mainWeapon != null)
            {
                mainWeapon.SetActive(true);
                animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetIndex(), 1);
            }
            Knife.SetActive(false);
        }
        else if(obj == PlayerMovementScirpt.PlayerState.Grabbed)
        {
            if (mainWeapon != null)
            {
                mainWeapon.SetActive(false);
                animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetIndex(), 0);
            }
            Knife.SetActive(true);
        }
    }*/
    /*private void OnDisable()
    {
        meleeButton.onClick.RemoveListener(Melee);
        switchButton.onClick.RemoveListener(SwitchWeapon);
        playerMovementScript.playerStateChanged -= PlayerMovementScript_onStateChanged;
    }*/
    /*private void Update()
    {
        if(mainWeapon!= null)
        {
            if (mainWeapon.GetComponent<NewWeaponScript>().GetCurrentBullet() == 0 && mainWeapon.GetComponent<NewWeaponScript>().GetTotalBullet() == 0)
            {
                DropWeapon();
            }
        }
    }*/
    /*private void SwitchWeapon()
    {
        animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetIndex(), 0);
        mainWeapon.SetActive(false);
        GameObject temp = mainWeapon;
        mainWeapon = backWeapon;
        backWeapon = temp;
        mainWeapon.SetActive(true);
        animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetIndex(), 1);
        updateUI?.Invoke(this , EventArgs.Empty);
    }*/
    /*private void DropWeapon()
    {
        animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetIndex(), 0);
        switchButton.gameObject.SetActive(false);
        if (backWeapon != null)
        {
            Destroy(mainWeapon);
            mainWeapon = backWeapon;
            mainWeapon.SetActive(true);
            animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetIndex(), 1);
            backWeapon = null;
        }
        else
        {
            Destroy(mainWeapon);
            mainWeapon = null;
        }
        updateUI?.Invoke(this, EventArgs.Empty);
        weaponDroped?.Invoke(this, EventArgs.Empty);
    }*/
    /*private void PickWeapon(GameObject weapon)
    {
        if (mainWeapon == null)
        {
            mainWeapon = weapon;
            weapon.GetComponent<Collider>().enabled = false;
            EnabledWeapon(weapon);
            animator.SetLayerWeight(mainWeapon.GetComponent<NewWeaponScript>().GetIndex(), 1);
        }
        else if (backWeapon == null)
        {
            if (mainWeapon.GetComponent<NewWeaponScript>().GetIndex() == weapon.GetComponent<NewWeaponScript>().GetIndex())
            {
                Destroy(weapon);
                mainWeapon.GetComponent<NewWeaponScript>().AddBullets(UnityEngine.Random.RandomRange(1, mainWeapon.GetComponent<NewWeaponScript>().GetMaxBullet()));
            }
            else
            {
                backWeapon = weapon;
                EnabledWeapon(weapon);
                switchButton.gameObject.SetActive(true);
                backWeapon.SetActive(false);
            }
        }
        else
        {
            Destroy(weapon);
            mainWeapon.GetComponent<NewWeaponScript>().AddBullets(UnityEngine.Random.RandomRange(1, mainWeapon.GetComponent<NewWeaponScript>().GetMaxBullet()));
        }
        updateUI?.Invoke(this, EventArgs.Empty);
    }*/
    /*private void EnabledWeapon(GameObject weapon)
    {
        weapon.transform.parent = weaponHolder.transform;
        weapon.GetComponent<NewWeaponScript>().enabled = true;
        weapon.GetComponent<NewWeaponScript>().SetTarget(targetScript);
        weapon.transform.GetChild(1).gameObject.SetActive(false);
        weapon.transform.GetChild(0).gameObject.SetActive(true);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }*/
    /*public GameObject GetMainWeapon()
    {
        return mainWeapon;
    }
    public GameObject GetBackWeapon()
    {
        return backWeapon;
    }*/
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            PickWeapon(other.gameObject);
        }
    }
    private void Melee()
    {
        meleeHit?.Invoke(this , EventArgs.Empty);
    }*/
}
