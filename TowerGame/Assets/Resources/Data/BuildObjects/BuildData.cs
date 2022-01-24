using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "BuildObject",menuName = "New Data/BuildObject")]
public class BuildData : ScriptableObject
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public Vector3 offset = new Vector3(0.5f,0.5f,0.5f);

    /// <summary>
    /// The amount of resources needed to create this object
    /// </summary>
    [SerializeField] public List<ItemCost> neededResources = new List<ItemCost>();
}

[Serializable]
public struct ItemCost
{
    public ItemData item;
    public int amount;
}
