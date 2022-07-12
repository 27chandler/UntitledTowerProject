using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergyConsumer : MonoBehaviour
{
    [SerializeField] private float requiredEnergy;
    [SerializeField] private UnityEvent onPowered;
    [SerializeField] private UnityEvent onUnpowered;

    private bool isPowered = false;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (EnergySystem.ConsumeRealtimeEnergy(requiredEnergy * Time.deltaTime))
        {
            ChangePowerState(true);
        }
        else
        {
            ChangePowerState(false);
        }

    }

    private void ChangePowerState(bool state)
    {
        if (isPowered != state)
        {
            isPowered = state;
            if (isPowered)
            {
                onPowered.Invoke();
            }
            else
            {
                onUnpowered.Invoke();
            }
        }
    }
}
