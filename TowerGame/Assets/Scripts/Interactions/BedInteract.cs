using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteract : MonoBehaviour, IClickable
{
    [SerializeField] private UnlockStation localStation;
    [SerializeField] private string bedroomWakeup;
    [SerializeField] private string oversleepWakeup;
    /// <summary>
    /// The earliest point in which the player can go to sleep
    /// </summary>
    [SerializeField] private string earliestSleep;
    public void Hover()
    {
        Debug.Log("Hover Bed");
    }

    public void LeftClicked()
    {
        if (DayCycle.cycles["Default"].IsAfter(earliestSleep))
        {
            Sleep();
        }
    }

    public void LeftClickHold()
    {

    }

    private void Sleep()
    {
        if (localStation.IsUnlocked("Bedroom"))
        {
            DayCycle.cycles["Default"].SkipTo(bedroomWakeup);
        }
        else
        {
            DayCycle.cycles["Default"].SkipTo(oversleepWakeup);
        }
    }
}
