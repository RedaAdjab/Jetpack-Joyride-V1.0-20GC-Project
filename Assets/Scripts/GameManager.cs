using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private static int highScore;

    public event EventHandler OnGameStateChanged;

    public enum GameState
    {
        Menu,
        Playing,
        GameOver
    }

    [SerializeField] private BackgroundController bgController;

    private GameState gameState = GameState.Menu;
    private int score = 0;
    private float distanceTraveled = 0;
    private float newWorldMoveSpeed = 0;
    private float worldMoveSpeed = 0;
    private SoundManager soundManager;
    //private float worldMoveSpeed = 3f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;
        worldMoveSpeed = bgController.GetWorldMoveSpeed();
        newWorldMoveSpeed = worldMoveSpeed;
        LoadHighScore();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if (GameIsMenu())
            {
                if (Input.anyKeyDown)
                {
                    gameState = GameState.Playing;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                    soundManager.PlayStartClip();
                }
            }
            else if (GameIsPlaying())
            {
                distanceTraveled += newWorldMoveSpeed * Time.deltaTime;
                score = Mathf.RoundToInt(distanceTraveled);
                float difficultyFactor = Mathf.Clamp01(score / 500f);
                newWorldMoveSpeed = Mathf.Lerp(worldMoveSpeed, worldMoveSpeed * 2, difficultyFactor);
                bgController.SetWorldMoveSpeed(newWorldMoveSpeed);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    if (score > highScore)
                    {
                        highScore = score;
                        SaveHighScore();
                    }
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
    }

    public void ChangeGameState(GameState gameState)
    {
        this.gameState = gameState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        if (score > highScore)
        {
            soundManager.PlayHighScoreGameOverClip();
        }
        else
        {
            soundManager.PlayGameOverClip();
        }
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    public bool GameIsMenu()
    {
        return gameState == GameState.Menu;
    }

    public bool GameIsPlaying()
    {
        return gameState == GameState.Playing;
    }

    public bool GameIsGameOver()
    {
        return gameState == GameState.GameOver;
    }

    public int GetScore()
    {
        return score;
    }
    public int GetHighScore()
    {
        return highScore;
    }
    public void ResetState()
    {
        gameState = GameState.Menu;
        score = 0;
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }
}
