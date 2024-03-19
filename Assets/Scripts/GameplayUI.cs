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
        [SerializeField] private GameObject GameoverPauseObj;
        [SerializeField] private TextMeshProUGUI coinText;
        [SerializeField] private TextMeshProUGUI yourCoin;
        [SerializeField] private TextMeshProUGUI yourWaveComplete;


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
            yourCoin.text = GameplayManager.Instance.coin.ToString();
            yourWaveComplete.text = "Complete wave: " +  WaveUI.Instance.countWaveComplete.ToString();
        }

        public void Pause()
        {
            Time.timeScale = 0;
            GameoverPauseObj.SetActive(true);
        }

        public void UnPause()
        {
            Time.timeScale = 1;
            GameoverPauseObj.SetActive(false);
        }
    }
}

