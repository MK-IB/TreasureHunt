using System.Collections;
using _Destruct._Scripts.ControllerRelated;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _TreasureHunt._Scripts.ControllerRelated
{
    public class UIController : MonoBehaviour
    {
        public static UIController instance;
        public TextMeshProUGUI levelNumText, coinNumText;
        public GameObject HUD, winPanel, failPanel, noKeyWarningPanel;

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
            if (newState == GameState.Levelwin)
            {
                HUD.SetActive(false);
                DOVirtual.DelayedCall(2.5f, () => { winPanel.SetActive(true); });
            }

            if (newState == GameState.Levelfail)
            {
                HUD.SetActive(false);

                DOVirtual.DelayedCall(0.75f, () => { failPanel.SetActive(true); });
            }
        }
    }
}