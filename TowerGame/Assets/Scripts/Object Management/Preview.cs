using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preview : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material previewMat;
    [SerializeField] private Material invalidMat;

    [SerializeField] private GameObject previewObject;
    [SerializeField] private DataDirectory directory;
    [SerializeField] private Player player;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Vector3 previewScale = new Vector3(0.9f, 0.9f, 0.9f);
    private Transform preview;
    private Quaternion previewRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    [SerializeField] private List<TriggerCollisionList> freeSpaceChecker = new List<TriggerCollisionList>();

    private void Update()
    {
        previewObject.transform.rotation = previewRotation;
    }

    public void ShowPreview(GameObject anchor, Vector3 position, BuildData data)
    {
        ShowPreview(anchor, position);
    }

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

    public void SetPosition(GameObject anchor, Vector3 position, BuildData data)
    {
        SetPosition(anchor, position);
    }

    public void SetPosition(GameObject anchor, Vector3 position)
    {
        if (preview == null)
        {
            RefreshPreview();

            preview = Instantiate(previewObject).transform;
            preview.rotation = previewRotation;

            //ChangeScale();

            Material current_mat;
            ValidCheck(out current_mat);
            //SetAllMaterials(preview, current_mat);
            //DisableComponents(preview.gameObject);

            UpdateFreeSpaceChecker();
        }
        IsSpaceFreeCheck();

        preview.position = position - new Vector3(0.5f,0.5f,0.5f);
    }

    private bool ValidCheck (out Material mat)
    {
        bool is_valid = true;

        BuildData selected_obj = directory.GetSelectedObject();

        foreach (var cost in selected_obj.neededResources)
        {
            int inventory_amount = inventory.FindAmount(cost.item.name);

            if (cost.amount > inventory_amount)
            {
                is_valid = false;
                break;
            }
        }


        if (is_valid)
        {
            mat = previewMat;
            return true;
        }
        else
        {
            mat = invalidMat;
            return false;
        }
    }

    //private void ChangeScale()
    //{
    //    Transform[] children = preview.GetComponentsInChildren<Transform>();

    //    foreach (var child in children)
    //    {
    //        Vector3 new_scale = child.localScale;
    //        new_scale.x *= previewScale.x;
    //        new_scale.y *= previewScale.y;
    //        new_scale.z *= previewScale.z;

    //        child.localScale = new_scale;
    //    }

    //}

    private void UpdateFreeSpaceChecker()
    {
        freeSpaceChecker.Clear();
        Collider[] colliders = preview.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.gameObject.layer = LayerMask.NameToLayer("Default");
            collider.isTrigger = true;
            freeSpaceChecker.Add(collider.gameObject.AddComponent<TriggerCollisionList>());
        }
    }

    //private void SetAllMaterials(Transform target, Material mat)
    //{
    //    MeshRenderer[] meshes = target.GetComponentsInChildren<MeshRenderer>();

    //    foreach (MeshRenderer mesh in meshes)
    //    {
    //        Material[] mats = mesh.materials;

    //        for (int i = 0; i < mats.Length; i++)
    //        {
    //            mats[i] = mat;
    //        }

    //        mesh.materials = mats;
    //    }
    //}

    //private void DisableComponents(GameObject obj)
    //{
    //    MonoBehaviour[] monos = obj.GetComponentsInChildren<MonoBehaviour>();
    //    foreach (MonoBehaviour mono in monos)
    //    {
    //        mono.enabled = false;
    //    }
    //}

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
        previewRotation = previewObject.transform.rotation;

        previewObject = directory.GetSelectedObject().preview;
        if (preview != null)
        {
            Destroy(preview.gameObject);
        }

        IsSpaceFreeCheck();
    }

    public void RotatePreview(float rotation)
    {
        preview.rotation = previewRotation;

        previewObject.transform.RotateAround(transform.position, Vector3.up, rotation);
        previewRotation = previewObject.transform.rotation;
        RefreshPreview();
    }
}
