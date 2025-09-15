using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    CarController carController;
    Drift driftController;

    private float steerInput, gasInput, brakeInput;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
        driftController = GetComponent<Drift>();
    }

    private void OnEnable()
    {
        InputManager.inputDriftDown += HandbrakeDown;
        InputManager.inputDriftUp += HandbrakeUp;
    }

    private void OnDisable()
    {
        InputManager.inputDriftDown -= HandbrakeDown;
        InputManager.inputDriftUp -= HandbrakeUp;
    }

    private void Update()
    {
        GetInput();
        carController.SetInputs(steerInput, gasInput, brakeInput);
    }

    private void GetInput()
    {
        steerInput = InputManager.steerInput;
        gasInput = InputManager.gasInput;
        brakeInput = InputManager.brakeInput;
    }

    private void HandbrakeDown()
    {
        driftController.DriftDown();
    }

    private void HandbrakeUp()
    {
        driftController.DriftUp();
    }
}
