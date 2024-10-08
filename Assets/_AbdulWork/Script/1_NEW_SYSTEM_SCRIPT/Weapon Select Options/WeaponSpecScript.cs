using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSpecScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI specsName, specsAmount;
    [SerializeField] private Slider specsSlider;

    public void SetSpecsValue(string specname , string specamount ,float specslider)
    {
        specsName.text = specname;
        specsAmount.text = specamount;
        specsSlider.value = specslider;
    }
}
