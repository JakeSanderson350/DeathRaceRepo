using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public event Action<Projectile> onExplode;

    public float damage;

    

}
