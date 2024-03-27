using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    Player player;

    [SerializeField]
    Enemy enemyPrefab;

    EnemySpawner enemySpawner;


    static float spawnInterval = .1f;
    float currentSpawnInterval = spawnInterval;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        new EnemySpatialGroups();
        enemySpawner = new(player, enemyPrefab);
    }


    private void FixedUpdate()
    {
        currentSpawnInterval -= Time.deltaTime;
        if (currentSpawnInterval <= 0)
        {
            enemySpawner.SpawnEnemy();
            currentSpawnInterval = spawnInterval;
        }
    }





}