using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class SolarPanel : EnergySource
{
    [SerializeField] private List<LightDirections> lightSources = new List<LightDirections>();
    [SerializeField] private float checkDistance = 10.0f;
    [SerializeField] private LayerMask mask;

    protected override void Start()
    {
        CalculateLightStrength();
        UnityEvent check_event = DayCycle.Subscribe("Default", "Midday");
        check_event.AddListener(CheckLightLevels);

        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    private void CalculateLightStrength()
    {
        totalPower = 0.0f;

        foreach (var source in lightSources)
        {
            if (!Physics.Raycast(transform.position, source.direction, checkDistance, mask))
            {
                totalPower += source.strength;
            }
        }

        totalPower *= energyValue;

        EnergySystem.RecalculateEnergy();
    }

    private void CheckLightLevels()
    {
        CalculateLightStrength();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (var source in lightSources)
        {
            Gizmos.DrawLine(transform.position, transform.position + (source.direction.normalized * checkDistance));
        }
    }
}

[Serializable]
public struct LightDirections
{
    public Vector3 direction;
    public float strength;
}
