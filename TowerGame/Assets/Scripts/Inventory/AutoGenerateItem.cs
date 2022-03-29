using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGenerateItem : MonoBehaviour
{
    [SerializeField] private List<ItemCost> items = new List<ItemCost>();
    private string segment;
    // Start is called before the first frame update
    void Start()
    {
        segment = DayCycle.CurrentTime("Default");
        DayCycle.Subscribe("Default", segment).AddListener(GenerateItems);
    }

    private void OnDestroy()
    {
        DayCycle.Subscribe("Default", segment).RemoveListener(GenerateItems);
    }

    public void GenerateItems()
    {
        foreach (var item in items)
        {
            Inventory.globalInventory.AddResources(item.item.name,item.amount,true);
        }
    }
}
