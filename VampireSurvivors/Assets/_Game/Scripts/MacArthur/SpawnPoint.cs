using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VSCodeEditor;

namespace MacArthur
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField]
        EnemyView enemyPrefab;
        List<EnemyView> enemies = new List<EnemyView>();

        float lastSpawnTime;
        const float SPAWN_INTERVAL = 1.0f;
        public const float MAX_ENEMY_COUNT = 5;

        Action<SpawnPoint, EnemyView> onSpawn;
        Func<int> getEnemyCount;

        public void Init(Action<SpawnPoint, EnemyView> onSpawn, Func<int> getEnemyCount)
        {
            this.onSpawn = onSpawn;
            this.getEnemyCount = getEnemyCount;
        }


        public void SpawnUpdateTask()
        {
            if (getEnemyCount() > MAX_ENEMY_COUNT)
                return;

            SpawnEnemy();
        }

        void SpawnEnemy()
        {
            if ((Time.time - lastSpawnTime) < SPAWN_INTERVAL)
                return;

            lastSpawnTime = Time.time;
            var enemy = Instantiate(enemyPrefab, RandomPosition(transform.position), Quaternion.identity, transform);
            enemies.Add(enemy);
            onSpawn(this, enemy);

            Vector3 RandomPosition(Vector3 center)
            {
                return center + new Vector3(UnityEngine.Random.Range(-5, 5), 0, UnityEngine.Random.Range(-5, 5));
            }
        }




    }

}



