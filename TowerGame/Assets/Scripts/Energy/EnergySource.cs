using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySource : MonoBehaviour
{
    [SerializeField] protected float energyValue;
    [SerializeField] [ReadOnly] protected float totalPower;

    public float TotalPower { get => totalPower; set => totalPower = value; }

    protected virtual void Start()
    {
        EnergySystem.AddSource(this);
    }

    protected virtual void OnDestroy()
    {
        EnergySystem.RemoveSource(this);
    }
}
