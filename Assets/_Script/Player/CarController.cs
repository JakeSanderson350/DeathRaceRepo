using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarController : MonoBehaviour
{
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

    [Header("Performance")]
    public float maxAcceleration = 30.0f;
    public float maxSpeed = 44.704f; // m/s
    //public float brakeAcceleration = 35.0f;

    [Header("Steering")]
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    [Header("Air Control")]
    public float airSteerForce = 300f;
    public float airAcceleration = 10f;
    public bool enableAirAcceleration = true;

    [Header("Physics")]
    public Vector3 _centerOfMass;
    public float groundMagnetism = 1.5f;

    [Header("Wheels")]
    public List<Wheel> wheels;

    [Header("Speed Info (Read Only)")]
    public float currentSpeed; //in m/s
    public float currentSpeedMPH; //speed in mp/h for guage
    public TextMeshProUGUI speedGauge;

    [Header("Jump Info (Read Only)")]
    public bool isGrounded;
    public float lastJumpTime;

    float gasInput, brakeInput;
    float steerInput;

    private Rigidbody carRb;

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass;

    }

    public void SetInputs(float _steer, float _gas, float _brake)
    {
        steerInput = _steer;
        gasInput = _gas;
        brakeInput = _brake;
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        UpdateSpeedInfo();
    }

    public void UpdateCarController()
    {
        Steer();
        ApplyGas();

        float groundForce = currentSpeed * groundMagnetism * 100f;
        carRb.AddForce(Vector3.down * groundForce);
    }

    void ApplyGas()
    {
        if (!isGrounded)
            return;

        float currentMaxSpeed = maxSpeed;
        float currentAcceleration = maxAcceleration;

        foreach (var wheel in wheels)
        {
            if (gasInput > 0)
            {
                if (currentSpeed < maxSpeed)
                {
                    wheel.wheelCollider.motorTorque = gasInput * maxAcceleration * 100;
                }
                else
                {
                    wheel.wheelCollider.motorTorque = gasInput * maxAcceleration * 20;
                }
            }
            else if (gasInput < 0)
            {
                Debug.Log("Sure?");
                wheel.wheelCollider.motorTorque = gasInput * maxAcceleration/* * 100*/;
            }
            else
            {
                wheel.wheelCollider.motorTorque = 0;
            }
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;

                float lerpSpeed = 0.6f;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, lerpSpeed);
            }
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
