using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemySpawner
{
    Player player;
    Enemy enemyPrefab;
    Action<Enemy> onEnemySpawn;

    float minimumSpawnDistance = 10f;
    float maximumSpawnDistance = 20f;
    float spawnInterval = .05f;
    float currentSpawnInterval = 0f;


    public EnemySpawner(Player palyer, Enemy enemyPrefab, Action<Enemy> onEnemySpawn)
    {
        this.player = palyer;
        this.enemyPrefab = enemyPrefab;
        this.onEnemySpawn = onEnemySpawn;
    }


    Enemy SpawnEnemy()
    {
        Vector3 spawnPosition = RandomSpawnPosition();
        var enemyIns = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        return enemyIns;
    }

    Vector3 RandomSpawnPosition()
    {
        float angle = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad; // 轉換為弧度
        float distance = UnityEngine.Random.Range(minimumSpawnDistance, maximumSpawnDistance);
        Vector3 spawnPosition;

        spawnPosition.x = player.transform.position.x + Mathf.Cos(angle) * distance;
        spawnPosition.y = player.transform.position.y + Mathf.Sin(angle) * distance;
        spawnPosition.z = player.transform.position.z;

        return spawnPosition;
    }

    public void EnemySpawnUpdate()
    {
        currentSpawnInterval -= Time.deltaTime;
        if (currentSpawnInterval <= 0)
        {
            var enemyIns = SpawnEnemy();
            onEnemySpawn?.Invoke(enemyIns);
            currentSpawnInterval = spawnInterval;
        }
    }

}
