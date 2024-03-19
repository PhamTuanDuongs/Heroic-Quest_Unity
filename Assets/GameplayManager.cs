using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public static GameplayManager Instance;
    public int coin = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddCoin()
    {
        coin++;
    }
}
