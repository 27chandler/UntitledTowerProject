using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnlockSystem : object
{
    public static List<Unlockable> unlocks = new List<Unlockable>();

    public static void AddUnlock(Unlockable unlock)
    {
        unlocks.Add(unlock);
    }

    public static void Subscribe(UnlockStation station)
    {
        for (int i = 0; i < unlocks.Count; i++)
        {
            if (unlocks[i].StationTag == station.Identifier)
            {
                unlocks[i].Subscribe(station);
            }
        }
    }
}
