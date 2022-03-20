using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlant", menuName = "New Data/Plant")]
public class PlantData : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public string growthSegment = "TimeAdvance";
    [SerializeField] public int growthTime = 5;

    /// <summary>
    /// The resources obtained when the plant is harvested
    /// </summary>
    [SerializeField] public List<ItemCost> harvestResources = new List<ItemCost>();
}
