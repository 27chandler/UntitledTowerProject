using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Selector : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5.0f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private bool isSelectingAdjacent = false;
    [Space]
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private DataDirectory directory;
    [SerializeField] private UnityEvent<GameObject,Vector3> activationEvent;
    [SerializeField] private UnityEvent deactivationEvent;

    private bool isSomethingSelected = false;

    private BuildData objectData;

    public bool IsSomethingSelected { get => isSomethingSelected; set => isSomethingSelected = value; }

    public void SelectObject()
    {
        RaycastHit hit = CastRay();

        if (hit.collider != null)
        {
            isSomethingSelected = true;

            Vector3 select_position;
            if (isSelectingAdjacent)
                select_position = hit.point - (transform.forward * 0.1f);           
            else
                select_position = hit.point + (transform.forward * 0.1f);
            select_position.x = Mathf.Ceil(select_position.x)/* - WorldData.GridOffset*/;
            select_position.y = Mathf.Ceil(select_position.y)/* - WorldData.GridOffset*/;
            select_position.z = Mathf.Ceil(select_position.z)/* - WorldData.GridOffset*/;

            selectedObject = hit.collider.gameObject;
            activationEvent?.Invoke(selectedObject, select_position);

        }
        else
        {
            isSomethingSelected = false;
            deactivationEvent?.Invoke();
        }
    }

    // Checks if a valid object is within range of the selectors raycast
    public bool RangeCheck()
    {
        RaycastHit hit = CastRay();

        if (hit.collider != null)
            return true;
        else
        {
            isSomethingSelected = false;
            deactivationEvent?.Invoke();
            return false;
        }
    }

    private RaycastHit CastRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, maxDistance, mask);

        return hit;
    }
}
