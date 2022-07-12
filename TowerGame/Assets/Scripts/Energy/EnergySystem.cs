using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] private static float energy;
    [SerializeField] private static List<EnergySource> sources = new List<EnergySource>();

    private static float realtimeEnergy = 0.0f;

    public static float Energy { get => energy; set => energy = value; }
    public static float RealtimeEnergy { get => realtimeEnergy; set => realtimeEnergy = value; }

    public static void AddSource(EnergySource source)
    {
        sources.Add(source);
        RecalculateEnergy();
    }

    public static void RemoveSource(EnergySource source)
    {
        sources.Remove(source);
        RecalculateEnergy();
    }

    public static void RecalculateEnergy()
    {
        energy = 0.0f;

        foreach(var source in sources)
        {
            energy += source.TotalPower;
        }

        Debug.Log("Total energy is now: " + energy);
    }

    public static void AddRealtimeEnergy(float amount)
    {
        realtimeEnergy += amount;
    }

    public static bool ConsumeRealtimeEnergy(float amount)
    {
        if (realtimeEnergy >= amount)
        {
            realtimeEnergy -= amount;
            return true;
        }
        else
        {
            realtimeEnergy = 0.0f;
            return false;
        }
    }
}
