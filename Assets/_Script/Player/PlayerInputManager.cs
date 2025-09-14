using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    CarController carController;

    private float steerInput, gasInput, brakeInput;

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
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
}
