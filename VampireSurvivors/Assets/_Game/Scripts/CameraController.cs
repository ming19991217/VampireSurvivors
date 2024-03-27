using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    Camera _camera;
    public Camera Camera => _camera;


    [SerializeField]
    float speed = 5f;

    [SerializeField]
    float borderRange = 10f;
    float borderSpeed = 20f;

    PlayerInputActions playerInputActions;


    public void Init()
    {
        playerInputActions = new();
        playerInputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.Confined;
    }


    public void SetTarget(Transform target)
    {
        this.transform.position = target.position;
    }


    void FixedUpdate()
    {
        var dir = playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;
        transform.position += new Vector3(dir.x, 0, dir.y) * speed * Time.deltaTime;


        var mousePos = Mouse.current.position.ReadValue();

        if (mousePos.x > Screen.width - borderRange)
        {
            transform.position += new Vector3(1, 0, 0) * borderSpeed * Time.deltaTime;
        }

        if (mousePos.x < borderRange)
        {
            transform.position += new Vector3(-1, 0, 0) * borderSpeed * Time.deltaTime;
        }

        if (mousePos.y > Screen.height - borderRange)
        {
            transform.position += new Vector3(0, 0, 1) * borderSpeed * Time.deltaTime;
        }

        if (mousePos.y < borderRange)
        {
            transform.position += new Vector3(0, 0, -1) * borderSpeed * Time.deltaTime;
        }

    }
}
