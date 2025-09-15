using ImprovedTimers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Projectle")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField, Tooltip("Radius")] float range;
    [SerializeField, Tooltip("Shot every x seconds")] int fireDelay;
    [SerializeField] float velocity;

    [Header("Targeting")]
    [SerializeField] Transform spawnPoint;
    [SerializeField, Range(0, 1)] float minimumPopularity = 0.5f;

    CountdownTimer shootTimer;
    OverlapTracker tracker;
    List<Projectile> activeProjectiles = new();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnPoint.position, Vector3.one * 0.5f);
    }

    private void Awake()
    {
        tracker = TryGetComponent(out OverlapTracker trk) ? trk : gameObject.AddComponent<OverlapTracker>();
        tracker.radius = range;
        shootTimer = new(fireDelay);
        shootTimer.OnTimerStop += Shoot;
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
        shootTimer.Reset();
        shootTimer.Start();

        if(tracker.Tracks.Count == 0) 
            return;

        List<Transform> validTargets = tracker.Tracks.FindAll(x => x.parent.TryGetComponent(out Popularity pop) && pop.Value <= minimumPopularity);
        Transform target = null;

        if(validTargets.Count <= 0)
            return;

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

        Vector3 aimPosition = Targeter.FindIntercept(target.GetComponent<Rigidbody>(), spawnPoint, velocity);
        Debug.DrawLine(spawnPoint.position, aimPosition, Color.yellow, fireDelay);

        Projectile shot = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        shot.transform.up = aimPosition - spawnPoint.position;
        activeProjectiles.Add(shot);

        shot.GetComponent<Rigidbody>().velocity = shot.transform.up * velocity;
        shot.onExplode += ProjectileExploded;
    }

    void ProjectileExploded(Projectile boom)
    {
        activeProjectiles.Remove(boom);
    }
}
