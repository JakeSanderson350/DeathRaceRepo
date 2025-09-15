using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransform : MonoBehaviour
{
    public Vector3 euler;
    public Space space;

    private void Update()
    {
        transform.Rotate(euler * Time.deltaTime, space); 
    }
}
