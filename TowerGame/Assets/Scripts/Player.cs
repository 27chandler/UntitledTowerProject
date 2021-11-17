using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterMovement movement;
    private CharacterGravity gravity;
    [SerializeField] private Rotation rotation;
    [SerializeField] private Selector buildSelector;
    [SerializeField] private Selector buildStartpointSelector;
    [SerializeField] private Selector deleteSelector;
    [SerializeField] private Selector previewSelector;
    [SerializeField] private CreateObject objectCreator;
    [SerializeField] private Preview objectPreview;

    [Space]

    [SerializeField] private bool canPlaceObject = true;
    [SerializeField] private bool canDragPlaceObjects = false;

    public bool CanPlaceObject { get => canPlaceObject; set => canPlaceObject = value; }

    // Start is called before the first frame update
    void Start()
    {
        // Move to its own class later:
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        movement = GetComponent<CharacterMovement>();
        gravity = GetComponent<CharacterGravity>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.RunInput();
        gravity.DoFall();
        rotation.Rotate();

        if (canPlaceObject)
        {
            if (canDragPlaceObjects)
            {
                if (Input.GetButtonDown("Create"))
                    buildStartpointSelector.SelectObject();
            }
            if (Input.GetButtonUp("Create"))
                buildSelector.SelectObject();
        }



        if (Input.GetButtonDown("Delete"))
            deleteSelector.SelectObject();

        if (previewSelector.RangeCheck())
            previewSelector.SelectObject();

        if (Input.GetButtonDown("Rotate"))
        {
            objectCreator.RotateCreation(90.0f);
            objectPreview.RotatePreview(90.0f);
        }
    }
}
