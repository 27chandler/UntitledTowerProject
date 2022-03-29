using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    private WaterSpawner spawner;
    private Transform floor;
    private Waterway waterway;

    private Vector3 lastPostion;

    public WaterSpawner Spawner { get => spawner; set => spawner = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (spawner != null)
        {
            Debug.Log("Recalculating water", this);
            floor = spawner.RecalculateWater(gameObject);
            lastPostion = floor.transform.position;
        }
    }

    public void Update()
    {
        if (floor == null)
        {
            floor = spawner.RecalculateWater(gameObject);
            CacheWaterway(floor.gameObject);
            lastPostion = floor.transform.position;
        }
        else
        {
            if (lastPostion != floor.transform.position)
            {
                floor = spawner.RecalculateWater(gameObject);
                lastPostion = floor.transform.position;
            }
        }
    }

    private void CacheWaterway(GameObject obj)
    {
        Waterway waterway = obj.GetComponent<Waterway>();
        this.waterway = waterway;
    }
}
