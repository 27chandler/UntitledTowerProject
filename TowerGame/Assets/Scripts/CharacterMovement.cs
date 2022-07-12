using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform headTarget;
    [SerializeField] private Transform bodyTarget;
    [SerializeField] private Rigidbody rb;

    private CharacterController controller;

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

        controller = GetComponent<CharacterController>();
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

        //controller.Move(direction * speed * Time.deltaTime);
        //rb.MovePosition(rb.position + (direction * speed * Time.deltaTime));
        rb.AddForce(direction * speed * Time.deltaTime);
    }
}
