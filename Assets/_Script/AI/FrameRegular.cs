using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRegular : Frame
{
    public override void HandleImpact(Frame other)
    {
        base.HandleImpact(other);

        if (other.transform.parent == transform.parent) return;

        Vector3 vec1 = GetComponentInParent<Rigidbody>().velocity;
        Vector3 vec2 = other.GetComponentInParent<Rigidbody>().velocity;
        float dot = Vector3.Dot(vec1.normalized, vec2.normalized);
        dot *= -1; // reverse values so that a more negative dot product results in a higher damage value
        dot += 1; // make all possible dot values positive
        dot /= 2; // clamp from 0 to 1

        float damageToThis = other.impactProperties.DamageBase * dot * other.impactProperties.DamageMultiplier * vec2.magnitude;

        switch (other)
        {
            case FrameArmored armored:
                popularity.Value -= damageToThis;
                break;
            case FrameRegular regular:
                //stub
                break;
            case FrameVulnerable vulnerable:
                popularity.Value += damageToThis / 2;
                break;
        }
    }
}
