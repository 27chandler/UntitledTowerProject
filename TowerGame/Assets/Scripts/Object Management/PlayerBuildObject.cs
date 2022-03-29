using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildObject : CreateObject
{
    [SerializeField] private Inventory playerInventory;
    // Start is called before the first frame update
    public override void PlaceObject(GameObject anchor, Vector3 position, BuildData data)
    {
        if (!ValidCheck(position))
        {
            Debug.Log("Outside of valid bounds!");
            return;
        }

        foreach (var resource in data.neededResources)
        {
            if (resource.amount > playerInventory.items[resource.item.name].amount)
            {
                Debug.Log("NOT ENOUGH RESOURCES!");
                return;
            }
        }

        RefreshSelection();
        playerInventory.ConsumeResources(anchor, position, data);
        base.PlaceObject(anchor, position);
    }

    /// <summary>
    /// Checks if the position the object is going to be placed
    /// is within one of the allowed limits
    /// </summary>
    private bool ValidCheck(Vector3 position)
    {
        return BuildLimitSystem.IsWithinLimits(position);
    }
}
