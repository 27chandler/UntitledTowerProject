using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisionList : MonoBehaviour
{
    private List<Collider> collisionList = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        collisionList.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (collisionList.Contains(other))
            collisionList.Remove(other);
        else
            Debug.LogWarning("Collider exited trigger without ever entering it");
    }

    public bool IsEmpty()
    {
        return collisionList.Count == 0;
    }
}
