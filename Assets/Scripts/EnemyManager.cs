using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject healthPack;
    [SerializeField] private float dropSpeed = 2f;

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
            HealthDrop(enemy);
        }
    }
    public void HealthDrop(Enemy enemy)
    {
        GameObject healthPack = Instantiate(this.healthPack, enemy.transform.position, enemy.transform.rotation);
        healthPack.GetComponent<Rigidbody2D>().velocity = new Vector2(0, dropSpeed);
    }

}
