using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginDisplayGizmo : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private Vector3 origin;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireCube(origin, Vector3.one);
    }
}
