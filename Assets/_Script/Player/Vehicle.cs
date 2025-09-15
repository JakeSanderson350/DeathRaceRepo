using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    CarController carController;
    Drift driftController;

    void Start()
    {
        carController = GetComponent<CarController>();
        driftController = GetComponent<Drift>();
    }

    private void FixedUpdate()
    {
        carController.UpdateCarController();
        driftController.UpdateDrift(carController.isGrounded);
    }
}
