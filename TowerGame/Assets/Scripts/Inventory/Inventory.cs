using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot
{
    public string name;
    public Texture icon;
    public int amount = 20;
}

public class Inventory : MonoBehaviour
{
    public Dictionary<string, InventorySlot> items = new Dictionary<string, InventorySlot>();
    private string inventoryId;
    [SerializeField] private InventoryUI inventoryUI;

    public string InventoryId { get => inventoryId; set => inventoryId = value; }

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI.Inventory = this;

        CreateDefaultItems();
    }

    private void CreateDefaultItems()
    {
        ItemData[] loaded_objects = Resources.LoadAll<ItemData>("Data/ItemData");

        foreach (ItemData item in loaded_objects)
        {
            InventorySlot new_slot = new InventorySlot();
            new_slot.name = item.name;
            new_slot.icon = item.icon;
            items.Add(new_slot.name, new_slot);
        }
    }

    public int FindAmount(string name)
    {
        if (items.ContainsKey(name))
        {
            return items[name].amount;
        }
        else
        {
            return 0;
        }
    }
}
