using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRegular : Frame
{
    public FrameStats stats;
    public override void HandleImpact(Frame other)
    {
        Vector3 vec1 = GetComponentInParent<Rigidbody>().velocity;
        Vector3 vec2 = other.GetComponentInParent<Rigidbody>().velocity;
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
