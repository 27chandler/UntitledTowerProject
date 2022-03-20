using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private Transform plantMeshAnchor;
    [SerializeField] private Text growthStageText;
    [SerializeField] private Text plantInfoText;
    [SerializeField] private string growthTextPrefix;
    [SerializeField] private ParticleSystem harvestParticles;
    private GameObject plantMesh;
    private int growthStage = 0;
    private bool isPlanted = false;
    private PlantData currentPlant;
    // Start is called before the first frame update
    void Start()
    {
        DayCycle.Subscribe("Default", "Morning").AddListener(HarvestCheck);
        UpdateUI();
    }

    private void Grow()
    {
        if (isPlanted)
        {
            growthStage++;
        }
        UpdateUI();
    }
    
    public void HarvestCheck()
    {
        if (currentPlant == null)
        {
            return;
        }

        if (growthStage >= currentPlant.growthTime)
        {
            Harvest();
        }
    }

    public void Harvest()
    {
        if (growthStage >= currentPlant.growthTime)
        {
            // Give resources
            RemovePlant();
            StartCoroutine(HarvestParticles(2.0f));
            GiveResources(currentPlant);
        }
        else
        {
            // Early harvest, not grown enough :(
            RemovePlant();
        }

        UpdateUI();
    }

    private void GiveResources(PlantData plant_data)
    {
        foreach (var resource in plant_data.harvestResources)
        {
            Inventory.globalInventory.AddResources(resource.item.name, resource.amount, true);
        }
    }

    private IEnumerator HarvestParticles(float duration)
    {
        harvestParticles.Play();
        yield return new WaitForSeconds(duration);
        harvestParticles.Stop();
        yield return null;
    }

    /// <summary>
    /// I would have said "plant" the plant but then it would be
    /// void PlantPlant() or void Plant() :L
    /// </summary>
    public void SowPlant(string plant_name)
    {
        if (isPlanted)
        {
            return;
        }

        currentPlant = PlantDirectory.FindPlantData(plant_name);
        DayCycle.Subscribe("Default", currentPlant.growthSegment).AddListener(Grow);
        isPlanted = true;

        SetPlantMesh();
        UpdateUI();
    }

    public void RemovePlant()
    {
        isPlanted = false;
        growthStage = 0;
        DayCycle.Subscribe("Default", currentPlant.growthSegment).RemoveListener(Grow);
        RemovePlantMesh();
        UpdateUI();
    }

    private void UpdateUI()
    {
        string required_growth = (currentPlant == null ? "?" : currentPlant.growthTime.ToString());
        growthStageText.text = growthTextPrefix + growthStage.ToString() + "/" + required_growth;
    }

    public void SetPlantMesh()
    {
        if (plantMesh != null)
        {
            RemovePlantMesh();
        }

        GameObject plant_obj = Instantiate(currentPlant.prefab);
        plant_obj.transform.SetParent(plantMeshAnchor);
        plant_obj.transform.localPosition = Vector3.zero;

        plantMesh = plant_obj;
    }

    private void RemovePlantMesh()
    {
        Destroy(plantMesh);
    }
}
