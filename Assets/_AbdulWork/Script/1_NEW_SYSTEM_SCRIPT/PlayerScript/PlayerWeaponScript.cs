using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerWeaponScript : MonoBehaviour
{
    public event EventHandler updateUI;
    public event EventHandler shootEvent;
    public event EventHandler fireButtonReleased;
    public event EventHandler playerKilled;
    public event EventHandler knifeAttack;
    public event EventHandler FreedPlayer;

    private GameObject meleeWeapon;
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
    //No Change
    private void Awake()
    {
        animator.gameObject.SetActive(false);
        mainWeaponClass = null;
        backWeaponClass = null;
        knifeIndex = weapons.Count - 1;
        mainWeaponIndex = -1;
        backWeaponIndex = -1;
        switchButton.gameObject.SetActive(false);
        for (int i = 0; i < weapons.Count; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
    }
    //Recheck Comments and their working
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
            //Implement New Pick Up Funtionality
            PickWeapon(mainWeaponIndex);
        }
        if (PlayerPrefs.HasKey("Weapon2Index"))
        {
            backWeaponIndex = PlayerPrefs.GetInt("Weapon2Index");
            //Implment New Pick up Script
            PickWeapon(backWeaponIndex);
        }
        switchButton.onClick.AddListener(SwitchButtonPressed);
        UpdateUI();
    }
    //No Change
    private void SwitchButtonPressed()
    {
        switchButton.gameObject.SetActive(false);
        int temp = mainWeaponIndex;
        mainWeaponIndex = backWeaponIndex;
        backWeaponIndex = temp;
        animator.Play("Away", backWeaponIndex);
        StartCoroutine(AnimationDoneAndFunction(SwitchWeapon, "Away", backWeaponIndex));
        //StartCoroutine(WaitForAnimationToEnd("Away"));
    }
    //No Change
    private void SwitchWeapon()
    {
        // Invoke the event once the animation is complete
        weapons[backWeaponIndex].SetActive(false);
        animator.SetLayerWeight(backWeaponIndex, 0);
        weapons[mainWeaponIndex].SetActive(true);
        animator.SetLayerWeight(mainWeaponIndex, 1);
        animator.Play("Out", mainWeaponIndex, 0);
        UpdateUI();
        switchButton.gameObject.SetActive(true);
    }
    //Recheck all good for now
    private void PickMeleeWeapon()
    {
        shootButtonScript.gameObject.SetActive(true);
        meleeWeapon = weapons[meleeIndex];
        DisableMainWeapon(meleeIndex, "Out");
        meleeWeapon.GetComponent<NewMeleeWeaponScript>().DropMelee += PlayerWeaponScript_DropMelee;
    }
    //Recheck all good for now
    public void DropMeleeWeapon()
    {
        print(meleeWeapon.name);
        meleeWeapon.SetActive(false);
        meleeWeapon = null;
        if(mainWeaponIndex>=0)
        {
            gunDrop = true;
            weapons[mainWeaponIndex].SetActive(true);
            animator.Play("Out" , mainWeaponIndex);
            animator.SetLayerWeight(mainWeaponIndex, 1);
        }
        animator.SetLayerWeight(meleeIndex, 0);
    }
    //No Change
    private void UpdateUI()
    {
        updateUI?.Invoke(this, EventArgs.Empty);
    }
