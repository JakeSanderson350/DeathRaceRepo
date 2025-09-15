using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    CarController carController;
    Drift driftController;
    TrickController trickController;

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
}
