using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInput controls;
    PlayerInput.DefaultActions defaultControls;
    PlayerInput.MenuActions menuControls;
    PlayerInput.DrivingActions drivingControls;

    public static Vector2 inputMove;
    public static Vector2 inputDeltaPointer;
    public static float steerInput, gasInput, brakeInput;
    public static Action inputMenu;
    public static Action inputStart, inputDriftDown, inputDriftUp, inputJump;


    private void Awake()
    {
        //setup controls
        controls = new PlayerInput();
        defaultControls = controls.Default;
        menuControls = controls.Menu;
        drivingControls = controls.Driving;

        defaultControls.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        defaultControls.Look.performed += ctx => inputDeltaPointer = ctx.ReadValue<Vector2>();
        menuControls.Menu.started += ctx => inputMenu?.Invoke();
        //menuControls.StartRun.started += ctx => inputStart?.Invoke();

        drivingControls.Steer.performed += ctx => steerInput = ctx.ReadValue<float>();
        drivingControls.Steer.canceled += ctx => steerInput = ctx.ReadValue<float>();
        drivingControls.Gas.performed += ctx => gasInput = ctx.ReadValue<float>();
        drivingControls.Gas.canceled += ctx => gasInput = ctx.ReadValue<float>();
        drivingControls.Brake.performed += ctx => brakeInput = ctx.ReadValue<float>();
        drivingControls.Brake.canceled += ctx => brakeInput = ctx.ReadValue<float>();
        drivingControls.Handbrake.started += ctx => inputDriftDown.Invoke();
        drivingControls.Handbrake.canceled += ctx => inputDriftUp.Invoke();
        drivingControls.Jump.started += ctx => inputJump.Invoke();
    }

    private void OnEnable() //restarts controls when needed
    {
        controls.Enable();
    }

    private void OnDisable() //prevents controls running in editor
    {
        controls.Disable();   
    }

    /*void EnableControls(InputManager.Profile profile)
    {
        
    }*/
}
