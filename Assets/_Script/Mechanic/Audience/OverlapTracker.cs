using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapTracker : MonoBehaviour
{
    public List<Transform> Tracks { get => tracks; }

    public float radius = 5f;

    [SerializeField] List<Transform> tracks = new();
    SphereCollider col;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Start()
    {
        col = gameObject.AddComponent<SphereCollider>();
        col.radius = radius;
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        tracks.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        tracks.Remove(other.transform);
        tracks.RemoveAll(x => !x);
    }
}
