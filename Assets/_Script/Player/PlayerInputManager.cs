using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    Vehicle vehicle;

    private float steerInput, gasInput, brakeInput;
    Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    private void OnEnable()
    {
        InputManager.inputDriftDown += HandbrakeDown;
        InputManager.inputDriftUp += HandbrakeUp;
        InputManager.inputJump += JumpDown;
    }

    private void OnDisable()
    {
        InputManager.inputDriftDown -= HandbrakeDown;
        InputManager.inputDriftUp -= HandbrakeUp;
        InputManager.inputJump -= JumpDown;
    }

    private void Update()
    {
        GetInput();

        vehicle.SetInputs(steerInput, gasInput, brakeInput, moveInput);
    }

    private void GetInput()
    {
        steerInput = InputManager.steerInput;
        gasInput = InputManager.gasInput;
        brakeInput = InputManager.brakeInput;
        moveInput = InputManager.inputMove;
    }

    private void HandbrakeDown()
    {
        vehicle.HandbrakeDown();
    }

    private void HandbrakeUp()
    {
        vehicle.HandbrakeUp();
    }

    private void JumpDown()
    {
        vehicle.JumpDown();
    }
}
