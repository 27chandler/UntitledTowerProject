using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBounds : MonoBehaviour
{
    [SerializeField] private float width;
    [SerializeField] private float height;

    private Collider collider;

    public float Width { get => width; set => width = value; }
    public float Height { get => height; set => height = value; }

    private void Start()
    {
        BuildLimitSystem.GenerateNewBounds(this, out collider);
    }

    private void OnDestroy()
    {
        BuildLimitSystem.RemoveExistingBounds(collider);
    }
}
