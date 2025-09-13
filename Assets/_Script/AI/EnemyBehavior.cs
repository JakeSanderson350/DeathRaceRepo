using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    EnemyState currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.LAPPING:

                break;
            case EnemyState.DESTROYING:

                break;
            case EnemyState.STYLING:

                break;
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
