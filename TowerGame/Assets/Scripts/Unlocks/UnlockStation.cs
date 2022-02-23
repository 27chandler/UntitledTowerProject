using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockStation : MonoBehaviour
{
    [SerializeField] private string identifier = "Default";
    [SerializeField] private float searchRange = 5.0f;
    /// <summary>
    /// The interval in which the station will search
    /// for satellites
    /// </summary>
    [SerializeField] private float updateInterval = 10.0f;
    [ReadOnly] [SerializeField] public List<UnlockSatellite> satellites = new List<UnlockSatellite>();
    [ReadOnly] [SerializeField] private List<string> satelliteTypes = new List<string>();
    [ReadOnly] [SerializeField] private List<string> completedUnlocks = new List<string>();
    private IEnumerator SearchRoutine;

    public string Identifier { get => identifier; set => identifier = value; }

    // Start is called before the first frame update
    void Start()
    {
        UnlockSystem.Subscribe(this);

        SearchRoutine = SatelliteSearch();
        StartCoroutine(SearchRoutine);
    }

    public void UnlockNow(string unlock_name)
    {
        if (!completedUnlocks.Contains(unlock_name))
        {
            completedUnlocks.Add(unlock_name);
        }
    }

    public bool IsUnlocked(string unlock_name)
    {
        return completedUnlocks.Contains(unlock_name);
    }

    private IEnumerator SatelliteSearch()
    {
        while(SearchRoutine != null)
        {
            PurgeInvalidSatellites();

            Collider[] results = Physics.OverlapSphere(transform.position, 5.0f);

            foreach(var result in results)
            {
                UnlockSatellite satellite = result.GetComponentInParent<UnlockSatellite>();

                if ((satellite != null) && (!satellites.Contains(satellite)))
                {
                    foreach (var tag in satellite.tags)
                    {
                        if (satelliteTypes.Contains(tag))
                        {
                            satellites.Add(satellite);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void PurgeInvalidSatellites()
    {
        for (int i = satellites.Count - 1; i >= 0; i--)
        {
            if (satellites[i] == null)
            {
                satellites.RemoveAt(i);
            }
        }
    }

    public void AddSatelliteTag(string tag)
    {
        if (!satelliteTypes.Contains(tag))
        {
            satelliteTypes.Add(tag);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color draw_colour = Color.blue;
        draw_colour.a = 0.5f;
        Gizmos.color = draw_colour;

        Gizmos.DrawWireSphere(transform.position, searchRange);

        foreach (var satellite in satellites)
        {
            Gizmos.DrawLine(transform.position, satellite.transform.position);
        }
    }
}
