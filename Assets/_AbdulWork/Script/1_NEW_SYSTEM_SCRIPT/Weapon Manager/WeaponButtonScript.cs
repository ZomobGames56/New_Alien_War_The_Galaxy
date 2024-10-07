using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponButtonScript : MonoBehaviour
{
    [SerializeField] private Image gunImage;
    [SerializeField] private TextMeshProUGUI gunName;
    [SerializeField] private GameObject lockImage;

    public void SetWeaponDetails(Sprite weaponImage , string weaponName , bool isAvalaible)
    {
        gunImage.sprite = weaponImage;
        gunName.text = weaponName;
        lockImage.SetActive(!isAvalaible);
    }
}
