using UnityEngine;

public class Targeter 
{
    public static Vector3 FindIntercept(Rigidbody target, Transform shooter, float ballisticVelocity)
    {
        Vector3 intercept = target.transform.position;
        float distance = Vector3.Distance(intercept, shooter.transform.position);
        intercept += target.velocity * (ballisticVelocity / distance);
        return intercept;
    }
}
