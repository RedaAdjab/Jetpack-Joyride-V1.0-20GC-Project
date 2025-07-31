using System;
using TMPro;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject gameStartUI;
    [SerializeField] private GameObject gamePlayingUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI playingScore;
    [SerializeField] private TextMeshProUGUI playingHighScore;
    [SerializeField] private TextMeshProUGUI endScore;
    [SerializeField] private TextMeshProUGUI endHighScore;
    [SerializeField] private TextMeshProUGUI congratsText;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OnGameStateChanged += GameManager_OnGameStateChanged;

        gameStartUI.SetActive(true);
        gamePlayingUI.SetActive(false);
        gameOverUI.SetActive(false);
    }

    private void LateUpdate()
    {
        if (gameManager.GameIsPlaying())
        {
            playingScore.text = "Score:" + gameManager.GetScore().ToString();
        }
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e)
    {
        if (gameManager.GameIsPlaying())
        {
            gameOverUI.SetActive(false);
            gameStartUI.SetActive(false);
            playingHighScore.text = "High Score:" + gameManager.GetHighScore().ToString();
            gamePlayingUI.SetActive(true);
        }
        else if (gameManager.GameIsGameOver())
        {
            gameStartUI.SetActive(false);
            gamePlayingUI.SetActive(false);
            endScore.text = "Score:" + gameManager.GetScore().ToString();
            endHighScore.text = "High Score:" + gameManager.GetHighScore().ToString();
            if (gameManager.GetScore() > gameManager.GetHighScore())
            {
                congratsText.gameObject.SetActive(true);
            }
            gameOverUI.SetActive(true);
        }
    }
}
