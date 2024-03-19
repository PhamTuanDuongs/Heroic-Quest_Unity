using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    public static WaveUI Instance;
    [SerializeField]
    GameObject WaveStart;
    [SerializeField]
    GameObject WaveCompleted;

    [SerializeField]
    GameObject Go;

    public int countWaveComplete = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public IEnumerator StartWave()
    {
        WaveStart.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        WaveStart.SetActive(false);

    }

    public IEnumerator WaveComplete()
    {
        WaveCompleted.SetActive(true);
        countWaveComplete++;    
        yield return new WaitForSeconds(1f);
        WaveCompleted.SetActive(false);

    }

    public IEnumerator WaveCount()
    {
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            Go.transform.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            Go.transform.GetChild(i).gameObject.SetActive(false);
        }

    }
}
