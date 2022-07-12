using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject waterPrefab;
    [SerializeField] private Vector3 raycastOffset = new Vector3(0.0f, -0.51f, 0.0f);
    private GameObject waterfall;

    private void OnEnable()
    {
        if (waterfall == null)
        {
            waterfall = Instantiate(waterPrefab);
            waterfall.GetComponent<Waterfall>().Spawner = this;        
        }

        RecalculateWater(waterfall);
    }

    private void OnDisable()
    {
        waterfall.SetActive(false);
    }

    public Transform RecalculateWater(GameObject water)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position + raycastOffset, Vector3.down, out hit);
        Vector3 hit_position = hit.point;
        float hit_distance = Vector3.Distance(transform.position, hit_position);

        water.transform.position = transform.position + (Vector3.down * (hit_distance / 2.0f + 0.1f));
        water.transform.localScale = new Vector3(water.transform.localScale.x, hit_distance, water.transform.localScale.z);
        water.transform.SetParent(transform);

        return hit.collider.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawRay(transform.position + raycastOffset, Vector3.down);
    }
}
