using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : RigidBodyAffector
{
    [SerializeField] private float speed;
    [SerializeField] private Transform headTarget;

    new private void Start()
    {
        if (headTarget == null)
            headTarget = transform;
        base.Start();
    }

    public void RunInput()
    {
        // Calculate forward ignoring downwards
        Vector3 flat_forwards = headTarget.forward;
        flat_forwards.y = 0.0f;
        flat_forwards.Normalize();

        Vector3 direction = new Vector3();
        direction += headTarget.right * Input.GetAxisRaw("Horizontal");
        direction += flat_forwards * Input.GetAxisRaw("Vertical");
        direction.Normalize();

        rb.AddForce(direction * speed * Time.deltaTime);
    }
}
