using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Movement movement;
    private Gravity gravity;
    private Drag drag;
    [SerializeField] private Rotation rotation;
    [SerializeField] private Selector buildSelector;
    [SerializeField] private Selector buildStartpointSelector;
    [SerializeField] private Selector deleteSelector;
    [SerializeField] private Selector previewSelector;
    // Start is called before the first frame update
    void Start()
    {
        // Move to its own class later:
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

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

        if (Input.GetButtonDown("Create"))
            buildStartpointSelector.SelectObject();
        if (Input.GetButtonUp("Create"))
            buildSelector.SelectObject();



        if (Input.GetButtonDown("Delete"))
            deleteSelector.SelectObject();

        if (previewSelector.RangeCheck())
            previewSelector.SelectObject();
    }
}
