using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueControl : MonoBehaviour
{
    Slider healthSlider;
    TextMeshProUGUI valueText;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        valueText = GetComponentInChildren<TextMeshProUGUI>();
        valueText.text = healthSlider.value.ToString() + "/" + healthSlider.maxValue.ToString();
    }

    public void UpdateHealthText()
    {
        valueText.text = healthSlider.value.ToString() + "/" + healthSlider.maxValue.ToString();
    }
}
