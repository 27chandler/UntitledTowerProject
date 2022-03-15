using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObjectUI : UIView
{
    [SerializeField] private DataDirectory directory;

    [Header("Modifiers")]
    [SerializeField] private float seperation = 105.0f;
    [SerializeField] private float scrollPadding = 40.0f;
    [Tooltip("How fast the scroll speeds up to its maximum")]
    [SerializeField] private float scrollAcceleration = 0.2f;
    [Tooltip("The maximum speed scrolling can do")]
    [SerializeField] private float scrollMaxSpeed = 20.0f;

    private List<ItemSlot> slots = new List<ItemSlot>();
    private int cachedSelection = -1;
    private Vector3 anchorStartPos;
    [SerializeField][ReadOnly] private float scrollSpeed = 0.0f;
    [SerializeField] [ReadOnly] private float offset = 0.0f;
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        anchorStartPos = uiAnchor.localPosition;
        cachedSelection = directory.SelectionIndex;
    }

    protected void Update()
    {
        base.Update();
        if (slots.Count > 0)
        {
            if (cachedSelection != directory.SelectionIndex)
            {
                cachedSelection = directory.SelectionIndex;
                RefreshUI();
            }

            if (slots[cachedSelection].transform.position.y - scrollPadding <= 0.0f)
            {
                offset += scrollSpeed += scrollAcceleration;

                if (scrollSpeed > scrollMaxSpeed)
                {
                    scrollSpeed = scrollMaxSpeed;
                }
            }
            else if (slots[cachedSelection].transform.position.y + scrollPadding >= Screen.height)
            {
                offset -= scrollSpeed += scrollAcceleration;

                if (scrollSpeed < -scrollMaxSpeed)
                {
                    scrollSpeed = -scrollMaxSpeed;
                }
            }
            else
            {
                scrollSpeed = 0.0f;
            }
            uiAnchor.transform.localPosition = anchorStartPos + new Vector3(0.0f, offset, 0.0f);
        }
    }

    protected override IEnumerator SetupUI()
    {
        int count = 0;

        while (!directory.IsInited)
        {
            yield return null;
        }

        foreach (var obj in directory.buildObjects)
        {
            GameObject new_ui_slot = Instantiate(uiSlot);
            new_ui_slot.transform.parent = uiAnchor;

            ItemSlot slot = new_ui_slot.GetComponent<ItemSlot>();
            AssignSlotValues(slot, obj, count == cachedSelection);
            slot.Index = count;

            slots.Add(slot);

            count++;
        }

        yield return null;
    }

    private Vector3 CalculatePosition(int index)
    {
        return Vector3.zero + (Vector3.down * seperation * index);
    }

    private void AssignSlotValues(ItemSlot slot, BuildData data, bool is_selected = false)
    {
        slot.Label.text = data.name;

        if (is_selected)
        {
            slot.Select(true);
        }
        else
        {
            slot.Select(false);
        }
    }

    public void RefreshUI()
    {
        int count = 0;
        foreach (var obj in directory.buildObjects)
        {
            bool is_selected = count == cachedSelection;
            Vector3 position = CalculatePosition(count);
            AssignSlotValues(slots[count], obj, is_selected);
            count++;
        }
    }
}
