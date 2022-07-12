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
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private float jumpStrength;
    [SerializeField] private float frictionStrength;
    private CharacterGravity gravity;
    [SerializeField] private Rotation rotation;
    [SerializeField] private Selector buildSelector;
    [SerializeField] private Selector buildStartpointSelector;
    [SerializeField] private Selector deleteSelector;
    [SerializeField] private Selector previewSelector;
    [SerializeField] private CreateObject objectCreator;
    [SerializeField] private Preview objectPreview;
    [SerializeField] private ObjectClicker objectClicker;

    [SerializeField] private LayerMask floorLayers;
    [SerializeField] private float floorDistance = 1.0f;
    [SerializeField] private bool doesDetectFloor = false;

    [Space]

    public int shownTab = 0;
    [SerializeField] public INTERACTION_STATE mode = INTERACTION_STATE.DEFAULT;
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
        //movement.RunInput();
        playerMove.RunInput();

        if (Input.GetButtonDown("Jump") && doesDetectFloor)
        {
            playerMove.Jump(Vector3.up, jumpStrength);
        }

        //if (Physics.Raycast(transform.position, -transform.up, floorDistance, floorLayers))
        if (Physics.CheckSphere(transform.position + new Vector3(0.0f,-floorDistance,0.0f),0.5f, floorLayers))
        {
            doesDetectFloor = true;
        }
        else
        {
            doesDetectFloor = false;
        }
        //gravity.DoFall();
        //rotation.Rotate();

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

        if (Input.GetButtonDown("NextTab"))
        {
            Debug.Log("Next tab pressed");
            shownTab++;

            if (shownTab >= System.Enum.GetNames(typeof(BLUEPRINT_CATEGORIES)).Length)
            {
                shownTab = 0;
            }
        }

        if (Input.GetButtonDown("PreviousTab"))
        {
            Debug.Log("Previous tab pressed");
            shownTab--;

            if (shownTab < 0)
            {
                shownTab = System.Enum.GetNames(typeof(BLUEPRINT_CATEGORIES)).Length - 1;
            }
        }
    }

    private void FixedUpdate()
    {
        if (doesDetectFloor)
        {
            OnTouchingFloor();
        }
    }

    private void OnTouchingFloor()
    {
        //playerMove.FloorFriction(frictionStrength);
    }

    private void DefaultMode()
    {
        if (Input.GetButtonDown("Select"))
        {
            objectClicker.Click();
        }
        if (Input.GetButton("Select"))
        {
            objectClicker.HoldClick();
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
        {
            deleteSelector.SelectObject();
        }

        if (previewSelector.RangeCheck())
        {
            previewSelector.SelectObject();
        }

        if (Input.GetButtonDown("Rotate"))
        {
            objectCreator.RotateCreation(90.0f);
            objectPreview.RotatePreview(90.0f);
        }
    }

    private void OnDrawGizmos()
    {
        if (doesDetectFloor)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0.0f, floorDistance, 0.0f));
    }
}
