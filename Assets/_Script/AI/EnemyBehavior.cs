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
    GameObject[] waypointArr;
    List<GameObject> waypoints;
    List<GameObject> reachedWaypoints;
    GameObject finalWaypoint;
    float variance;
    
    

    // Start is called before the first frame update
    void Start()
    {
        waypointArr = GameObject.FindGameObjectsWithTag("Waypoint");
        waypoints.AddRange(waypointArr);
        variance = 1f / enemyData.skill;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.LAPPING:
                float dist = float.MaxValue;
                GameObject chosenWP = null;
                foreach (GameObject wp in waypoints)
                {
                    if (wp == finalWaypoint && waypoints.Count > 1)
                    {
                        continue;
                    }
                    if (Vector3.Distance(transform.position, wp.transform.position) <= dist)
                    {
                        goalPosition = wp.transform.position;
                        chosenWP = wp;
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
        if (other.CompareTag("Waypoint"))
        {
            
        }
    }

    public void ReachWaypoint(GameObject waypoint)
    {
        waypoints.Remove(waypoint);
        reachedWaypoints.Add(waypoint);
    }

    public void ResetWaypoints(GameObject finalWP)
    {
        foreach(GameObject wp in reachedWaypoints)
        {
            waypoints.Add(wp);
        }
        finalWaypoint = finalWP;
    }

    // Lapping is normal driving around the track, destroying is aiming to destroy other cars, styling is going for tricks
    public enum EnemyState
    {
        LAPPING,
        DESTROYING,
        STYLING
    }
}
