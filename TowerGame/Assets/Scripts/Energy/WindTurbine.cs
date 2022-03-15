using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : EnergySource
{
    private float heightMultiplier;
    // Start is called before the first frame update
    protected override void Start()
    {
        heightMultiplier = transform.position.y;
        totalPower = energyValue * (heightMultiplier + 1.0f);

        Debug.Log("Turbine power at: " + totalPower);

        base.Start();
    }
}