/*    public GameObject GetMainWeapon()
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
    }*/
    //Recheck all good for now
    private void PickWeapon(int index)
    {
        animator.gameObject.SetActive(true);
        if (mainWeaponClass == null)
        {
            mainWeaponIndex = index;
            if(meleeWeapon == null)
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
    //Recheck all good for now
    private void DropWeapon()
    {
        NewWeaponScript mainWeaponScript = weapons[mainWeaponIndex].GetComponent<NewWeaponScript>();
        mainWeaponScript.DropWeaponEvent -= MainWeaponScript_DropWeaponEvent;
        mainWeaponScript.updateUI -= MainWeaponScript_updateUI;
        gunDrop = true;
        switchButton.gameObject.SetActive(false);
        if (backWeaponClass != null)
        {
            animator.SetLayerWeight(mainWeaponIndex, 0);
            weapons[mainWeaponIndex].SetActive(false);
            mainWeaponIndex = backWeaponIndex;
            backWeaponIndex = -1;
            backWeaponClass = null;
            weapons[mainWeaponIndex].SetActive(true);
            animator.SetLayerWeight(mainWeaponIndex, 1);
            animator.Play("Out", mainWeaponIndex, 0);
        }
        else
        {
            weapons[mainWeaponIndex].SetActive(false);
            mainWeaponClass = null;
            mainWeaponIndex = -1;
            animator.gameObject.SetActive(false);
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
    //check code is nono essential
    private void MainWeaponScript_updateUI(object sender, EventArgs e)
    {
        UpdateUI();
    }
    //Recheck all good for now
    private void PlayerWeaponScript_DropMelee(object sender, EventArgs e)
    {
        meleeWeapon.GetComponent<NewMeleeWeaponScript>().DropMelee -= PlayerWeaponScript_DropMelee;
        animator.Play("Away" , meleeIndex);
        StartCoroutine(AnimationDoneAndFunction(DropMeleeWeapon, "Away", meleeIndex));
    }
    private void MainWeaponScript_DropWeaponEvent(object sender, EventArgs e)
    {
        animator.Play("Away", mainWeaponIndex);
        StartCoroutine(AnimationDoneAndFunction(DropWeapon , "Away" , mainWeaponIndex));
    }
    //Recheck Required
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
    //Recheck required
    public GameObject GetBackWeapon()
    {
        if (backWeaponIndex >= 0 && backWeaponIndex < weapons.Count)
        {
            if (backWeaponClass == null)
            {
                backWeaponClass = weaponJSONHandler.GetWeaponClass(backWeaponIndex);
                NewWeaponScript backWeaponScript = weapons[backWeaponIndex].GetComponent<NewWeaponScript>();
                backWeaponScript.SetWeapon(backWeaponClass);
                backWeaponScript.updateUI += MainWeaponScript_updateUI;
                backWeaponScript.DropWeaponEvent += MainWeaponScript_DropWeaponEvent;
            }
            return weapons[backWeaponIndex];
        }
        return null;
    }
    //No Change
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
    //No Change
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
    //No Change
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
    //No Change
    private void OnDisable()
    {
        shootButtonScript.ButtonState -= ShootButtonScript_ButtonState;
    }
    //No Change
    private void Shoot()
    {
        if (!gunDrop && animator.GetCurrentAnimatorStateInfo(meleeIndex).IsName("Run") && meleeWeapon != null)
        {
            shootEvent?.Invoke(this, EventArgs.Empty);
        }
        else if (!gunDrop && animator.GetCurrentAnimatorStateInfo(mainWeaponIndex).IsName("Run"))
        {
            playerTargetScript.SetWeaponRange(weaponJSONHandler.GetWeaponClass(mainWeaponIndex).range);
            shootEvent?.Invoke(this, EventArgs.Empty);
        }
    }
    //NO Change
    public void SetKnife()
    {
        if (meleeWeapon == null)
        {
            DisableMainWeapon(knifeIndex, "Grabbed");
        }
        else
        {
            meleeWeapon.SetActive(false);
            animator.SetLayerWeight(meleeIndex, 0);
            animator.SetLayerWeight(knifeIndex, 1);
            weapons[knifeIndex].SetActive(true);
            animator.Play("Grabbed", knifeIndex);
        }
            StartCoroutine(AnimationDoneAndFunction(GrabbedFunction, "Grabbed", knifeIndex));
    }
    //Recheck
    private void DisableMainWeapon(int weaponIndex , string animName)
    {
        switchButton.gameObject.SetActive(false);
        if (mainWeaponIndex >= 0)
        {
            animator.SetLayerWeight(mainWeaponIndex, 0);
            weapons[mainWeaponIndex].SetActive(false);
        }
        animator.SetLayerWeight(weaponIndex, 1);
        weapons[weaponIndex].SetActive(true);
        animator.Play(animName , weaponIndex);

    }
    //Reimplment Not working Properly
    private void GrabbedFunction()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            playerKilled?.Invoke(this, EventArgs.Empty);
        }
    }
    //No Change
    public void AttackKnife()
    {
        animator.Play("Attack" , knifeIndex);
        knifeAttack?.Invoke(this, EventArgs.Empty);
        StartCoroutine(AnimationDoneAndFunction(KnifeAway, "Attack", knifeIndex));
    }
    //Remove this code and join the player attacking and reseting pose in one and same for alien
    private void KnifeAway()
    {
        print("Remove this code and merge the attack and reseting pose for alien and player");
        animator.Play("Away" , knifeIndex);
        StartCoroutine(AnimationDoneAndFunction(KnifeAttackCompleted, "Away", knifeIndex));
    }
    //NoChange
    private void KnifeAttackCompleted()
    {
        FreedPlayer?.Invoke(this, EventArgs.Empty);
    }
    //No change
    IEnumerator AnimationDoneAndFunction(Action action, string name, int weaponIndex)
    {
        do
        {
            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(weaponIndex).IsName(name));
        action();
        StopCoroutine(AnimationDoneAndFunction(action, name, weaponIndex));
    }
    //No change
    public void SetMainWeaponFromKnife()
    {
        if (meleeWeapon == null)
        {
            if (mainWeaponIndex >= 0)
            {
                weapons[mainWeaponIndex].SetActive(true);
                animator.SetLayerWeight(mainWeaponIndex, 1);
            }
        }
        else
        {
            weapons[meleeIndex].SetActive(true);
            animator.SetLayerWeight(meleeIndex, 1);
        }
        weapons[knifeIndex].SetActive(false);
        animator.SetLayerWeight(knifeIndex, 0);
        animator.Play("Out", mainWeaponIndex);
    }
    //NO Change
    public int GetMeleeWeaponIndex()
    {
        return meleeIndex;
    }
    //No Change
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
            if(mainWeaponIndex >= 0)
            {
                animator.Play("Away", mainWeaponIndex);
                shootButtonScript.gameObject.SetActive(false);
                StartCoroutine(AnimationDoneAndFunction(PickMeleeWeapon, "Away", mainWeaponIndex));
            }
            else
            {
                print("Melee Else Function is Called");
                shootButtonScript.gameObject.SetActive(false);
                PickMeleeWeapon();
            }

        }
    }
}