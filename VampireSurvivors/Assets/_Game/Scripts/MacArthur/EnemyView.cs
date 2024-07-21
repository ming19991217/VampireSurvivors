using System;
using UnityEngine;
using UnityEngine.AI;


namespace MacArthur
{
    public class EnemyView : MonoBehaviour
    {

        [SerializeField]
        NavMeshAgent agent;
        Action<EnemyView> onDestroy;
        public SpawnPoint SpawnPoint { get; private set; }

        IEnemyState state = new EnemyState_Idle();

        void Init(SpawnPoint spawnPoint, Action<EnemyView> onDestroy)
        {
            this.onDestroy = onDestroy;
            SpawnPoint = spawnPoint;
        }

        public void SetDestination(Vector3 pos)
        {
            agent.SetDestination(pos);
        }


        public void ChangeState(IEnemyState state)
        {
            this.state = state;
            state.EnterState(this);
        }

        void FixedUpdate()
        {
            state.UpdateState(this);
        }

        void OnTriggerEnter(Collider other)
        {
            state.OnTriggerEnter(this, other);
        }





    }
}