using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantGrowth : MonoBehaviour
{
    [SerializeField] private Transform plantMeshAnchor;
    [SerializeField] private Text growthStageText;
    [SerializeField] private string growthTextPrefix;
    private GameObject plantMesh;
    private int growthStage = 0;
    private bool isPlanted = false;
    // Start is called before the first frame update
    void Start()
    {
        DayCycle.Subscribe("Default", "TimeAdvance").AddListener(Grow);
        plantMesh = plantMeshAnchor.GetChild(0).gameObject;
        ShowPlantMesh(isPlanted);
        UpdateUI();
    }

    private void Grow()
    {
        growthStage++;
        UpdateUI();
    }

    /// <summary>
    /// I would have said "plant" the plant but then it would be
    /// void PlantPlant() or void Plant() :L
    /// </summary>
    public void SowPlant()
    {
        isPlanted = true;
        ShowPlantMesh(isPlanted);
        UpdateUI();
    }

    public void RemovePlant()
    {
        isPlanted = false;
        ShowPlantMesh(isPlanted);
        UpdateUI();
    }

    private void UpdateUI()
    {
        growthStageText.text = growthTextPrefix + growthStage.ToString();
    }

    private void ShowPlantMesh(bool is_shown)
    {
        plantMesh.SetActive(is_shown);
    }
}
