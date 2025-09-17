using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Impact Properties", menuName = "Vehicle/ImpactProperties")]
public class ImpactProperties : ScriptableObject
{
    //public
    [field: SerializeField] public float DamageMultiplier { get; private set; }
    [field: SerializeField] public float MinimumDamageForce { get; private set; }
    [field: SerializeField] public float DamageBase { get; private set; }
}
