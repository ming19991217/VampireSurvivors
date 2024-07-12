using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    float speed = 15.0f;

    [SerializeField]
    Transform rotatePivot;

    bool IsSpeedKeyPressed() => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");
        Vector3 moveDirection = new Vector3(moveX, 0, moveZ).normalized;

        float speed = IsSpeedKeyPressed() switch
        {
            true => 30.0f,
            false => 15.0f
        };

        Vector3 moveVelocity = (rotatePivot.right * moveX + rotatePivot.forward * moveZ).normalized * speed * Time.deltaTime;
        moveVelocity.y = 0;
        transform.position = transform.position + moveVelocity;

        float rotateDir = 0;
        if (Input.GetKey(KeyCode.Q))
            rotateDir = -1;
        else if (Input.GetKey(KeyCode.E))
            rotateDir = 1;
        rotatePivot.Rotate(Vector3.up, rotateDir * 100 * Time.deltaTime);

    }








}