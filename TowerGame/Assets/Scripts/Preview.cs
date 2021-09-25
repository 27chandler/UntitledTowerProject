using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    [SerializeField] private GameObject previewObject;
    [SerializeField] private Material previewMat;
    private Transform preview;
    public void SetPosition(GameObject anchor, Vector3 position)
    {
        if (preview == null)
        {
            preview = Instantiate(previewObject).transform;
            MeshRenderer mesh = preview.GetComponent<MeshRenderer>();
            mesh.material = previewMat;
            Collider collider = preview.GetComponent<Collider>();
            collider.enabled = false;
        }
        preview.position = position;
    }
}
