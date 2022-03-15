using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateResources : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private List<ItemCost> dailyItems = new List<ItemCost>();

    [Header("Modifiers")]
    [SerializeField] private static float dailyMultiplier = 1.0f;
    public void GiveDailyResources()
    {
        EnergySystem.RecalculateEnergy();

        foreach (var item in dailyItems)
        {
            int amount = Mathf.FloorToInt(item.amount * dailyMultiplier);

            inventory.AddResources(item.item.name, amount, true);
        }
    }

    /// <summary>
    /// Change the multiplier by the defined amount
    /// </summary>
    public static void ChangeMultiplier(float amount)
    {
        dailyMultiplier += amount;
    }

    private void Update()
    {
        dailyMultiplier = 1.0f + EnergySystem.Energy;
    }
}
