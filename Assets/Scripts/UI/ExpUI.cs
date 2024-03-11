using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [SerializeField] private Slider expBar;

    public void UpdateExp(int currentValue, int maxValue)
    {
        expBar.maxValue = maxValue;
        expBar.value = currentValue;
    }
}
