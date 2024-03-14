using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;

    public void OnMute()
    {
        foreach (var source in audioSources) source.mute = !source.mute;
    }
}
