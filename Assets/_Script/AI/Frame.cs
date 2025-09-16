using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Frame : MonoBehaviour
{
    public abstract void HandleImpact(Frame other);

}
