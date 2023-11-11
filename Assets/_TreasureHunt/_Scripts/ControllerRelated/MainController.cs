using System;
using System.Collections;
using UnityEngine;

namespace _TreasureHunt._Scripts.ControllerRelated
{
    public enum GameState
    {
        None,
        Create,
        GameStart,
        Levelwin,
        Levelfail
    }

    public class MainController : MonoBehaviour
    {
        public static MainController instance;

        [SerializeField] private GameState _gameState;
        public static event Action<GameState, GameState> GameStateChanged;
        public static event Action BallPlacedAction;

        public GameState GameState
        {
            get => _gameState;
            private set
            {
                if (value != _gameState)
                {
                    GameState oldState = _gameState;
                    _gameState = value;
                    if (GameStateChanged != null)
                        GameStateChanged(_gameState, oldState);
                }
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            CreateGame();
        }

        void CreateGame()
        {
            GameState = GameState.Create;
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForSeconds(2);
            GameState = GameState.GameStart;
        }

        public void SetActionType(GameState _curState)
        {
            GameState = _curState;
        }

        public void Start_BallPlacedAction()
        {
            BallPlacedAction?.Invoke();
        }
    }
}