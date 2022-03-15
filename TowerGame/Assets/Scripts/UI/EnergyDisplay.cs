using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDisplay : MonoBehaviour
{
    [SerializeField] private Text energyText;

    // Update is called once per frame
    void Update()
    {
        energyText.text = EnergySystem.Energy.ToString();
    }
}
