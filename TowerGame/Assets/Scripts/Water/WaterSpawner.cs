using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject waterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject new_water = Instantiate(waterPrefab);
        RecalculateWater(new_water);
        new_water.GetComponent<Waterfall>().Spawner = this;
    }
    
    public Transform RecalculateWater(GameObject water)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit);
        Vector3 hit_position = hit.point;
        float hit_distance = Vector3.Distance(transform.position, hit_position);

        water.transform.position = transform.position + (Vector3.down * (hit_distance / 2.0f));
        water.transform.localScale = new Vector3(water.transform.localScale.x, hit_distance, water.transform.localScale.z);
        water.transform.SetParent(transform);

        return hit.collider.transform;
    }
}
