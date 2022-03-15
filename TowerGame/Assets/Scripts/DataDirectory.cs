using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataDirectory : MonoBehaviour
{
    [SerializeField] public List<BuildData> buildObjects = new List<BuildData>();
    [SerializeField] private UnityEvent changeSelectionEvent;
    [SerializeField] private int selectionIndex = 0;
    private bool isInited = false;

    public int SelectionIndex { get => selectionIndex; set => selectionIndex = value; }
    public bool IsInited { get => isInited; set => isInited = value; }

    // Start is called before the first frame update
    void Start()
    {
        BuildData[] loaded_objects = Resources.LoadAll<BuildData>("Data/BuildObjects");

        foreach (BuildData obj in loaded_objects)
        {
            buildObjects.Add(obj);
        }

        isInited = true;
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

    public BuildData FindObjectData(string id)
    {
        return buildObjects.Find(x => x.identifier == id);
    }

    public GameObject GetSelectedObjectPreview()
    {
        return buildObjects[selectionIndex].preview;
    }

    private void ClampSelection()
    {
        selectionIndex = Mathf.Clamp(selectionIndex,0, buildObjects.Count - 1);
    }
}
