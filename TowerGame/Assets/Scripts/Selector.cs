using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] private float maxDistance = 5.0f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private GameObject selectedObject;

    public void SelectObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, maxDistance, mask);

        if (hit.collider != null)
        {
            selectedObject = hit.collider.gameObject;
        }
    }
}
