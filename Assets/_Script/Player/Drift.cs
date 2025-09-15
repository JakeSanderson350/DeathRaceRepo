using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    private Rigidbody rb;
    private CarController carController;
    private List<Wheel> wheels;
    private WheelFrictionCurve[] originalSidewaysFriction;

    public bool isDrifting = false;
    private bool isGrounded;
    private float driftAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();
        wheels = carController.wheels; //Not very upwards blind but uhhhh
        StoreFrictionCurves();
    }

    public void DriftPressed()
    {
        if (isGrounded)
            isDrifting = !isDrifting;
    }

    // Update is called once per frame
    public void UpdateDrift(bool _isGrounded)
    {
        isGrounded = _isGrounded;

        if (isDrifting)
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                if (wheels[i].axel == Axel.Rear)
                {
                    float tireGripFactor = TireGripLookUpCurve(GetDriftAngle());
                    WheelFrictionCurve sidewaysFriction = originalSidewaysFriction[i];

                    sidewaysFriction.extremumValue *= 0.2f;
                    sidewaysFriction.asymptoteValue *= 0.2f;

                    Debug.Log(sidewaysFriction.extremumValue);

                    wheels[i].wheelCollider.sidewaysFriction = sidewaysFriction;
                }
            }
        }
        else
        {
            for (int i = 0; i < wheels.Count; i++)
            {
                if (wheels[i].axel == Axel.Rear)
                {
                    wheels[i].wheelCollider.sidewaysFriction = originalSidewaysFriction[i];
                }
            }
        }
    }



    private float TireGripLookUpCurve(float _tireAngle)
    {
        float tireGrip;

        if (_tireAngle < 0)
        {
            tireGrip = 1.0f;
        }
        else
        {
            tireGrip = 0.0075f * _tireAngle/* - 0.5f*/;
        }
        return tireGrip;
    }

    public float GetDriftAngle()
    {
        if (isGrounded)
        {
            return 0.0f;
        }

        Vector3 driveDirection = transform.forward.normalized;

        // Calculates drift angle based on the angle between the velocity of the rb and the direction the car is actually facing
        return Vector3.Angle(rb.velocity, driveDirection) * Vector3.Cross(rb.velocity.normalized, driveDirection).y;
    }

    private void StoreFrictionCurves()
    {
        originalSidewaysFriction = new WheelFrictionCurve[wheels.Count];

        for (int i = 0; i < wheels.Count; i++)
        {
            originalSidewaysFriction[i] = wheels[i].wheelCollider.sidewaysFriction;
        }
    }
}
