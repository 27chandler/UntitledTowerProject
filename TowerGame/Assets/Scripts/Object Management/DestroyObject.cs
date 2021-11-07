using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void DeleteObject(GameObject anchor, Vector3 position)
    {
        Destroy(anchor);
    }
}
