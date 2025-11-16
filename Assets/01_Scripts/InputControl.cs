using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControl : MonoBehaviour
{
    DefaultInput inputs;
    Vector2 MoveInput;

    [SerializeField] PlayerMovement playerMove;

    private void Awake()
    {
        inputs = new DefaultInput();

        inputs.Player.Move.performed += Move_performed;
        inputs.Player.Move.canceled += Move_canceled;
        inputs.Player.Jump.started += Jump_started;
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void Move_canceled(InputAction.CallbackContext obj)
    {
        MoveInput = Vector2.zero;
        playerMove.MoveInput = MoveInput;
    }

    private void Move_performed(InputAction.CallbackContext obj)
    {
        MoveInput = obj.ReadValue<Vector2>();
        playerMove.MoveInput = MoveInput;
    }

    private void Jump_started(InputAction.CallbackContext obj)
    {
        if (!obj.started) return;
        playerMove.Jump();
    }
}
