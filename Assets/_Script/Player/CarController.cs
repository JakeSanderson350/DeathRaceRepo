using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;


public enum Axel
{
    Front,
    Rear
}

[Serializable]
public struct Wheel
{
    public GameObject wheelModel;
    public WheelCollider wheelCollider;
    //public GameObject wheelEffectObj;
    //public ParticleSystem smokeParticle;
    public Axel axel;
}

public class CarController : MonoBehaviour
{
    [SerializeField] private CarStats carProfile;

    [Header("Wheels")]
    public List<Wheel> wheels;
    private float currentTorque;

    [Header("Speed Info (Read Only)")]
    public float currentSpeed; //in m/s
    public float currentSpeedMPH; //speed in mp/h for guage
    public TextMeshProUGUI speedGauge;

    [Header("Jump Info (Read Only)")]
    public bool isGrounded;
    public float lastJumpTime;

    float gasInput, brakeInput, steerInput, steerAngle;
    private bool jumpPressedRecently = false;

    private RaycastHit hit;
    private Vector3 roadNormal;
    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = carProfile._centerOfMass;

    }

    public void SetInputs(float _steer, float _gas, float _brake)
    {
        
        steerInput = jumpPressedRecently ? 0.0f : _steer; //No steer input when doing trick input
        gasInput = _gas;
        brakeInput = _brake;
    }

    public void JumpDown()
    {
        if (isGrounded)
        {
            jumpPressedRecently = true;
            Invoke("ResetJump", carProfile.inputBufferLength);
        }
    }

    private void ResetJump()
    {
        jumpPressedRecently = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        UpdateSpeedInfo();
    }

    public void UpdateCarController()
    {
        currentTorque = 0.0f;

        Steer();
        ApplyGas();
        ApplyBrake();
        ApplyMotorTorque();
        ApplyDownforce();
    }

    void ApplyGas()
    {
        if (!isGrounded)
            return;

        if (gasInput > 0)
        {
            if (currentSpeed < carProfile.maxSpeed)
            {
                currentTorque += gasInput * carProfile.maxAcceleration /** 100*/;
            }
            else
            {
                currentTorque += gasInput * carProfile.maxAcceleration * 20;
            }
        }
    }

    void ApplyBrake()
    {
        if (!isGrounded)
            return;

        if (brakeInput > 0)
        {
            currentTorque += brakeInput * carProfile.brakeAcceleration * -1;
        }
    }


    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * carProfile.maxSteerAngle;
                steerAngle = Mathf.Lerp(steerAngle, _steerAngle, carProfile.turnSensitivity * Time.fixedDeltaTime);

                wheel.wheelCollider.steerAngle = steerAngle;
                wheel.wheelModel.transform.localEulerAngles = new Vector3(0.0f, Mathf.Clamp(Mathf.Lerp(_steerAngle, wheel.wheelCollider.steerAngle, 1 * Time.fixedDeltaTime), -carProfile.maxSteerAngle, carProfile.maxSteerAngle), 0.0f);
            }
        }
    }

    private void ApplyMotorTorque()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = currentTorque;
        }

        currentTorque = 0.0f;
    }

    private void ApplyDownforce()
    {
        if (Physics.Raycast(transform.position, -transform.up, out hit, carProfile.magnetismDistance))
        {
            // Apply magnetism
            roadNormal = hit.normal;

            float magForce = currentSpeed * carProfile.groundMagnetism * 100;
            carRb.AddForce(-roadNormal * magForce);
        }

        else
        {
            // Apply normal gravity
            carRb.AddForce(Vector3.down * carProfile.gravity, ForceMode.Acceleration);
        }
    }

    void CheckGrounded()
    {
        isGrounded = true;
        foreach (var wheel in wheels)
        {
            if (!wheel.wheelCollider.isGrounded)
            {
                isGrounded = false;
                break;
            }
        }
    }

    void UpdateSpeedInfo()
    {
        currentSpeed = carRb.velocity.magnitude;
        currentSpeedMPH = currentSpeed * 2.237f; //conversion
        //speedGauge.text = Mathf.Round(currentSpeedMPH).ToString() + " MPH";
    }
}
