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
            slot.Amount.text = item.Value.amount.ToString();
            slot.Image.texture = item.Value.icon;
            slot.Label.text = item.Value.name;

            count++;
        }

        yield return null;
    }
}
