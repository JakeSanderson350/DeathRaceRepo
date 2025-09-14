using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceManager : MonoBehaviour
{
    public static AudienceManager inst;

    public List<Popularity> racers = new();

    private void Awake()
    {
        inst = this;
    }
}
