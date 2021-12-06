using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<List<Enemy>> currentEnemyCount = new List<List<Enemy>>();
    

    public void Register(int waveIndex, Enemy enemy)
    {
        currentEnemyCount[waveIndex].Add(enemy);
    }

    public void MarkDead(int waveIndex, Enemy enemy)
    {
        currentEnemyCount[waveIndex].Remove(enemy);
        if (currentEnemyCount[waveIndex].Count == 0)
        {
            enemy.HealthDrop();
        }
    }
    
}
