using UnityEngine;

namespace BallLightning
{
    public class Ball : MonoBehaviour
    {

        Vector3 direction;

        [SerializeField]
        float speed;
        public string ParentId { get; private set; }

        public Vector3 Direction => direction;

        public void Init(string parentId, Vector3 pos, Quaternion rot, Vector3 dir)
        {
            this.ParentId = parentId;
            transform.position = pos;
            transform.rotation = rot;
            this.direction = dir.normalized;
        }

        public void SetDirection(Vector3 dir)
        {
            this.direction = dir.normalized;
        }

        void FixedUpdate()
        {
            transform.position += direction * speed * Time.fixedDeltaTime;
        }



    }
}