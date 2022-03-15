using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuildLimitSystem : MonoBehaviour
{
    [Serializable]
    public class Zone
    {
        public Vector3 center;
        public float width;
        public float height;
        public Collider bounds;
    }

    [SerializeField] private GameObject zoneObject;
    private static GameObject staticZoneObject;

    [SerializeField] private List<Zone> startingZones = new List<Zone>();
    private static List<Zone> zones = new List<Zone>();

    private static Transform limitAnchor;

    public static bool IsWithinLimits(Vector3 point)
    {
        foreach (var zone in zones)
        {
            if (zone.bounds.bounds.Contains(point))
            {
                return true;
            }
        }

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        staticZoneObject = zoneObject;
        limitAnchor = transform;
        zones.AddRange(startingZones);

        InitZones();
    }

    private void InitZones()
    {
        for (int i = 0; i < zones.Count; i++)
        {
            GameObject new_zone = Instantiate(zoneObject);
            new_zone.transform.position = zones[i].center;
            new_zone.transform.position += new Vector3(0.0f, zones[i].height / 2.0f, 0.0f);
            new_zone.transform.localScale = new Vector3(zones[i].width, zones[i].height, zones[i].width);
            new_zone.transform.SetParent(limitAnchor);
            zones[i].bounds = new_zone.GetComponent<Collider>();
        }
    }

    public static void GenerateNewBounds(BuildBounds bounds)
    {
        GameObject new_zone = Instantiate(staticZoneObject);
        new_zone.transform.position = bounds.transform.position;
        new_zone.transform.position += new Vector3(0.0f, bounds.Height / 2.0f, 0.0f);
        new_zone.transform.localScale = new Vector3(bounds.Width, bounds.Height, bounds.Width);
        new_zone.transform.SetParent(limitAnchor);

        Zone zone = new Zone();
        zone.bounds = new_zone.GetComponent<Collider>();
        zone.center = bounds.transform.position;
        zone.height = bounds.Height;
        zone.width = bounds.Width;
        zones.Add(zone);
    }
}
