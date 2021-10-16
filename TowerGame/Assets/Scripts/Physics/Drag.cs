using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : RigidBodyAffector
{
    [SerializeField] private Vector3 dragStrength;

    public void DoDrag()
    {
        Vector3 velocity = rb.velocity;
        Vector3 drag = -(velocity/2.0f);

        drag.x *= (dragStrength.x + 1) * Time.deltaTime;
        drag.y *= (dragStrength.y + 1) * Time.deltaTime;
        drag.z *= (dragStrength.z + 1) * Time.deltaTime;

        rb.AddForce(drag);
    }
}
