using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;
    [SerializeField] private DataDirectory directory;
    private bool isSingle = true;
    private Vector3 startPosition; // Starting point for a drag selection
    private Quaternion objectRotation = Quaternion.Euler(0.0f,0.0f,0.0f);
    public void PlaceObject(GameObject anchor, Vector3 position)
    {
        if (isSingle)
            Create(anchor, position);
        else
        {
            // Forcing the placement to start from a specific corner and
            // end at the opposite
            Vector3 end_corner;
            Vector3 start_corner;

            if (position.x < startPosition.x)
            {
                start_corner.x = position.x;
                end_corner.x = startPosition.x;
            }
            else
            {
                start_corner.x = startPosition.x;
                end_corner.x = position.x;
            }

            if (position.y < startPosition.y)
            {
                start_corner.y = position.y;
                end_corner.y = startPosition.y;
            }
            else
            {
                start_corner.y = startPosition.y;
                end_corner.y = position.y;
            }

            if (position.z < startPosition.z)
            {
                start_corner.z = position.z;
                end_corner.z = startPosition.z;
            }
            else
            {
                start_corner.z = startPosition.z;
                end_corner.z = position.z;
            }


            int height = (int)Mathf.Abs(end_corner.y - start_corner.y) + 1;
            int width = (int)Mathf.Abs(end_corner.x - start_corner.x) + 1;
            int depth = (int)Mathf.Abs(end_corner.z - start_corner.z) + 1;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int k = 0; k < depth; k++)
                    {
                        Create(anchor, start_corner + new Vector3((float)i, (float)j, (float)k));
                    }
                }
            }
            isSingle = true;
        }
    }

    public void SetStart(GameObject anchor, Vector3 position)
    {
        startPosition = position;
        isSingle = false;
    }

    private void Create(GameObject anchor, Vector3 position)
    {
        GameObject new_object = Instantiate(selectedObject, position - directory.GetSelectedObject().offset, objectRotation);
        new_object.transform.SetParent(anchor.transform);
    }

    public void RefreshSelection()
    {
        selectedObject = directory.GetSelectedObject().prefab;
    }

    public void RotateCreation(float rotation)
    {
        objectRotation *= Quaternion.AngleAxis(rotation, Vector3.up);
    }
}
