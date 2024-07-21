using System.Collections.Generic;
using System.Linq;
using MacArthur;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    List<SpawnPoint> spawnPoints;
    Dictionary<SpawnPoint, List<EnemyView>> enemyDic;


    void Awake()
    {
        Init();
    }

    void Init()
    {
        enemyDic = spawnPoints.ToDictionary(spawnPoint => spawnPoint, spawnPoint => new List<EnemyView>());
        spawnPoints.ForEach(spawnPoint => spawnPoint.Init(AddEnemy, () => enemyDic[spawnPoint].Count));
    }

    public void AddEnemy(SpawnPoint spawnPoint, EnemyView enemyView)
    {
        enemyDic[spawnPoint].Add(enemyView);

        if (enemyDic[spawnPoint].Count < SpawnPoint.MAX_ENEMY_COUNT)
            return;

        MoveEnemy(enemyDic[spawnPoint]);
    }

    void MoveEnemy(List<EnemyView> enemies)
    {





    }

    void FixedUpdate()
    {
        spawnPoints.ForEach(spawnPoint => spawnPoint.SpawnUpdateTask());
    }







}