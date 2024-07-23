using System;
using UnityEngine;

namespace BallLightning
{
    public class TriggerBase : MonoBehaviour
    {
        [SerializeField]
        Item item;

        [SerializeField]
        float interval = .1f;

        float time;

        [SerializeField]
        Transform root;

        [SerializeField]
        Ball ballPrefab;
        string parentId;
        bool isEnable;

        void Awake()
        {
            item.RegisterOnEnableChanged((isEnable) =>
            {
                this.isEnable = isEnable;
            });

            parentId = Guid.NewGuid().ToString();
        }

        void OnTriggerEnter(Collider other)
        {
            if (isEnable == false)
                return;

            if (Time.time - time < interval)
                return;

            if (other.gameObject.TryGetComponent<Ball>(out var ball) == false)
                return;

            if (ball.ParentId == parentId)
                return;

            Destroy(ball.gameObject);

            ball = Instantiate(ballPrefab, root);
            ball.Init(parentId, transform.position, transform.rotation, transform.forward);
        }

    }
}