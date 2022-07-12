using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private Transform headTarget;
    [SerializeField] private Transform bodyTarget;

    [SerializeField] private float maxSpeed;

    // Is the player putting effort in to create movement?
    private bool isActivelyMoving = false;

    private void Start()
    {
        if (headTarget == null)
        {
            headTarget = transform;
        }

        if (bodyTarget == null)
        {
            bodyTarget = transform;
        }
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

        MovePlayer(direction,speed);
    }

    public void FloorFriction(float strength)
    {
        if (!isActivelyMoving)
        {
            Debug.Log("Applying friction: " + strength);
            rb.AddForce(-rb.velocity.normalized * strength * rb.velocity.magnitude * Time.deltaTime);
        }
    }

    public void MovePlayer(Vector3 direction, float speed)
    {
        //rb.AddForce(direction.normalized * speed * Time.deltaTime);
        float fallVelocity = rb.velocity.y;
        rb.velocity = new Vector3(0.0f, fallVelocity,0.0f) + (direction.normalized * speed);

        if (direction != Vector3.zero)
        {
            isActivelyMoving = true;
        }
        else
        {
            isActivelyMoving = false;
        }
    }

    public void Jump(Vector3 direction, float strength)
    {
        rb.AddForce(direction.normalized * strength);
    }
}
