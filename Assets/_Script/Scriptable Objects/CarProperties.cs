using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Car Properties", menuName = "Vehicle/CarProperties")]
public class CarProperties : ScriptableObject
{
    [Header("Performance")]
    public float maxAcceleration = 2500.0f;
    public float brakeAcceleration = 1500.0f;
    public float maxSpeed = 135.0f; // m/s

    [Header("Steering")]
    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;
    public float maxSteerAngleAtMaxSpeed = 5.0f;

    [Header("Air Control")]
    public float airSteerForce = 300f;
    public float airAcceleration = 10f;
    public bool enableAirAcceleration = true;

    [Header("Physics")]
    public Vector3 _centerOfMass;
    public float gravity = 9.8f;
    public float groundMagnetism = 1.0f;
    public float magnetismDistance = 20.0f;
    public LayerMask roadLayer;

    [Header("Tricks")]
    public float jumpForce = 20000.0f;
    public float inputBufferLength = 0.2f;
}
