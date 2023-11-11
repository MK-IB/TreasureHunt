using System.Collections;
using _TreasureHunt._Scripts.ControllerRelated;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Destruct._Scripts.ControllerRelated
{
    public class GameController : MonoBehaviour
    {
        public static GameController instance;
        public Image targetBallImage, targetDisplayImg;

        public GameObject confetti, initialFocusCamera;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            MainController.GameStateChanged += GameManager_GameStateChanged;
        }

        private void OnDisable()
        {
            MainController.GameStateChanged -= GameManager_GameStateChanged;
        }

        void GameManager_GameStateChanged(GameState newState, GameState oldState)
        {
            if (newState == GameState.GameStart)
            {
                initialFocusCamera.SetActive(false);
            }
        }

        public void On_NextButtonClicked()
        {
            //if (GAScript.Instance) GAScript.Instance.LevelCompleted(PlayerPrefs.GetInt("levelnumber", 1).ToString(), AttemptsCounter);
            if (PlayerPrefs.GetInt("level", 1) >= SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(UnityEngine.Random.Range(0, SceneManager.sceneCountInBuildSettings - 1));
                PlayerPrefs.SetInt("level", (PlayerPrefs.GetInt("level", 1) + 1));
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.SetInt("level", (PlayerPrefs.GetInt("level", 1) + 1));
                if (PlayerPrefs.GetInt("lastLevelPlayed") < SceneManager.GetActiveScene().buildIndex)
                {
                    PlayerPrefs.SetInt("lastLevelPlayed", SceneManager.GetActiveScene().buildIndex);
                }
            }

            PlayerPrefs.SetInt("levelnumber", PlayerPrefs.GetInt("levelnumber", 1) + 1);
            SoundsController.instance.PlaySound(SoundsController.instance.UiClick);
            //Vibration.Vibrate(27);
            //ISManager.instance.ShowInterstitialAds();
        }

        public void On_RetryButtonClicked()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            SoundsController.instance.PlaySound(SoundsController.instance.UiClick);
            //Vibration.Vibrate(27);
            //ISManager.instance.ShowInterstitialAds();
            //print("Interstitial : Retry Lv");
        }

        public IEnumerator DumpUnused(GameObject gameObject, float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(false);
        }
    }
}