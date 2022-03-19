using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySource : MonoBehaviour
{
    [SerializeField] protected ParticleSystem particles;
    [SerializeField] protected bool usesParticles = false;
    [SerializeField] protected float energyValue;
    [SerializeField] [ReadOnly] protected float totalPower;
    protected ParticleSystem.EmissionModule emmisionSystem;

    public float TotalPower { get => totalPower; set => totalPower = value; }

    protected virtual void Start()
    {
        if (usesParticles)
        {
            emmisionSystem = particles.emission;

            emmisionSystem.rateOverTimeMultiplier = totalPower * 10.0f;
        }

        EnergySystem.AddSource(this);
    }

    protected virtual void OnDestroy()
    {
        EnergySystem.RemoveSource(this);
    }
}
