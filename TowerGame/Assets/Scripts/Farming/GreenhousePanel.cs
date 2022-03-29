using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenhousePanel : MonoBehaviour
{
    [SerializeField] private UnlockStation unlockStation;
    [ReadOnly] [SerializeField] private List<UnlockSatellite> satellites = new List<UnlockSatellite>();
    [ReadOnly] [SerializeField] private List<PlantGrowth> plants = new List<PlantGrowth>();

    [SerializeField] private int updateTime = 5;
    
    [Header("UI")]
    [SerializeField] private Text harvestStatusText;
    [SerializeField] private string harvestStatus;
    private int readyPlantsCount = 0;
    private bool isUnlocked = false;

    public void DoUnlock()
    {
        if (!isUnlocked)
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

            isUnlocked = true;
            StartCoroutine(UpdateStep());
        }
    }

    public void HarvestAll()
    {
        foreach (var plant in plants)
        {
            plant.HarvestCheck();
        }
    }

    private IEnumerator UpdateStep()
    {
        while (true)
        {
            UpdateStatusText();
            yield return new WaitForSeconds(updateTime);
        }
    }

    private void UpdateStatusText()
    {
        readyPlantsCount = 0;
        foreach (var plant in plants)
        {
            if (plant.IsHarvestReady())
            {
                readyPlantsCount++;
            }
        }

        harvestStatusText.text = harvestStatus + readyPlantsCount.ToString() + "/" + plants.Count.ToString();
    }
}
