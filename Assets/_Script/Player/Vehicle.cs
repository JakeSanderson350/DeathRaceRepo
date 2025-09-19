using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    CarController carController;
    Drift driftController;
    TrickController trickController;

    private float steerInput, gasInput, brakeInput;
    Vector2 moveInput;

    void Start()
    {
        carController = GetComponent<CarController>();
        driftController = GetComponent<Drift>();
        trickController = GetComponent<TrickController>();
    }

    private void FixedUpdate()
    {
        carController.UpdateCarController();
        driftController.UpdateDrift(carController.isGrounded);
        trickController.UpdateTrickController(carController.isGrounded);
    }

    public void SetInputs(float _steer, float _gas, float _brake, Vector2 _move)
    {
        steerInput = _steer;
        gasInput = _gas;   
        brakeInput = _brake;
        moveInput = _move;

        carController.SetInputs(steerInput, gasInput, brakeInput);
        trickController.SetMoveInput(moveInput);
    }

    public void HandbrakeDown()
    {
        driftController.DriftDown();
    }

    public void HandbrakeUp()
    {
        driftController.DriftUp();
    }

    public void JumpDown()
    {
        trickController.JumpDown();
    }
}
