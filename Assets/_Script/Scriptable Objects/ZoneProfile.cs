using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZoneProfile", menuName = "Track/Zone Profile")]
public class ZoneProfile : ScriptableObject
{
    [Header("Behavior Weights")]
    // weights
    [Tooltip("Likelihood of enemies to accelerate when within this zone.")]
    [Range(0f, 1f)]
    public float accelerate;

    [Tooltip("Likelihood of enemies to brake when within this zone.")]
    [Range(0f, 1f)]
    public float brake;

    [Tooltip("Likelihood of enemies to drift when within this zone.")]
    [Range(0f, 1f)]
    public float drift;

    [Tooltip("Likelihood of enemies to attempt a trick in this zone.")]
    [Range(0f, 1f)]
    public float trick;

}
