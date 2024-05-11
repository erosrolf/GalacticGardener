using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
            PlanetState = 4,
            GameWin = 5
        }

        [SerializeField] private GameObject _menuInterface;
        [SerializeField] private GameObject _playerInterface;
        [SerializeField] private GameObject _gameOverInterface;

        public ScoreManager scoreManager;
        public CinemachineVirtualCamera vcam1;
        public CinemachineVirtualCamera vcam2;
        public GameObject player;
        public GameState CurrentState { get; private set; }
        public static GameManager Instance { get; private set; }

        public delegate void GameStateDelegate();
        public static event GameStateDelegate StartGameEvent;
        public static event GameStateDelegate PlanetEvent;
        public static event GameStateDelegate Win;

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
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            CurrentState = GameState.Menu;
        }

        public GameObject GetPlayer()
        {
            return player;
        }

        public Vector3 GetPlayerPosition()
        {
            return player.transform.position;
        }

        public void GameStart()
        {
            StartGameEvent?.Invoke();
            _menuInterface.SetActive(false);
            CurrentState = GameState.Playing;
            StartCoroutine(StartPlanetStateAfterDelay(GameSettings.Instance.timeToPlanet));
        }

        public void EndGame()
        {
            if (CurrentState == GameState.Playing)
            {
                Debug.Log("GAME OVER");
                AudioManager.Instance.PlaySFX("GameOver");
                CurrentState = GameState.GameOver;
                _playerInterface.SetActive(false);
                _gameOverInterface.SetActive(true);
                AudioManager.Instance.PlayMusic("EndGameMusic");
            }
        }

        public void PlanetState()
        {
            if (CurrentState == GameState.Playing)
            {
                StartCoroutine(PlanetStateCoroutine());
            }
        }

        private IEnumerator PlanetStateCoroutine()
        {
            AudioManager.Instance.PlaySFX("GameWin");

            CurrentState = GameState.PlanetState;
            PlanetEvent?.Invoke();
            yield return new WaitForSeconds(2f);
            vcam1.Priority = 0;
            yield return new WaitForSeconds(4f);
            GameWin();
        }


        public void GameWin()
        {
            CurrentState = GameState.GameWin;
            PlayerPrefs.SetInt("Score", scoreManager.GetScore());
            PlayerPrefs.Save();
            SceneManager.LoadScene("WinScene");
            Win?.Invoke();
        }

        public void ReloadGame()
        {
            SceneManager.LoadScene("Game");
        }

        private IEnumerator StartPlanetStateAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            PlanetState();
        }
    }
}