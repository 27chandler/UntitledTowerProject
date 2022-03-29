using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObjectUI : UIView
{
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
    protected new void Start()
    {
        base.Start();
        anchorStartPos = uiAnchor.localPosition;
        cachedSelection = directory.SelectionIndex;
    }

    protected new void Update()
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

        if (isMultipleTabs)
        {
            SetupTabs(count);
        }
        else
        {
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
        }


        yield return null;
    }

    private void SetupTabs(int index)
    {
        for (int i = 0; i < System.Enum.GetNames(typeof(BLUEPRINT_CATEGORIES)).Length; i++)
        {
            GameObject new_anchor = Instantiate(uiAnchor.gameObject, uiAnchor.position, uiAnchor.localRotation, uiAnchor.parent);
            uiAnchorTabs.Add(System.Enum.GetNames(typeof(BLUEPRINT_CATEGORIES))[i],new_anchor.transform);
        }

        foreach (var obj in directory.buildObjects)
        {
            string category = obj.category.ToString();

            GameObject new_ui_slot = Instantiate(uiSlot);
            new_ui_slot.transform.parent = uiAnchorTabs[category];

            ItemSlot slot = new_ui_slot.GetComponent<ItemSlot>();
            AssignSlotValues(slot, obj, index == cachedSelection);
            slot.Index = index;

            slots.Add(slot);

            index++;
        }
    }

    private Vector3 CalculatePosition(int index)
    {
        return Vector3.zero + (Vector3.down * seperation * index);
    }

    private void AssignSlotValues(ItemSlot slot, BuildData data, bool is_selected = false)
    {
        slot.Label.text = data.identifier;

        if (is_selected)
        {
            slot.Select(true);
        }
        else
        {
            slot.Select(false);
        }
    }

    protected override void RefreshUI()
    {
        base.RefreshUI();
        int count = 0;
        foreach (var obj in directory.buildObjects)
        {
            bool is_selected = obj == directory.GetSelectedObject();
            Vector3 position = CalculatePosition(count);
            AssignSlotValues(slots[count], obj, is_selected);
            count++;
        }
    }
}
