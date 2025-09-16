using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class FrameArmored : Frame
{
    public FrameStats stats;
    public override void HandleImpact(Frame other)
    {
        Vector3 vec1 = GetComponentInParent<Rigidbody>().velocity;
        Vector3 vec2 = other.GetComponentInParent<Rigidbody>().velocity;
        float dot = Vector3.Dot(vec1.normalized, vec2.normalized);
        dot *= -1;
        dot += 1;
        dot /= 2;
        Frame frame2;
        if (other.GetType() == typeof(FrameArmored))
        {
            frame2 = other as FrameArmored;
            
            
        }
        else if (other.GetType() == typeof(FrameRegular))
        {
            frame2 = other as FrameRegular;
        }
        else if (other.GetType() == typeof(FrameVulnerable))
        {
            frame2 = other as FrameVulnerable;
        }




    }
}
