using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldSpaceClick : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    [SerializeField] private List<Transform> cursors;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Select"))
        {
            LeftClick();
        }


        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
        List<IClickable> buttons = new List<IClickable>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            IClickable button = result.gameObject.GetComponent<IClickable>();
            if (button != null)
            {
                buttons.Add(button);
            }
        }

        if (results.Count > 0)
        {
            foreach (Transform cursor in cursors)
            {
                cursor.position = results[0].worldPosition;
            }
        }

        if (buttons.Count > 0)
        {
            foreach (IClickable button in buttons)
            {
                button.Hover();
            }
        }
    }

    private void LeftClick()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = Input.mousePosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();
        List<IClickable> buttons = new List<IClickable>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            IClickable button = result.gameObject.GetComponent<IClickable>();
            if (button != null)
            {
                buttons.Add(button);
            }
        }

        if (results.Count > 0)
        {
            foreach (Transform cursor in cursors)
            {
                cursor.position = results[0].worldPosition;
            }
        }

        if (buttons.Count > 0)
        {
            foreach (IClickable button in buttons)
            {
                button.LeftClicked();
            }
        }
    }
}
