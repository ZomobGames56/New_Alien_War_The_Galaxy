using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class WeaponUIScript : MonoBehaviour
{
    private GameObject mainWeapon, backWeapon;
    private NewWeaponScript mainWeaponScript, backWeaponScript;


    [SerializeField] private GameObject[] bullets;
    [SerializeField] private PlayerWeaponScript playerWeaponScript;
    [SerializeField] private Image mainWeaponImage, backWeaponImage;
    [SerializeField] private GameObject completeWeaponPanel, backWeaponPanel;
    [SerializeField] private TextMeshProUGUI mainWeaponBullets, backWeaponBullets;

    //No Change required
    private void Awake()
    {
        playerWeaponScript.shootEvent += PlayerWeaponScript_shootEvent;
        playerWeaponScript.updateUI += PlayerWeaponScript_updateUI;
    }
    //No Change Required
    private void OnEnable()
    {
        playerWeaponScript.shootEvent += PlayerWeaponScript_shootEvent;
        playerWeaponScript.updateUI += PlayerWeaponScript_updateUI;
    }

    //No Change Required
    private void OnDisable()
    {
        playerWeaponScript.shootEvent -= PlayerWeaponScript_shootEvent;
        playerWeaponScript.updateUI -= PlayerWeaponScript_updateUI;
    }

    //Under Review
    private void PlayerWeaponScript_shootEvent(object sender, System.EventArgs e)
    {
        DisableBullet();
        UpdateUI();
    }

    //Under Review
    private void PlayerWeaponScript_updateUI(object sender, System.EventArgs e)
    {
        DisableBullet();
        if (playerWeaponScript.GetBackWeapon() != null)
        {
            backWeaponPanel.SetActive(true);
            backWeapon = playerWeaponScript.GetBackWeapon();
            backWeaponScript = backWeapon.GetComponent<NewWeaponScript>();
            mainWeapon = playerWeaponScript.GetMainWeapon();
            mainWeaponScript = mainWeapon.GetComponent<NewWeaponScript>();
        }
        else if (playerWeaponScript.GetMainWeapon() != null)
        {
            backWeaponPanel.SetActive(false);
            mainWeapon = playerWeaponScript.GetMainWeapon();
            mainWeaponScript = mainWeapon.GetComponent<NewWeaponScript>();
            completeWeaponPanel.SetActive(true);
            backWeapon = null;
        }
        else
        {
            completeWeaponPanel.SetActive(false);
        }
        UpdateUI();
    }
    //Under Review
    private void UpdateUI()
    {
        if (backWeapon != null)
        {
            for (int i = 0; i < mainWeaponScript.GetCurrentBullet(); i++)
            {
                bullets[i].SetActive(true);
            }
            mainWeaponImage.sprite = playerWeaponScript.GetMainWeaponImage();
            backWeaponImage.sprite = playerWeaponScript.GetBackWeaponImage();
            mainWeaponBullets.text = mainWeaponScript.GetTotalBullet().ToString();
            backWeaponBullets.text = backWeaponScript.GetTotalBullet().ToString();
        }
        else if (mainWeapon != null)
        {
            for (int i = 0; i < mainWeaponScript.GetCurrentBullet(); i++)
            {
                bullets[i].SetActive(true);
            }
            mainWeaponImage.sprite = playerWeaponScript.GetMainWeaponImage();
            mainWeaponBullets.text = mainWeaponScript.GetTotalBullet().ToString();
        }
    }
    //NO change requiered
    private void DisableBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].SetActive(false);
        }
    }
}
