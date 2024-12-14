using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text coinCountText;
    [SerializeField] private Text restartingGameText;

    private void Start()
    {
        // Hide the "Game Over" text at start
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
            restartingGameText.gameObject.SetActive(false);
        }
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score;
        }
    }

    public void UpdateCount(int count)
    {
        if (coinCountText != null)
        {
            coinCountText.text = "COIN COUNT: " + count;
        }
    }

    public void ShowGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        StartCoroutine(RestartCountdown());  
    }

    private IEnumerator RestartCountdown()
    {
        restartingGameText.gameObject.SetActive(true);

        // Count down from 10 to 1
        for (int i = 10; i > 0; i--)
        {
            restartingGameText.text = "Restarting in " + i.ToString() + "...";
            yield return new WaitForSeconds(1f);
        }

        // After 10 seconds, reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


