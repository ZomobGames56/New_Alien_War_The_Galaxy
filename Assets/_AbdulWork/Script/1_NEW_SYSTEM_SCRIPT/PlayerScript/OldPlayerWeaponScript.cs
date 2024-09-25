using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class OldPlayerWeaponScript : MonoBehaviour
{
    public event EventHandler updateUI;
    public event EventHandler shootEvent;
    public event EventHandler fireButtonReleased;
    public event EventHandler playerKilled;
    public event EventHandler knifeAttack;

    private GameObject meleeWeapon;
    private bool firstFrame;
    private bool gunDrop;
    private int mainWeaponIndex, backWeaponIndex, meleeIndex, knifeIndex;
    private Weapon mainWeaponClass = null, backWeaponClass = null;
    private List<int> availableIndexs = new List<int>();
    [SerializeField] private Animator animator;
    [SerializeField] private Button switchButton;
    [SerializeField] private WeaponJSONHandler weaponJSONHandler;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private ButtonPressedScript shootButtonScript;
    [SerializeField] private PlayerTargetScript playerTargetScript;

    private void Awake()
    {
        mainWeaponClass = null;
        backWeaponClass = null;
        knifeIndex = weapons.Count - 1;
        for (int i = 0; i < weapons.Count; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
    }
    private void Start()
    {
        List<Weapon> allWeapons = new List<Weapon>();
        allWeapons = weaponJSONHandler.GetAllWeaponList();
        foreach (Weapon weapon in allWeapons)
        {
            if (weapon.isAvailable)
            {
                availableIndexs.Add(weapon.index);
            }
        }
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        if (PlayerPrefs.HasKey("Weapon1Index"))
        {
            mainWeaponIndex = PlayerPrefs.GetInt("Weapon1Index");
            weapons[mainWeaponIndex].SetActive(true);
            animator.SetLayerWeight(mainWeaponIndex, 1);
        }
        else
        {
            mainWeaponIndex = -1;
        }
        if (PlayerPrefs.HasKey("Weapon2Index"))
        {
            switchButton.gameObject.SetActive(true);
            backWeaponIndex = PlayerPrefs.GetInt("Weapon2Index");
        }
        else
        {
            switchButton.gameObject.SetActive(false);
            backWeaponIndex = -1;
        }
        if (mainWeaponIndex < 0 && backWeaponIndex >= 0)
        {
            mainWeaponIndex = backWeaponIndex;
            weapons[mainWeaponIndex].SetActive(true);
            animator.SetLayerWeight(mainWeaponIndex, 1);
            backWeaponIndex = -1;
        }
        switchButton.onClick.AddListener(SwitchButtonPressed);
        UpdateUI();
    }
    private void SwitchButtonPressed()
    {
        switchButton.gameObject.SetActive(false);
        int temp = mainWeaponIndex;
        mainWeaponIndex = backWeaponIndex;
        backWeaponIndex = temp;
        animator.Play("Away", backWeaponIndex);
        firstFrame = true;
        StartCoroutine(AnimationDoneAndFunction(AwayWeapon, "Away", backWeaponIndex));
        //StartCoroutine(WaitForAnimationToEnd("Away"));
    }
    private void AwayWeapon()
    {
        weapons[backWeaponIndex].SetActive(false);
        // Invoke the event once the animation is complete
        animator.SetLayerWeight(backWeaponIndex, 0);
        weapons[mainWeaponIndex].SetActive(true);
        animator.SetLayerWeight(mainWeaponIndex, 1);
        animator.Play("Out", mainWeaponIndex, 0);
        UpdateUI();
        switchButton.gameObject.SetActive(true);
    }
    private void SetMeleeWeapon()
    {
        shootButtonScript.gameObject.SetActive(true);
        print("main weapon is set");
        if (mainWeaponIndex >= 0)
        {
            weapons[mainWeaponIndex].SetActive(false);
        }
        meleeWeapon = weapons[meleeIndex];
        meleeWeapon.SetActive(true);
        animator.SetLayerWeight(meleeIndex, 1);
        animator.SetLayerWeight(mainWeaponIndex, 0);
        animator.Play("Out", meleeIndex);
        meleeWeapon.GetComponent<NewMeleeWeaponScript>().DropMelee += PlayerWeaponScript_DropMelee;
    }
    private void UpdateUI()
    {

        updateUI?.Invoke(this, EventArgs.Empty);
    }
    public void DropMeleeWeapon()
    {
        meleeWeapon.SetActive(false);
        meleeWeapon = null;
        if (mainWeaponIndex >= 0)
        {
            gunDrop = true;
            weapons[mainWeaponIndex].SetActive(true);
            animator.Play("Out", mainWeaponIndex);
            animator.SetLayerWeight(mainWeaponIndex, 1);
        }
        animator.SetLayerWeight(meleeIndex, 0);
    }
    private IEnumerator WaitForAnimationToEnd(string animName)
    {
        while (animator.GetCurrentAnimatorStateInfo(backWeaponIndex).IsName(animName) || firstFrame)
        {
            firstFrame = false;
            yield return null;
        }
        weapons[backWeaponIndex].SetActive(false);
        // Invoke the event once the animation is complete
        animator.SetLayerWeight(backWeaponIndex, 0);
        weapons[mainWeaponIndex].SetActive(true);
        animator.SetLayerWeight(mainWeaponIndex, 1);
        animator.Play("Out", mainWeaponIndex, 0);
        UpdateUI();
        switchButton.gameObject.SetActive(true);
        StopCoroutine("WaitForAnimationToEnd");
    }
    public GameObject GetMainWeapon()
    {
        if (mainWeaponIndex >= 0 && mainWeaponIndex < weapons.Count)
        {
            if (mainWeaponClass == null)
            {
                mainWeaponClass = weaponJSONHandler.GetWeaponClass(mainWeaponIndex);
                weapons[mainWeaponIndex].GetComponent<NewWeaponScript>().SetWeapon(mainWeaponClass);
                NewWeaponScript mainWeaponScript = weapons[mainWeaponIndex].GetComponent<NewWeaponScript>();
                mainWeaponScript.DropWeaponEvent += MainWeaponScript_DropWeaponEvent;
                mainWeaponScript.updateUI += MainWeaponScript_updateUI;
            }
            return weapons[mainWeaponIndex];
        }
        return null;
    }
    private void DropWeapon()
    {
        gunDrop = true;
        switchButton.gameObject.SetActive(false);

        if (backWeaponClass != null)
        {
            int temp = mainWeaponIndex;
            mainWeaponIndex = backWeaponIndex;
            backWeaponIndex = temp;
            animator.SetLayerWeight(backWeaponIndex, 0);
            weapons[backWeaponIndex].SetActive(false);
            weapons[mainWeaponIndex].SetActive(true);
            animator.SetLayerWeight(mainWeaponIndex, 1);
            animator.Play("Out", mainWeaponIndex, 0);
        }
        if (backWeaponClass != null)
        {
            backWeaponClass = null;
            backWeaponIndex = -1;
        }
        else
        {
            if (mainWeaponIndex >= 0)
            {
                weapons[mainWeaponIndex].SetActive(false);
                mainWeaponClass = null;
                mainWeaponIndex = -1;
                animator.gameObject.SetActive(false);
            }
        }
        UpdateUI();
    }
    private void PickWeapon(int index)
    {
        animator.gameObject.SetActive(true);
        if (mainWeaponClass == null)
        {
            mainWeaponIndex = index;
            if (meleeWeapon == null)
            {
                weapons[mainWeaponIndex].SetActive(true);
            }
            animator.SetLayerWeight(mainWeaponIndex, 1);
            animator.Play("Out", mainWeaponIndex, 0);
        }
        else if (backWeaponClass == null && index != mainWeaponIndex)
        {
            backWeaponIndex = index;
            switchButton.gameObject.SetActive(true);
        }
        else if (backWeaponClass == null && index == mainWeaponIndex)
        {
            weapons[mainWeaponIndex].GetComponent<NewWeaponScript>().AddBullets(UnityEngine.Random.Range(5, weapons[mainWeaponIndex].GetComponent<NewWeaponScript>().GetMaxBullet()));
        }
        else
        {
            weapons[mainWeaponIndex].GetComponent<NewWeaponScript>().AddBullets(UnityEngine.Random.Range(5, weapons[mainWeaponIndex].GetComponent<NewWeaponScript>().GetMaxBullet()));
        }
        UpdateUI();
    }
    private void ShootButtonScript_ButtonState(bool obj)
    {
        if (obj == true)
        {
            Shoot();
        }
        else if (obj == false)
        {
            gunDrop = false;
            fireButtonReleased?.Invoke(this, EventArgs.Empty);
        }
    }
    private void MainWeaponScript_updateUI(object sender, EventArgs e)
    {
        UpdateUI();
    }
    private void PlayerWeaponScript_DropMelee(object sender, EventArgs e)
    {
        meleeWeapon.GetComponent<NewMeleeWeaponScript>().DropMelee -= PlayerWeaponScript_DropMelee;
        firstFrame = true;
        animator.Play("Away", meleeIndex);
        StartCoroutine(AnimationDoneAndFunction(DropMeleeWeapon, "Away", meleeIndex));
    }
    private void MainWeaponScript_DropWeaponEvent(object sender, EventArgs e)
    {
        print("Main Weapon Dropped");
        DropWeapon();
    }
    public GameObject GetBackWeapon()
    {
        if (backWeaponIndex >= 0 && backWeaponIndex < weapons.Count)
        {
            if (backWeaponClass == null)
            {
                backWeaponClass = weaponJSONHandler.GetWeaponClass(backWeaponIndex);
                weapons[backWeaponIndex].GetComponent<NewWeaponScript>().SetWeapon(backWeaponClass);
                NewWeaponScript backWeaponScript = weapons[backWeaponIndex].GetComponent<NewWeaponScript>();
                backWeaponScript.DropWeaponEvent += MainWeaponScript_DropWeaponEvent;
                backWeaponScript.updateUI += MainWeaponScript_updateUI;
            }
            return weapons[backWeaponIndex];
        }
        return null;
    }
    public Sprite GetMainWeaponImage()
    {
        if (mainWeaponIndex >= 0 && mainWeaponIndex < weapons.Count)
        {
            return weaponJSONHandler.GetWeaponClass(mainWeaponIndex).image;
        }
        else
        {
            return null;
        }
    }
    public Sprite GetBackWeaponImage()
    {
        if (backWeaponIndex >= 0 && backWeaponIndex < weapons.Count)
        {
            return weaponJSONHandler.GetWeaponClass(backWeaponIndex).image;
        }
        else
        {
            return null;
        }
    }
    public Sprite GetMeleeWeaponImage()
    {
        if (meleeIndex >= 0 && meleeIndex < weapons.Count)
        {
            return weaponJSONHandler.GetWeaponClass(meleeIndex).image;
        }
        else
        {
            return null;
        }
    }
    private void OnEnable()
    {
        shootButtonScript.ButtonState += ShootButtonScript_ButtonState;
    }
    private void OnDisable()
    {
        shootButtonScript.ButtonState -= ShootButtonScript_ButtonState;
        //playerWeaponScript.weaponDroped -= PlayerWeaponScript_weaponDroped;
    }
    private void Shoot()
    {
        if (meleeWeapon != null)
        {
            shootEvent?.Invoke(this, EventArgs.Empty);
        }
        if (!gunDrop && animator.GetCurrentAnimatorStateInfo(mainWeaponIndex).IsName("Run"))
        {
            playerTargetScript.SetWeaponRange(weaponJSONHandler.GetWeaponClass(mainWeaponIndex).range);
            shootEvent?.Invoke(this, EventArgs.Empty);
        }
    }
    public void SetKnife()
    {
        if (mainWeaponIndex >= 0)
        {
            animator.SetLayerWeight(mainWeaponIndex, 0);
            weapons[mainWeaponIndex].SetActive(false);
        }
        animator.SetLayerWeight(knifeIndex, 1);
        weapons[knifeIndex].SetActive(true);
        firstFrame = true;
        animator.Play("Grabbed");
        StartCoroutine(AnimationDoneAndFunction(GrabbedFunction, "Grabbed", knifeIndex));
        //StartCoroutine(AnimationChecker());
    }
    private void GrabbedFunction()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            playerKilled?.Invoke(this, EventArgs.Empty);
        }
    }
    public void AttackKnife()
    {
        firstFrame = true;
        animator.Play("Attack");
        StartCoroutine(AnimationDoneAndFunction(InvokeKnife, "Attack", knifeIndex));
    }
    private void InvokeKnife()
    {
        knifeAttack?.Invoke(this, EventArgs.Empty);
    }
    IEnumerator AnimationDoneAndFunction(Action action, string name, int weaponIndex)
    {
        while (firstFrame || animator.GetCurrentAnimatorStateInfo(weaponIndex).IsName(name))
        {
            firstFrame = false;
            yield return null;
        }
        action();
        StopCoroutine(AnimationDoneAndFunction(action, name, weaponIndex));
    }
    IEnumerator AnimationChecker()
    {
        while (firstFrame || animator.GetCurrentAnimatorStateInfo(0).IsName("Grabbed"))
        {
            firstFrame = false;
            yield return null;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            playerKilled?.Invoke(this, EventArgs.Empty);
        }
        StopCoroutine(AnimationChecker());
    }
    public void SetMainWeaponFromKnife()
    {
        if (mainWeaponIndex >= 0)
        {
            weapons[mainWeaponIndex].SetActive(true);
            animator.SetLayerWeight(mainWeaponIndex, 1);
        }
        weapons[knifeIndex].SetActive(false);
        animator.SetLayerWeight(knifeIndex, 0);
    }
    public int GetMeleeWeaponIndex()
    {
        return meleeIndex;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            int index = UnityEngine.Random.Range(0, availableIndexs.Count - 1);
            index = availableIndexs[index];
            PickWeapon(index);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Melee")
        {
            meleeIndex = other.GetComponent<WeaponIndex>().GetIndex();
            firstFrame = true;
            animator.Play("Away", mainWeaponIndex);
            shootButtonScript.gameObject.SetActive(false);
            StartCoroutine(AnimationDoneAndFunction(SetMeleeWeapon, "Away", mainWeaponIndex));
        }
    }
}