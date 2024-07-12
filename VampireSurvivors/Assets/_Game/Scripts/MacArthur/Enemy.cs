using System;
using UnityEngine;
using UnityEngine.AI;


namespace MacArthur
{
    public class Enemy : MonoBehaviour
    {

        [SerializeField]
        NavMeshAgent agent;

        Action<Enemy> onDestroy;

        void Init(Action<Enemy> onDestroy)
        {
            this.onDestroy = onDestroy;

        }

        public void SetDestination(Vector3 pos)
        {
            agent.SetDestination(pos);
        }



    }
}