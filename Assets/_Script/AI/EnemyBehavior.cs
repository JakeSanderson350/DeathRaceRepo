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
    Waypoint[] waypointArr;
    List<GameObject> waypoints;
    List<GameObject> reachedWaypoints;
    GameObject finalWaypoint;
    float variance;
    
    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
        waypointArr = FindObjectsByType<Waypoint>(FindObjectsSortMode.None);
        waypoints = new List<GameObject>();
        reachedWaypoints = new List<GameObject>();
        for (int i = 0; i < waypointArr.Length; i++)
        {
            waypoints.Add(waypointArr[i].gameObject);
        }
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
                        dist = Vector3.Distance(transform.position, wp.transform.position);
                        chosenWP = wp;
                    }

                }
                carController.SetInputs(DetermineSteering(goalPosition), 1f, 0f);
                break;
            case EnemyState.DESTROYING:

                break;
            case EnemyState.STYLING:

                break;
        }
    }

    private void FixedUpdate()
    {
        carController.UpdateCarController();
    }

    void CalculateGoals(float accel, float brake, float drift)
    {
        float driftChance = enemyData.style * drift;

        
    }

    float DetermineSteering(Vector3 goalPos)
    {
        float steer = 0f;

        float angleBetween = Vector3.SignedAngle(transform.forward, goalPos, Vector3.up);
        Debug.Log(angleBetween);

        float t = Mathf.InverseLerp(-180f, 180f, angleBetween);
        steer = Mathf.Lerp(-1f, 1f, t);
        Debug.Log(steer);

        return -steer * 0.2f;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TrackZone"))
        {
            ZoneProfile zoneData = other.GetComponent<TrackZone>().profile;
            CalculateGoals(zoneData.accelerate, zoneData.brake, zoneData.drift);
            
        }
    }

    public void ReachWaypoint(GameObject waypoint)
    {
        waypoints.Remove(waypoint);
        reachedWaypoints.Add(waypoint);
        Debug.Log(waypoint.name);

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
