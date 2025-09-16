using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class FrameArmored : Frame
{
    public override void HandleImpact(Frame other)
    {
        Vector3 vec1 = GetComponentInParent<Rigidbody>().velocity;
        Vector3 vec2 = other.GetComponentInParent<Rigidbody>().velocity;
        float dot = Vector3.Dot(vec1.normalized, vec2.normalized);
        dot *= -1; // reverse values so that a more negative dot product results in a higher damage value
        dot += 1; // make all possible dot values positive
        dot /= 2; // clamp from 0 to 1
        Frame frame2;
        if (other.GetType() == typeof(FrameArmored))
        {
            frame2 = other as FrameArmored;
            float damageToThis = ImpactController.Instance.ArmoredDamageBase * dot * ImpactController.Instance.DamageMultiplier * vec2.magnitude;
            
            // Damage Health, Damage popularity
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Frame"))
        {
            HandleImpact(other.GetComponent<Frame>());
        }
    }
}
