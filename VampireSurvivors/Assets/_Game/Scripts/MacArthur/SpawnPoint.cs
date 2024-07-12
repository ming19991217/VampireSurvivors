using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VSCodeEditor;

namespace MacArthur
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField]
        Enemy enemyPrefab;
        List<Enemy> enemies = new List<Enemy>();

        [SerializeField]
        Transform target;

        float lastSpawnTime;
        const float SPAWN_INTERVAL = 1.0f;
        bool waitCombatFinish = false;


        void FixedUpdate()
        {
            if (waitCombatFinish)
            {
                if (enemies.Count == 0)
                    waitCombatFinish = false;

                return;
            }


            if (enemies.Count < 5)
            {
                SpawnEnemy();
            }
            else
            {
                GoToFighting();
                waitCombatFinish = true;
            }
        }
        void GoToFighting()
        {
            enemies.ForEach(e => e.SetDestination(target.position));
        }


        void SpawnEnemy()
        {
            if ((Time.time - lastSpawnTime) < SPAWN_INTERVAL)
                return;

            lastSpawnTime = Time.time;
            var enemy = Instantiate(enemyPrefab, RandomPosition(transform.position), Quaternion.identity, transform);
            enemies.Add(enemy);

            Vector3 RandomPosition(Vector3 center)
            {
                return center + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            }
        }




    }

}



