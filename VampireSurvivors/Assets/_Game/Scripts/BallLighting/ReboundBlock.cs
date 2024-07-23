using UnityEngine;


namespace BallLightning
{
    public class ReboundBlock : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<Ball>(out var ball) == false)
                return;

            // 獲取碰撞點
            Vector3 collisionPoint = other.ClosestPoint(transform.position);
            Debug.DrawLine(ball.transform.position, collisionPoint, Color.green, 2f);

            // 計算入射向量（從物體到碰撞點的向量）
            Vector3 incomingVector = ball.Direction;

            // 獲取牆面的法線向量
            Vector3 wallNormal = transform.right; // 假設牆面的前方是法線方向
                                                  // 判斷撞擊方向並調整法線

            if (Vector3.Dot(incomingVector, wallNormal) > 0)
            {
                // 如果入射向量和牆面法線方向相同，說明是從背面撞擊
                wallNormal = -wallNormal;
            }

            Debug.DrawRay(collisionPoint, wallNormal, Color.blue, 2f);

            // 計算反彈向量
            Vector3 bounceDirection = Vector3.Reflect(incomingVector, wallNormal).normalized;


            ball.SetDirection(bounceDirection);



        }

    }
}