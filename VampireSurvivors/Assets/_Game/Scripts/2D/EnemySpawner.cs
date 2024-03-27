using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner
{
    Player player;
    Enemy enemyPrefab;
    float minimumSpawnDistance = 10f;
    float maximumSpawnDistance = 20f;


    public EnemySpawner(Player palyer, Enemy enemyPrefab)
    {
        this.player = palyer;
        this.enemyPrefab = enemyPrefab;
    }

    public Enemy SpawnEnemy()
    {
        Vector3 spawnPosition = RandomSpawnPosition();
        var enemyIns = GameObject.Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        enemyIns.Init(player, Random.Range(0.01f, 0.05f));
        return enemyIns;
    }

    Vector3 RandomSpawnPosition()
    {
        float angle = Random.Range(0, 360) * Mathf.Deg2Rad; // 轉換為弧度
        float distance = Random.Range(minimumSpawnDistance, maximumSpawnDistance);
        Vector3 spawnPosition;

        spawnPosition.x = player.transform.position.x + Mathf.Cos(angle) * distance;
        spawnPosition.y = player.transform.position.y + Mathf.Sin(angle) * distance;
        spawnPosition.z = player.transform.position.z;

        return spawnPosition;
    }



}
