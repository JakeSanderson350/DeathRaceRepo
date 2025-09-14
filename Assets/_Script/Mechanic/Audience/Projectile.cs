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
        List<Collider> hits = Physics.OverlapSphere(transform.position, radius, mask).ToList();

        foreach (Collider c in hits.ToList())
        {
            if(c.transform.TryGetComponent(out Popularity pop))
            {
                pop.Value -= damage; //this is bad fix later
                c.GetComponent<Rigidbody>().AddExplosionForce(force, transform.position, radius);
            }
        }

        onExplode?.Invoke(this);
        gameObject.SetActive(false);
    }
}
