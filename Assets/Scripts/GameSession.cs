using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    private int playerScore;

    public int PlayerScore => playerScore;

    private void Awake()
    {
        Singleton();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncreaseScoreForHit(int hitPoints)
    {
        playerScore += hitPoints;
    }

    public void IncreaseScoreForKill(int killPoints)
    {
        playerScore += killPoints;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    private void Singleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}