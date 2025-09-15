using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    CarController carController;
    Drift driftController;
    TrickController trickController;

    private float steerInput, gasInput, brakeInput;
    Vector2 moveInput;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
        driftController = GetComponent<Drift>();
        trickController = GetComponent<TrickController>();
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

        carController.SetInputs(steerInput, gasInput, brakeInput);
        trickController.SetMoveInput(moveInput);
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
        driftController.DriftDown();
    }

    private void HandbrakeUp()
    {
        driftController.DriftUp();
    }

    private void JumpDown()
    {
        trickController.JumpDown();
    }
}
