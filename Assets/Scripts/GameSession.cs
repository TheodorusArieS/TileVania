using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    ScenePersist scenePersist;
    int coins = 0;
    void Awake()
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        scenePersist = FindObjectOfType<ScenePersist>();
        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = coins.ToString();
    }



    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        scenePersist.ResetScenePersist();
    }

    void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseCoin(int points)
    {
        coins++;
        scoreText.text = (coins * points).ToString();
        Debug.Log("Current Coins:" + coins);
    }


}
