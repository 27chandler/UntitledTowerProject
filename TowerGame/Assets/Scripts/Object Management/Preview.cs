using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    [SerializeField] private GameObject previewObject;
    [SerializeField] private Material previewMat;
    [SerializeField] private DataDirectory directory;
    [SerializeField] private Player player;
    [SerializeField] private Vector3 previewScale = new Vector3(0.9f, 0.9f, 0.9f);
    private Transform preview;
    private Quaternion previewRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    private List<TriggerCollisionList> freeSpaceChecker = new List<TriggerCollisionList>();
    
    public void ShowPreview(GameObject anchor, Vector3 position)
    {
        if (preview != null)
        {
            MeshRenderer[] meshes = preview.GetComponentsInChildren<MeshRenderer>();

            foreach (var mesh in meshes)
            {
                mesh.enabled = true;
            }
        }
    }

    public void HidePreview()
    {
        if (preview != null)
        {
            MeshRenderer[] meshes = preview.GetComponentsInChildren<MeshRenderer>();

            foreach (var mesh in meshes)
            {
                mesh.enabled = false;
            }
        }
    }

    public void SetPosition(GameObject anchor, Vector3 position)
    {
        if (preview == null)
        {
            RefreshPreview();
            preview = Instantiate(previewObject).transform;
            preview.rotation = previewRotation;

            Vector3 new_scale = preview.localScale;
            new_scale.x *= previewScale.x;
            new_scale.y *= previewScale.y;
            new_scale.z *= previewScale.z;

            preview.localScale = new_scale;

            SetAllMaterials(preview, previewMat);

            freeSpaceChecker.Clear();
            Collider[] colliders = preview.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
            {
                collider.gameObject.layer = LayerMask.NameToLayer("Default");
                collider.isTrigger = true;
                freeSpaceChecker.Add(collider.gameObject.AddComponent<TriggerCollisionList>());
            }
        }
        IsSpaceFreeCheck();

        preview.position = position - directory.GetSelectedObject().offset;
    }

    private void SetAllMaterials(Transform target, Material mat)
    {
        MeshRenderer[] meshes = target.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer mesh in meshes)
        {
            Material[] mats = mesh.materials;

            for (int i = 0; i < mats.Length; i++)
            {
                mats[i] = mat;
            }

            mesh.materials = mats;
        }
    }

    private void IsSpaceFreeCheck()
    {
        bool is_space_clear = true;

        foreach (TriggerCollisionList triggers in freeSpaceChecker)
        {
            if (triggers.IsEmpty() == false)
            {
                is_space_clear = false;
                break;
            }
        }

        if (is_space_clear)
        {
            player.CanPlaceObject = true;
        }
        else
        {
            player.CanPlaceObject = false;
        }
    }

    public void RefreshPreview()
    {
        previewObject = directory.GetSelectedObject().prefab;
        if (preview != null)
            Destroy(preview.gameObject);
    }

    public void RotatePreview(float rotation)
    {
        previewObject.transform.RotateAround(transform.position, Vector3.up, rotation);
        //previewRotation *= Quaternion.AngleAxis(rotation, Vector3.up);
        previewRotation = previewObject.transform.rotation;
        RefreshPreview();
    }
}
