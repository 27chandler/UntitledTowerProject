using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroyObject : DestroyObject
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private DataDirectory directory;
    public override void DeleteObject(GameObject anchor, Vector3 position, BuildData data)
    {
        BuildData found_data = directory.FindObjectData(anchor.GetComponentInParent<ObjectMeta>().identifier);

        if (anchor.GetComponentInParent<ObjectMeta>().identifier != "Voxels")
        {
            inventory.AddResources(anchor, position, found_data);
        }

        base.DeleteObject(anchor, position, data);
    }
}
