using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 6;
    [SerializeField] private float moveSpeed = 2f;
    
    public GameObject EnemyPrefab { get => enemyPrefab; }
    public float TimeBetweenSpawns {get => timeBetweenSpawns;}
    public float SpawnRandomFactor { get => spawnRandomFactor; }
    public int NumberOfEnemies { get => numberOfEnemies;}
    public float MoveSpeed { get => moveSpeed;}

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }
}