using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataDirectory : MonoBehaviour
{
    [SerializeField] private List<BuildData> buildObjects = new List<BuildData>();
    [SerializeField] private UnityEvent changeSelectionEvent;
    [SerializeField] private int selectionIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        BuildData[] loaded_objects = Resources.LoadAll<BuildData>("Data/BuildObjects");

        foreach (BuildData obj in loaded_objects)
        {
            buildObjects.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0.0f)
        {
            selectionIndex++;
            ClampSelection();
            changeSelectionEvent?.Invoke();
        }

        if (Input.mouseScrollDelta.y < 0.0f)
        {
            selectionIndex--;
            ClampSelection();
            changeSelectionEvent?.Invoke();
        }
    }

    public BuildData GetSelectedObject()
    {
        return buildObjects[selectionIndex];
    }

    private void ClampSelection()
    {
        selectionIndex = Mathf.Clamp(selectionIndex,0, buildObjects.Count - 1);
    }
}
