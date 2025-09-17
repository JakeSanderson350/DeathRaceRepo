using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public event Action<Projectile> onExplode;

    public float damage;
    public float force;
    public float radius;
    public LayerMask mask;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        //add all over the racers hit 
        List<Popularity> racersHit = new();
        Physics.OverlapSphere(transform.position, radius, mask).ToList().FindAll(x => x.GetComponentInParent<Popularity>() != null).ForEach(x => racersHit.Add(x.GetComponentInParent<Popularity>())); //THE LAMBDA EXPRESSION FROM HELL >:^D
        racersHit = racersHit.Distinct().ToList(); //remove duplicates

#if UNITY_EDITOR
        //Debug.Log("Rocket Exp");
        racersHit.ForEach(x => Debug.DrawLine(transform.position, x.transform.position, Color.red, 2f));
#endif

        foreach (Popularity pop in racersHit)
        {
            //Debug.Log(pop.name + " hit for " + damage);
            pop.Value -= damage; //this is bad fix later
            pop.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
        }

        onExplode?.Invoke(this);
        gameObject.SetActive(false);
        Invoke("DestorySelf", 1f);
    }

    void DestorySelf()
    {
        Destroy(gameObject);
    }
}
