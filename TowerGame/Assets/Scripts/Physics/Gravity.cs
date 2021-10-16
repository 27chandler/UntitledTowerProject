using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : RigidBodyAffector
{
    [SerializeField] private float strength = 9.81f;
    [SerializeField] private float floorDistance = 1.0f;

    public void DoFall()
    {
        if (!Physics.Raycast(rb.position,-transform.up,floorDistance))
        {
            rb.AddForce(-transform.up * strength * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(rb.position, rb.position - new Vector3(0.0f, floorDistance, 0.0f));
        }
    }
}
