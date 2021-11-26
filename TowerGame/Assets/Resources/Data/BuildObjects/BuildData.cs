using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuildObject",menuName = "New Build Object")]
public class BuildData : ScriptableObject
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public Vector3 offset = new Vector3(0.5f,0.5f,0.5f);
}
