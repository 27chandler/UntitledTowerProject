using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum INTERACTION_STATE
    {
        DEFAULT,
        BUILD
    }

    private CharacterMovement movement;
    private CharacterGravity gravity;
    [SerializeField] private Rotation rotation;
    [SerializeField] private Selector buildSelector;
    [SerializeField] private Selector buildStartpointSelector;
    [SerializeField] private Selector deleteSelector;
    [SerializeField] private Selector previewSelector;
    [SerializeField] private CreateObject objectCreator;
    [SerializeField] private Preview objectPreview;
    [SerializeField] private ObjectClicker objectClicker;

    [Space]

    [SerializeField] private INTERACTION_STATE mode = INTERACTION_STATE.DEFAULT;
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

        if (Input.GetButtonDown("SwapMode"))
        {
            mode++;
            if (mode > INTERACTION_STATE.BUILD)
            {
                mode = INTERACTION_STATE.DEFAULT;
            }
        }

        if (mode == INTERACTION_STATE.DEFAULT)
        {
            DefaultMode();
        }
        else if (mode == INTERACTION_STATE.BUILD)
        {
            BuildMode();
        }
    }

    private void DefaultMode()
    {
        if (Input.GetButtonDown("Select"))
        {
            objectClicker.Click();
        }
    }

    private void BuildMode()
    {
        if (canPlaceObject)
        {
            if (canDragPlaceObjects)
            {
                if (Input.GetButtonDown("Select"))
                {
                    buildStartpointSelector.SelectObject();
                }
            }
            if (Input.GetButtonUp("Select"))
            {
                buildSelector.SelectObject();
            }
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
