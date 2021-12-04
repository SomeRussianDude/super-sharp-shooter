using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    [SerializeField] private float delayInSeconds = 3f;

    private GameSession gameSession;

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        FindObjectOfType<GameSession>().ResetGame();
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}