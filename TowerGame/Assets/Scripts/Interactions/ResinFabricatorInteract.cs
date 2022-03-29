using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResinFabricatorInteract : MonoBehaviour, IClickable
{
    [SerializeField] private float holdTime = 3.0f;
    [SerializeField] private bool isUsingIndicator = false;
    [SerializeField] private Image progressIndicator;
    [SerializeField] private List<ItemCost> items = new List<ItemCost>();
    private float holdTimer = 0.0f;
    public void Hover()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (isUsingIndicator)
        {
            progressIndicator.fillAmount = holdTimer / holdTime;
        }
    }

    public void LeftClicked()
    {
        Debug.Log("Start Click!");
    }

    public void LeftClickHold()
    {
        holdTimer += Time.deltaTime;
        
        if (holdTimer > holdTime)
        {
            holdTimer = 0.0f;
            Debug.Log("Giving manual items");
            GenerateResin();
        }
    }

    private void GenerateResin()
    {
        foreach (var item in items)
        {
            Inventory.globalInventory.AddResources(item.item.name, item.amount, true);
        }
    }
}
