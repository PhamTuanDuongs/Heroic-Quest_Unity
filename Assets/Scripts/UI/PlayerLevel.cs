using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [SerializeField] public int currentLevel;
    [SerializeField] private float currentExp;
    [SerializeField] private float currentMaxExp;
    [SerializeField] private float baseExp;

    private ExpUI expUI;
    private void Start()
    {
        expUI = GetComponent<ExpUI>();

        currentLevel = 1;
        currentExp = 0;
        currentMaxExp = baseExp;
        expUI.UpdateExp((int)currentExp, (int)currentMaxExp);
    }

    public void OnGetExp(object sender, int  amount)
    {
        currentExp += amount;
        if(currentExp >= currentMaxExp)
        {
            currentLevel++;
            currentExp -= currentMaxExp;
            currentMaxExp = UpdateMaxExp();
            OnLevelUp();
        }

        expUI.UpdateExp((int)currentExp, (int)currentMaxExp);
    }

    private void OnLevelUp()
    {

    }

    private float UpdateMaxExp()
    {
        return baseExp * currentMaxExp / .2f;
    }
}
