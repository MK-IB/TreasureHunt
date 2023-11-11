using System;
using UnityEngine;

namespace _TreasureHunt._Scripts.ControllerRelated
{
    public class PlayerController : MonoBehaviour
    {
        [HideInInspector] public bool hasKey;
        public GameObject playerWinCam;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
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
                playerWinCam.SetActive(true);
                _animator.SetTrigger("Dance");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Key"))
            {
                hasKey = true;
                other.GetComponent<Collider>().enabled = false;
            }
            //if(other.)
        }
    }
}