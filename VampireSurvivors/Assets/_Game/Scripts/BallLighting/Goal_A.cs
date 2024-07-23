using UnityEngine;


namespace BallLightning
{
    public class Goal_A : MonoBehaviour
    {
        [SerializeField]
        Renderer renderer;

        [SerializeField]
        Material original, connect;

        float time;

        private void FixedUpdate()
        {
            if (Time.time - time > 1)
                renderer.material = original;
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Ball>(out var ball) == false)
                return;

            Destroy(ball.gameObject);

            renderer.material = connect;
            time = Time.time;
        }

    }
}
