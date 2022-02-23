using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildObject : CreateObject
{
    [SerializeField] private Inventory playerInventory;
    // Start is called before the first frame update
    public override void PlaceObject(GameObject anchor, Vector3 position, BuildData data)
    {
        foreach (var resource in data.neededResources)
        {
            if (resource.amount > playerInventory.items[resource.item.name].amount)
            {
                Debug.Log("NOT ENOUGH RESOURCES!");
                return;
            }
        }

        playerInventory.ConsumeResources(anchor, position, data);
        base.PlaceObject(anchor, position);
    }
}
