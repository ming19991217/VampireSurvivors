using UnityEngine;

namespace MacArthur
{

    public interface IEnemyState
    {
        void EnterState(EnemyView enemyView);
        void UpdateState(EnemyView enemyView);
        void OnTriggerEnter(EnemyView enemyView, Collider other);
    }

    public class EnemyState_Idle : IEnemyState
    {
        public void EnterState(EnemyView enemyView)
        {
            enemyView.SetDestination(enemyView.transform.position);
        }

        float timer = 0;

        public void UpdateState(EnemyView enemyView)
        {
            timer += Time.deltaTime;
            if (timer > 3 && Random.Range(0, 100) < 10)
            {
                RandomMove();
                timer = 0;
            }

            void RandomMove()
            {
                var randomPos = enemyView.SpawnPoint.transform.position + new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
                enemyView.SetDestination(randomPos);
            }
        }

        public void OnTriggerEnter(EnemyView enemyView, Collider other) { }
    }



}