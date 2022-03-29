using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] protected DataDirectory directory;
    [SerializeField] protected Player player;
    [SerializeField] protected Transform uiAnchor;
    [SerializeField] protected Dictionary<string,Transform> uiAnchorTabs = new Dictionary<string, Transform>();
    [SerializeField] protected bool isMultipleTabs = false;
    [SerializeField] protected GameObject uiSlot;

    [Header("Modifiers")]
    [SerializeField] protected Player.INTERACTION_STATE visibleState;

    protected Player.INTERACTION_STATE currentMode;
    protected int selectedTab = 0;
    // Start is called before the first frame update
    protected void Start()
    {
        StartCoroutine(SetupUI());
        currentMode = player.mode;
        UpdateMode(currentMode);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (currentMode != player.mode)
        {
            UpdateMode(player.mode);
        }

        if (selectedTab != player.shownTab)
        {
            selectedTab = player.shownTab;
            directory.SetCategory((BLUEPRINT_CATEGORIES)selectedTab);
            RefreshUI();
        }

        if (isMultipleTabs)
        {
            int showTabIndex = -1;
            if (currentMode == visibleState)
            {
                showTabIndex = selectedTab;
            }

            UpdateTab(showTabIndex);
        }
    }

    protected void UpdateTab(int index)
    {
        int count = 0;
        foreach (var tab in uiAnchorTabs)
        {
            tab.Value.gameObject.SetActive(index == count);
            count++;
        }
    }

    protected void UpdateMode(Player.INTERACTION_STATE new_mode)
    {
        currentMode = new_mode;

        if (currentMode == visibleState)
        {
            uiAnchor.gameObject.SetActive(true);
        }
        else
        {
            uiAnchor.gameObject.SetActive(false);
        }
    }

    protected virtual IEnumerator SetupUI()
    {
        yield return null;
    }

    protected virtual void RefreshUI()
    {

    }
}
