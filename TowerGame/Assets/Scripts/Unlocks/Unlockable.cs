using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unlockable : MonoBehaviour
{
    [SerializeField] private string unlockName;
    [Header("Subscribed stations:")]
    [ReadOnly][SerializeField] private List<UnlockStation> stations = new List<UnlockStation>();
    [ReadOnly] [SerializeField] private bool isUnlocked = false;
    [SerializeField] private bool startCheck = false;
    [Space]
    [SerializeField] private string stationTag = "";
    [SerializeField] private List<Requirement> requirements = new List<Requirement>();

    public string StationTag { get => stationTag; set => stationTag = value; }
    public bool IsUnlocked { get => isUnlocked; set => isUnlocked = value; }

    // Start is called before the first frame update
    void Start()
    {
        UnlockSystem.AddUnlock(this);
    }

    private void Update()
    {
        if (startCheck)
        {
            startCheck = false;
            CheckUnlockRequirements();
        }
    }

    public void Subscribe(UnlockStation station)
    {
        stations.Add(station);

        foreach (var requirement in requirements)
        {
            station.AddSatelliteTag(requirement.tag);
        }

        PurgeInvalidSubscribers();
    }

    private void PurgeInvalidSubscribers()
    {
        for (int i = stations.Count - 1; i >= 0; i--)
        {
            if (stations[i] == null)
            {
                stations.RemoveAt(i);
            }
        }
    }

    public void CheckUnlockRequirements()
    {
        PurgeInvalidSubscribers();

        foreach (var station in stations)
        {
            List<RequirementProgress> progress = new List<RequirementProgress>();

            foreach (var requirement in requirements)
            {
                RequirementProgress new_progress = new RequirementProgress();
                new_progress.requirement = requirement;
                new_progress.amount = 0;
                progress.Add(new_progress);
            }

            foreach (var satellite in station.satellites)
            {
                RequirementProgress progress_update = progress.Find(x => satellite.tags.Contains(x.requirement.tag));
                progress_update.amount++;
            }

            bool is_unlocked = true;
            foreach (var requirement_progress in progress)
            {
                if (requirement_progress.amount < requirement_progress.requirement.quantity)
                {
                    is_unlocked = false;
                }
            }

            if (is_unlocked)
            {
                isUnlocked = true;
                station.UnlockNow(unlockName);
                return;
            }
        }

    }
}

[Serializable]
public struct Requirement
{
    public string tag;
    public int quantity;
}

public class RequirementProgress
{
    public Requirement requirement;
    public int amount;
}