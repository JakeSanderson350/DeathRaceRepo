using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Enemy/Enemy Profile")]
public class EnemyData : ScriptableObject
{
    [Header("Behavior")]
    [Tooltip("Affects the likelihood of an enemy to attempt to target and destroy a nearby car" +
        "as a method of increasing audience favor.")]
    [Range(0f, 1f)]
    public float aggression;
    [Tooltip("Affects the likelihood of an enemy to attempt tricks as a method of increasing" +
        "audience favor.")]
    [Range(0f, 1f)]
    public float style;


    [Header("Stats")]
    // if we have a different method of handling car stats
    // I will change this to link to another SO with the type of car they have
    public float acceleration;


}
