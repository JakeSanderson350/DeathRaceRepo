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
    List<Transform> validTargets = new();
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnPoint.position, Vector3.one * 0.5f);
    }

    private void OnValidate()
    {
        tracker = TryGetComponent(out OverlapTracker trk) ? trk : gameObject.AddComponent<OverlapTracker>();
        tracker.radius = range;
    }

    private void Awake()
    {
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

        //clear valid targets list
        validTargets.Clear();
        foreach (var x in tracker.Tracks)
        {
            if(x.GetComponentInParent<Popularity>() != null)
                validTargets.Add(x.GetComponentInParent<Popularity>().transform);
        }
        
        //filter sort valid targets
        validTargets.RemoveAll(x => x.GetComponent<Popularity>().Value > minimumPopularity);

        if(validTargets.Count <= 0)
            return;

        Transform target = validTargets[0];
        validTargets.ForEach(x => target = x.GetComponent<Popularity>().Value <= target.GetComponent<Popularity>().Value ? x : target);

        //get aim pos of target
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
