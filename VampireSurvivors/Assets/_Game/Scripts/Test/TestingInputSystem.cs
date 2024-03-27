using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{
    Rigidbody rigidbody;
    PlayerInputActions playerInputActions;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();

        playerInputActions = new();
        playerInputActions.Player.Enable();
        // playerInputActions.Player.Jump.performed += Jump;
    }

    void FixedUpdate()
    {
        var input = playerInputActions.Player.Movement.ReadValue<Vector2>();
        rigidbody.AddForce(new Vector3(input.x, 0, input.y) * 0.5f, ForceMode.Impulse);
    }

    void Movement_Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Movement " + context);

        var input = context.ReadValue<Vector2>();

        float speed = 5;

        rigidbody.AddForce(new Vector3(input.x, 0, input.y) * speed, ForceMode.Impulse);
    }

    void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump" + context.phase);

        if (context.phase == InputActionPhase.Performed)
        {
            rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }


    }

}
