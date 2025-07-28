using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public int score;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverHighestScoreText;
    private int highestScore;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        highestScore = PlayerPrefs.GetInt("HighestScore", 0);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (score > highestScore)
        {
            highestScore = score;
            PlayerPrefs.SetInt("HighestScore", highestScore);
            PlayerPrefs.Save();
        }

        gameOverPanel.SetActive(true);
        gameOverHighestScoreText.text = $"{highestScore}";
        gameOverScoreText.text = scoreText.text;
    }


    public void RetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
