using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrameData", menuName = "Frame/Frame Data")]
public class FrameStats : ScriptableObject
{
    [Header("Damage")]
    public float damageMult;
}
