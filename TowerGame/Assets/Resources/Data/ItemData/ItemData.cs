using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Data/Item")]
public class ItemData : ScriptableObject
{
    [SerializeField] public string name;
    [SerializeField] public Texture icon;
}
