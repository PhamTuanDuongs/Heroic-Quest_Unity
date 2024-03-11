using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    public void UpdateHealth(int currentValue, int maxValue)
    {
        healthBar.maxValue = maxValue;
        healthBar.value = currentValue;
    }
}
