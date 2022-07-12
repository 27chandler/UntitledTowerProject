using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterway : MonoBehaviour
{
    [SerializeField] private GameObject water;
    [SerializeField] private WaterSpawner waterfallSpawner;
    [SerializeField] private LayerMask neighbourLayers;
    private Neighbourhood neighbourhood;
    private bool isWaterSupplied = false;
    private bool hasNeighbourhood = false;

    private Dictionary<string, Waterway> neighbours = new Dictionary<string, Waterway>();

    public bool HasNeighbourhood { get => hasNeighbourhood; set => hasNeighbourhood = value; }
    public Neighbourhood Neighbourhood { get => neighbourhood; set => neighbourhood = value; }

    private void Start()
    {
        CheckAllNeighbours();
    }

    private void OnTriggerEnter(Collider other)
    {
        Waterfall waterfall = other.GetComponent<Waterfall>();

        if (waterfall != null)
        {
            SetWaterState(true,true);
            neighbourhood.IsWet = true;
            neighbourhood.RefreshWaterways();
        }
    }

    public void ResetNeighbourhood()
    {
        neighbourhood = null;
    }

    public void CheckAllNeighbours()
    {
        neighbourhood = null;

        CheckNeighbours(new Vector3(1.0f, 0.0f, 0.0f), "E");
        CheckNeighbours(new Vector3(-1.0f, 0.0f, 0.0f), "W");
        CheckNeighbours(new Vector3(0.0f, 0.0f, 1.0f), "N");
        CheckNeighbours(new Vector3(0.0f, 0.0f, -1.0f), "S");

        if (neighbourhood == null)
        {
            CreateNewNeighbourhood();
        }
    }

    private void OnWet()
    {
        waterfallSpawner.enabled = true;
    }

    private void OnDry()
    {
        waterfallSpawner.enabled = false;
    }

    private void SetWaterState(bool state, bool isSupplied = false)
    {
        isWaterSupplied = isSupplied;

        if (water == null)
        {
            return;
        }

        water.SetActive(state);

        if (state)
        {
            OnWet();
        }
        else
        {
            OnDry();
        }
    }

    private void CreateNewNeighbourhood()
    {
        neighbourhood = new Neighbourhood();
        neighbourhood.AddWaterway(this);

        //neighbourhood.debugColour = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        neighbourhood.debugColour = Random.ColorHSV(0.0f, 1.0f, 0.0f, 1.0f, 0.0f, 1.0f, 1.0f, 1.0f);
    }

    public void Refresh()
    {
        SetWaterState(neighbourhood.IsWet);
    }

    public void AddToNeighbourhood(Waterway waterway)
    {
        neighbourhood.AddWaterway(waterway);
    }

    private void CheckNeighbours(Vector3 offset, string direction)
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + offset, new Vector3(0.3f,0.5f,0.3f),new Quaternion(), neighbourLayers);

        foreach (var col in colliders)
        {
            Waterway neighbour = col.GetComponent<Waterway>();

            if ((neighbour != null) && (neighbour != this))
            {
                if (neighbour.neighbourhood == null)
                {
                    neighbour.CreateNewNeighbourhood();
                }

                if (neighbourhood != null && neighbour.neighbourhood != neighbourhood)
                {
                    MergeNeighbourhoods(neighbour.neighbourhood);
                }

                if (neighbour.neighbourhood != null)
                {
                    Debug.Log("Neighbour found!");
                    neighbourhood = neighbour.neighbourhood;
                    AddToNeighbourhood(this);
                    SetWaterState(neighbourhood.IsWet);

                    Debug.Log("Adding neighbour to: " + direction);
                    if (!neighbours.ContainsKey(direction))
                    {
                        neighbours.Add(direction, neighbour);
                    }
                    else
                    {
                        neighbours[direction] = neighbour;
                    }
                }
            }
        }
    }

    private void MergeNeighbourhoods(Neighbourhood alternate_neighbourhood)
    {
        // Swaps the neighbour to be merged depending on which one is smaller
        if (alternate_neighbourhood.waterways.Count > neighbourhood.waterways.Count)
        {
            Neighbourhood temporary_neighbourhood = alternate_neighbourhood;
            alternate_neighbourhood = neighbourhood;
            neighbourhood = temporary_neighbourhood;
        }

        foreach (var waterway in alternate_neighbourhood.waterways)
        {
            if (waterway != null)
            {
                neighbourhood.AddWaterway(waterway);
            }
        }

        if (neighbourhood.IsWet || alternate_neighbourhood.IsWet)
        {
            neighbourhood.IsWet = true;
        }

        neighbourhood.RefreshWaterways();
    }

    private void OnDrawGizmos()
    {
        if (neighbourhood != null)
        {
            Gizmos.color = neighbourhood.debugColour;
            Gizmos.DrawSphere(transform.position, 0.3f);
            if (isWaterSupplied)
            {
                Gizmos.DrawLine(transform.position, transform.position + Vector3.up);
            }
        }
    }

    private void OnDestroy()
    {
        if (neighbourhood != null)
        {
            neighbourhood.RemoveWaterway(this);
        }

        neighbourhood.RecalculateAll();
    }
}

public class Neighbourhood
{
    public List<Waterway> waterways = new List<Waterway>();
    private bool isWet = false;

    public bool IsWet { get => isWet; set => isWet = value; }

    public Color debugColour;

    public void AddWaterway(Waterway waterway)
    {
        if (!waterways.Contains(waterway))
        {
            waterways.Add(waterway);
            waterway.Neighbourhood = this;
        }
    }

    public void RemoveWaterway(Waterway waterway)
    {
        if (waterways.Contains(waterway))
        {
            waterways.Remove(waterway);
        }
    }

    public void RefreshWaterways()
    {
        foreach (var waterway in waterways)
        {
            waterway.Refresh();
        }
    }

    public void RecalculateAll()
    {
        Debug.Log("Recalulating neighbours");
        List<Waterway> not_calculated = new List<Waterway>();

        foreach (var waterway in waterways)
        {
            not_calculated.Add(waterway);
            waterway.ResetNeighbourhood();
        }

        waterways.Clear();

        foreach (var waterway in not_calculated)
        {
            waterway.CheckAllNeighbours();
        }
    }
}
