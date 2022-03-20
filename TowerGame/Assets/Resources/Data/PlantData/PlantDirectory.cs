using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlantDirectory
{
    private static List<PlantData> plants = new List<PlantData>();

    private static bool isInited = false;

    static PlantDirectory()
    {
        PlantData[] loaded_objects = Resources.LoadAll<PlantData>("Data/PlantData");

        foreach (PlantData obj in loaded_objects)
        {
            plants.Add(obj);
        }

        isInited = true;
    }

    public static PlantData FindPlantData(string name)
    {
        return plants.Find(x => x.name == name);
    }

}
