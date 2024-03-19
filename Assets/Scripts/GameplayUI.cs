using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeroicQuest
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private AudioSource[] audioSources;
        [SerializeField] private GameObject GameoverObj;
        [SerializeField] private TextMeshProUGUI coinText;


        private void Awake()
        {

        }
        public void OnMute()
        {
            foreach (var source in audioSources) source.mute = !source.mute;
        }

        public void OnGoHome()
        {
            SceneManager.LoadScene("menu", LoadSceneMode.Single);
        }

        public void GameOverMenu()
        {
            GameoverObj.SetActive(true);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(1);
        }

        public void UpdateMoney()
        {
            GameplayManager.Instance.coin++;
            coinText.text = GameplayManager.Instance.coin.ToString("D3");
        }
    }
}

