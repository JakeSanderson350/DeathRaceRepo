using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource Properties", menuName = "Resource/Resource Stats")]
public class ResourceProperties : ScriptableObject
{
    public float startValue;
    public float max;
    public float min;
}
