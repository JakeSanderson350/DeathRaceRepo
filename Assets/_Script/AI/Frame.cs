using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Frame : MonoBehaviour
{
    [SerializeField] public ImpactProperties impactProperties;
    protected Popularity popularity;
    //protected Health health;

    public virtual void HandleImpact(Frame other)
    {

    }

    private void Start()
    {
        popularity = GetComponentInParent<Popularity>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Touch " + other.collider.gameObject.name + " with tag " + other.collider.gameObject.tag);

        if (other.collider.gameObject.CompareTag("Frame")) //UNITY IS DUMB, MAKE SURE TO ROUTE THROUGH COLLDER BEFORE GAMEOBJECT
        {
            HandleImpact(other.collider.gameObject.GetComponent<Frame>());
        }
    }
}
