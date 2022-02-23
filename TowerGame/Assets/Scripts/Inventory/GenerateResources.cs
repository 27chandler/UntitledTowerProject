using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateResources : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private List<ItemCost> dailyItems = new List<ItemCost>();
    public void GiveDailyResources()
    {
        foreach (var item in dailyItems)
        {
            inventory.AddResources(item.item.name, item.amount, true);
        }
    }
}
