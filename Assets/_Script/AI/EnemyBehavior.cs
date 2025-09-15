using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] EnemyState currentState;
    [SerializeField] EnemyData enemyData;
    CarController carController;

    float velocityGoal;
    Vector3 goalPosition;
    bool isDrifting;

    float currentSteer;
    float currentGas;
    float currentBrake;
    Waypoint[] waypoints;
    float variance;
    

    // Start is called before the first frame update
    void Start()
    {
        waypoints = FindObjectsOfType<Waypoint>();
        variance = 1f / enemyData.skill;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.LAPPING:
                float dist = float.MaxValue;
                foreach (Waypoint wp in waypoints)
                {
                    if (Vector3.Distance(transform.position, wp.transform.position) <= dist)
                    {
                        goalPosition = wp.transform.position;
                    }
                }
                break;
            case EnemyState.DESTROYING:

                break;
            case EnemyState.STYLING:

                break;
        }
    }

    void CalculateGoals(float accel, float brake, float drift)
    {
        float driftChance = enemyData.style * drift;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TrackZone"))
        {
            ZoneProfile zoneData = other.GetComponent<TrackZone>().profile;
            CalculateGoals(zoneData.accelerate, zoneData.brake, zoneData.drift);
        }
    }

    // Lapping is normal driving around the track, destroying is aiming to destroy other cars, styling is going for tricks
    public enum EnemyState
    {
        LAPPING,
        DESTROYING,
        STYLING
    }
}
