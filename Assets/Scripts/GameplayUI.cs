using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeroicQuest
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audioSources;

        public void OnMute()
        {
            foreach (var source in audioSources) source.mute = !source.mute;
        }

        public void OnGoHome()
        {
            SceneManager.LoadScene("menu", LoadSceneMode.Single);
        }
    }
}

