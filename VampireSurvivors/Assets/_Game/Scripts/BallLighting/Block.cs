using UnityEngine;

namespace BallLightning
{

    public class Block : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Ball>(out var ball) == false)
                return;

            Destroy(ball.gameObject);
        }

    }
}