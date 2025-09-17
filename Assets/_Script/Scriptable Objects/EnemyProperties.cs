using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Enemy/Enemy Profile")]
public class EnemyProperties : ScriptableObject
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

    [Tooltip("Affects the tendency of an enemy to drive along an ideal path and make better" +
        "driving decisions.")]
    [Range(0f, 1f)]
    public float skill;


    // add car profile
    

}
