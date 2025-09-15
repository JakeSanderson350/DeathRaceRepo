using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Tooltip("Is this the final waypoint on the track before the finish line?")]
    [SerializeField] bool isFinal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponentInParent<EnemyBehavior>().ReachWaypoint(gameObject);
            if (isFinal)
            {
                other.GetComponentInParent<EnemyBehavior>().ResetWaypoints(gameObject);
            }
        }
    }
}
