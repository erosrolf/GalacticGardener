using System;
using UnityEditor.Build.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Architecture
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState
        {
            Menu = 1,
            Playing = 2,
            GameOver = 3,
        }

        [SerializeField] private GameObject _menuInterface;
        [SerializeField] private GameObject _playerInterface;
        [SerializeField] private GameObject _gameOverInterface;

        public GameState CurrentState { get; private set; }
        public static GameManager Instance { get; private set; }

        public delegate void GameStateDelegate();
        public static event GameStateDelegate StartGameEvent;
        public static event GameStateDelegate EndGameEvent;


        void OnEnable()
        {
            CollideInspector.CollideWithEnemy += EndGame;
        }

        void OnDisable()
        {
            CollideInspector.CollideWithEnemy -= EndGame;
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        void Start()
        {
            CurrentState = GameState.Menu;
            AudioManager.Instance.PlayMusic("MenuMusic");
        }

        public void GameStart()
        {
            StartGameEvent?.Invoke();
            _menuInterface.SetActive(false);
            CurrentState = GameState.Playing;
            AudioManager.Instance.PlayMusic("GameMusic");
        }

        public void EndGame()
        {
            if (CurrentState == GameState.Playing)
            {
                Debug.Log("GAME OVER");
                CurrentState = GameState.GameOver;
                _playerInterface.SetActive(false);
                _gameOverInterface.SetActive(true);
                AudioManager.Instance.PlayMusic("EndGameMusic");
            }
        }

        public void ReloadGame()
        {
            SceneManager.LoadScene("Game");
        }
    }
}