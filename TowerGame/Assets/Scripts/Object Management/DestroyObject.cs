using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private bool doesDestroyParent = false;

    public void DeleteObject(GameObject anchor, Vector3 position, BuildData data)
    {
        DeleteObject(anchor, position);
    }

    public void DeleteObject(GameObject anchor, Vector3 position)
    {
        if (doesDestroyParent)
        {
            if (anchor.transform.parent == null)
            {
                Destroy(anchor);
            }
            else
            {
                Destroy(anchor.transform.parent.gameObject);
            }
        }
        else
        {
            Destroy(anchor);
        }
    }
}
