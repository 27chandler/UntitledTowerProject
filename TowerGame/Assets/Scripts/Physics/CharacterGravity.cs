using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterGravity : MonoBehaviour
{
    [SerializeField] private float strength = 9.81f;
    [SerializeField] private float floorDistance = 1.0f;
    [SerializeField] private bool doesDetectFloor = false;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void DoFall()
    {
        if (doesDetectFloor)
        {
            if (!Physics.Raycast(transform.position, -transform.up, floorDistance))
            {
                Fall();
            }
        }
        else
        {
            Fall();
        }
    }

    private void Fall()
    {
        Vector3 current_velocity = rb.velocity;
        current_velocity.y += (strength * Time.deltaTime);
        rb.AddForce(new Vector3(0.0f, (strength * Time.deltaTime), 0.0f));
    }

    private void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0.0f, floorDistance, 0.0f));
        }
    }
}
