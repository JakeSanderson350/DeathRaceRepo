using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactController : MonoBehaviour
{

    public static ImpactController Instance;

    //public
    [field: SerializeField] public float DamageMultiplier {  get; private set; }
    [field: SerializeField] public float MinimumDamageForce { get; private set; }
    [field: SerializeField] public float RegularDamageBase { get; private set; }
    [field: SerializeField] public float ArmoredDamageBase { get; private set; }
    [field: SerializeField] public float VulnerableDamageBase { get; private set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
