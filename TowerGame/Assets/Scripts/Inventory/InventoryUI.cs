using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform uiAnchor;
    [SerializeField] private GameObject uiSlot;

    [Header("Modifiers")]
    [SerializeField] private float seperation = 105.0f;

    private Inventory inventory;
    private List<ItemSlot> slots = new List<ItemSlot>();

    public Inventory Inventory { get => inventory; set => inventory = value; }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetupUI());
    }

    private IEnumerator SetupUI()
    {
        int count = 0;

        if (inventory == null)
            yield return null;

        foreach (var item in inventory.items)
        {
            GameObject new_ui_slot = Instantiate(uiSlot);
            new_ui_slot.transform.parent = uiAnchor;
            new_ui_slot.transform.localPosition = Vector3.zero + (Vector3.down * seperation * count);

            ItemSlot slot = new_ui_slot.GetComponent<ItemSlot>();
            AssignSlotValues(slot, item.Value);

            slots.Add(slot);

            count++;
        }

        yield return null;
    }

    private void AssignSlotValues(ItemSlot slot, InventorySlot item)
    {
        slot.Amount.text = item.amount.ToString();
        slot.Image.texture = item.icon;
        slot.Label.text = item.name;
    }

    public void RefreshUI()
    {
        int count = 0;
        foreach (var item in inventory.items)
        {
            AssignSlotValues(slots[count], item.Value);
            count++;
        }
    }
}
