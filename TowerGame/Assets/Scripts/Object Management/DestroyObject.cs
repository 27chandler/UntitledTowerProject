using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private bool doesDestroyParent = false;

    public virtual void DeleteObject(GameObject anchor, Vector3 position, BuildData data)
    {
        DeleteObject(anchor, position);
    }

    public void DeleteObject(GameObject anchor, Vector3 position)
    {
        GameObject object_to_destroy = anchor;

        GenerateChunk chunk = object_to_destroy.GetComponent<GenerateChunk>();

        if (chunk != null)
        {
            DestroyVoxel(chunk, position);
        }
        else if (doesDestroyParent)
        {
            bool is_destroy_finished = false;

            while (!is_destroy_finished)
            {
                if (object_to_destroy.GetComponent<ObjectMeta>() != null
                    || object_to_destroy.transform.parent == null)
                {
                    Destroy(object_to_destroy);
                    is_destroy_finished = true;
                }
                else
                {
                    object_to_destroy = object_to_destroy.transform.parent.gameObject;
                }
            }

        }
        else
        {
            Destroy(object_to_destroy);
        }
    }

    public void DestroyVoxel(GenerateChunk chunk, Vector3 position)
    {
        chunk.DeleteVoxel(chunk.FindGridPosition(position));
    }
}
