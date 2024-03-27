using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController_2D
{
    Transform transform;
    float speed = 8f;
    PlayerInputActions playerInputActions;
    public void Init(Transform transform)
    {
        playerInputActions = new();
        playerInputActions.Player.Enable();
        this.transform = transform;
    }

    public void UpdateMove()
    {
        var dir = playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;
        transform.position += new Vector3(dir.x, dir.y, 0) * speed * Time.deltaTime;
    }
}
