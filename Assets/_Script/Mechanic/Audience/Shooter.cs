using ImprovedTimers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Projectle")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField, Tooltip("Radius")] float range;
    [SerializeField, Tooltip("Shots per second")] int fireRate;
    [SerializeField] float velocity;

    [Header("Targeting")]
    [SerializeField] Vector3 spawnPoint = Vector3.up;
    [SerializeField, Range(0, 1)] float minimumPopularity = 0.5f;

    FrequencyTimer shootTimer;
    OverlapTracker tracker;
    List<Projectile> activeProjectiles = new();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + spawnPoint, Vector3.one * 0.5f);
    }

    private void Awake()
    {
        tracker = gameObject.AddComponent<OverlapTracker>();
        shootTimer = new(fireRate);
        shootTimer.OnTick += Shoot;
    }

    private void OnDestroy()
    {
        shootTimer.Dispose();
    }

    private void Start()
    {
        shootTimer.Start();
    }

    void Shoot()
    {
        if(tracker.Tracks.Count == 0) 
            return;

        List<Transform> validTargets = tracker.Tracks.FindAll(x => x.TryGetComponent(out Popularity pop) && pop.Value > minimumPopularity);
        Transform target = null;

        foreach(Transform t in validTargets)
        {
            if(target == null)
            {
                target = t;
                continue;
            }

            if(t.GetComponent<Popularity>().Value <= target.GetComponent<Popularity>().Value)
                target = t;
        }

        Vector3 aimPosition = Targeter.FindIntercept(target.GetComponent<Rigidbody>(), transform, velocity);
        Projectile shot = Instantiate(projectilePrefab, transform.position + spawnPoint, Quaternion.Euler(transform.position + spawnPoint - aimPosition)).GetComponent<Projectile>();
        activeProjectiles.Add(shot);

        shot.GetComponent<Rigidbody>().velocity = shot.transform.up * velocity;
        shot.onExplode += ProjectileExploded;
    }

    void ProjectileExploded(Projectile boom)
    {
        activeProjectiles.Remove(boom);
    }
}
