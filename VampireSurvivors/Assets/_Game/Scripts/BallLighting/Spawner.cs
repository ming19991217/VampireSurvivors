using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Serialization;
using UnityEngine;

namespace BallLightning
{
    public class Spawner : MonoBehaviour
    {

        [SerializeField]
        Item item;

        [SerializeField]
        float interval = 1;

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

            parentId = System.Guid.NewGuid().ToString();
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(interval);

                if (isEnable == false)
                    continue;

                var ball = Instantiate(ballPrefab, root);
                ball.Init(parentId, transform.position, transform.rotation, transform.forward);
            }
        }


    }
}