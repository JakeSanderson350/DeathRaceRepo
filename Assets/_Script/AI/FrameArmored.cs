using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameArmored : Frame
{
    public override void HandleImpact(Frame other)
    {
        Vector3 rb1 = GetComponent<Rigidbody>().velocity;
        Frame frame2;
        if (other.GetType() == typeof(FrameArmored))
        {
            frame2 = other as FrameArmored;
        }
        
        // Vector3 rb2 = car2.GetComponent<Rigidbody>().velocity;
        

        

    }
}
