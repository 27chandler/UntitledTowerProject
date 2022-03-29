using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BLUEPRINT_CATEGORIES
{
    MISC,
    WALL,
    FLOOR,
    DECORATION,
    POWER,
}

public class DataDirectory : MonoBehaviour
{
    [SerializeField] public List<BuildData> buildObjects = new List<BuildData>();
    [SerializeField] public Dictionary<string, List<BuildData>> objectDirectory = new Dictionary<string, List<BuildData>>();
    [SerializeField] private UnityEvent changeSelectionEvent;
    [SerializeField] private int selectionIndex = 0;
    [SerializeField] private bool isCategoryMode = false;
    [SerializeField] private BLUEPRINT_CATEGORIES selectionCategory = BLUEPRINT_CATEGORIES.MISC;
    private bool isInited = false;

    public int SelectionIndex { get => selectionIndex; set => selectionIndex = value; }
    public bool IsInited { get => isInited; set => isInited = value; }

    // Start is called before the first frame update
    void Start()
    {
        BuildData[] loaded_objects = Resources.LoadAll<BuildData>("Data/BuildObjects");

        foreach (BuildData obj in loaded_objects)
        {
            if (obj.isEnabled)
            {
                buildObjects.Add(obj);

                if (objectDirectory.ContainsKey(obj.category.ToString()))
                {
                    objectDirectory[obj.category.ToString()].Add(obj);
                }
                else
                {
                    objectDirectory.Add(obj.category.ToString(), new List<BuildData>());
                    objectDirectory[obj.category.ToString()].Add(obj);
                }
                
            }
        }

        isInited = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCategoryMode)
        {
            if (Input.mouseScrollDelta.y > 0.0f)
            {
                NextInCategory(selectionCategory.ToString(), 1);
                ClampSelection();
                changeSelectionEvent?.Invoke();
            }

            if (Input.mouseScrollDelta.y < 0.0f)
            {
                NextInCategory(selectionCategory.ToString(), -1);
                ClampSelection();
                changeSelectionEvent?.Invoke();
            }
        }
        else
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
    }

    public void SetCategory(BLUEPRINT_CATEGORIES category)
    {
        selectionCategory = category;
        Debug.Log("Category is now " + selectionCategory);
        ClampSelection();
    }

    public void SetSelection(int index)
    {
        selectionIndex = index;
    }

    public void NextInCategory(string category, int delta)
    {
        selectionIndex += delta;
        //int category_max = objectDirectory[category].Count;

        //if (category_max <= 0)
        //{
        //    Debug.Log("Attempting to access an empty category");
        //    return;
        //}

        //int current_selection_in_category = FindIndexInCategory(category, buildObjects[selectionIndex].identifier);
        //current_selection_in_category = buildObjects.FindIndex(x => x.identifier == objectDirectory[category][current_selection_in_category + delta].identifier);

        //if (current_selection_in_category >= category_max)
        //{
        //    current_selection_in_category = category_max;
        //}
        //else if (current_selection_in_category < 0)
        //{
        //    current_selection_in_category = 0;
        //}

        //selectionIndex = current_selection_in_category;
    }

    //public BuildData GetSelectedObject()
    //{
    //    return buildObjects[selectionIndex];
    //}
    
    public BuildData GetSelectedObject()
    {
        return objectDirectory[selectionCategory.ToString()][selectionIndex];
    }

    public BuildData FindObjectData(string id)
    {
        BuildData return_data;

        foreach (var category in objectDirectory)
        {
            return_data = category.Value.Find(x => x.identifier == id);

            if (return_data != null)
            {
                return return_data;
            }
        }

        return null;
    }

    //private void ClampSelection()
    //{
    //    selectionIndex = Mathf.Clamp(selectionIndex, 0, buildObjects.Count - 1);
    //}

    private void ClampSelection()
    {
        selectionIndex = Mathf.Clamp(selectionIndex, 0, objectDirectory[selectionCategory.ToString()].Count - 1);
    }
}
