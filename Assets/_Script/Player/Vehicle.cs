using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    CarController carController;

    void Start()
    {
        carController = GetComponent<CarController>();
    }

    private void FixedUpdate()
    {
        carController.UpdateCarController();
    }
}
