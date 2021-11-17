using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterGravity : MonoBehaviour
{
    [SerializeField] private float strength = 9.81f;
    [SerializeField] private float floorDistance = 1.0f;
    [SerializeField] private bool doesDetectFloor = false;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
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
        controller.Move(-transform.up * strength * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (controller != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position - new Vector3(0.0f, floorDistance, 0.0f));
        }
    }
}
