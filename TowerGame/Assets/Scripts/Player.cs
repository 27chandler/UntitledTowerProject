using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement movement;
    private Gravity gravity;
    private Drag drag;
    [SerializeField] private Rotation rotation;
    [SerializeField] private Selector selector;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        gravity = GetComponent<Gravity>();
        drag = GetComponent<Drag>();
        rotation.SetRigidbody(GetComponent<Rigidbody>());
    }

    // Update is called once per frame
    void Update()
    {
        movement.RunInput();
        gravity.DoFall();
        drag.DoDrag();
        rotation.Rotate();

        if (Input.GetButtonDown("Select"))
            selector.SelectObject();
    }
}
