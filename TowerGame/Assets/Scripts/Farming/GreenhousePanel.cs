using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenhousePanel : MonoBehaviour
{
    [SerializeField] private UnlockStation unlockStation;
    [ReadOnly] [SerializeField] private List<UnlockSatellite> satellites = new List<UnlockSatellite>();
    [ReadOnly] [SerializeField] private List<PlantGrowth> plants = new List<PlantGrowth>();

    public void DoUnlock()
    {
        satellites.AddRange(unlockStation.Satellites);
        plants.Clear();

        foreach (var satellite in satellites)
        {
            PlantGrowth new_plant = satellite.GetComponentInChildren<PlantGrowth>();
            if (new_plant != null)
            {
                plants.Add(new_plant);
            }
        }
    }

    public void HarvestAll()
    {
        foreach (var plant in plants)
        {
            plant.HarvestCheck();
        }
    }
}
