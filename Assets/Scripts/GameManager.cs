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
            Paused = 4,
        }

        [SerializeField] private Canvas _menu;
        public GameState CurrentState { get; private set; }
        public static GameManager Instance { get; private set; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene((int)GameState.Menu);
            CurrentState = GameState.Menu;
            // AudioManager.Instance.PlayMusic("MenuMusic");
        }

        public void GameStart()
        {
            _menu.enabled = false;
            CurrentState = GameState.Playing;
            AudioManager.Instance.PlayMusic("GameMusic");
        }

        public void PauseGame()
        {
            if (CurrentState == GameState.Playing)
            {
                // Здесь может быть код, который ставит игру на паузу
                // Например, остановка времени в игре
                _menu.enabled = true;
                CurrentState = GameState.Paused;
            }
        }

        public void ResumeGame()
        {
            if (CurrentState == GameState.Paused)
            {
                // Здесь может быть код, который возобновляет игру
                // Например, возобновление времени в игре
                CurrentState = GameState.Playing;
            }
        }

        public void EndGame()
        {
            if (CurrentState == GameState.Playing)
            {
                // Здесь может быть код, который обрабатывает окончание игры
                // Например, показ экрана Game Over, сохранение счета и т.д.
                SceneManager.LoadScene((int)GameState.GameOver);
                CurrentState = GameState.GameOver;
                AudioManager.Instance.PlayMusic("EndGameMusic");
            }
        }
    }
}