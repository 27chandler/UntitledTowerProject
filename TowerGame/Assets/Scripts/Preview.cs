using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    [SerializeField] private GameObject previewObject;
    [SerializeField] private Material previewMat;
    [SerializeField] private DataDirectory directory;
    private Transform preview;
    public void SetPosition(GameObject anchor, Vector3 position)
    {
        if (preview == null)
        {
            RefreshPreview();
            preview = Instantiate(previewObject).transform;
            MeshRenderer[] meshes = preview.GetComponentsInChildren<MeshRenderer>();

            foreach (MeshRenderer mesh in meshes)
            {
                Material[] mats = mesh.materials;

                for (int i = 0; i < mats.Length; i++)
                {
                    mats[i] = previewMat;
                }

                mesh.materials = mats;
            }

            Collider[] colliders = preview.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }
        preview.position = position - directory.GetSelectedObject().offset;
    }

    public void RefreshPreview()
    {
        previewObject = directory.GetSelectedObject().prefab;
        if (preview != null)
            Destroy(preview.gameObject);
    }
}
