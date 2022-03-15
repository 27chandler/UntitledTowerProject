using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected Transform uiAnchor;
    [SerializeField] protected GameObject uiSlot;

    [Header("Modifiers")]
    [SerializeField] protected Player.INTERACTION_STATE visibleState;

    protected Player.INTERACTION_STATE currentMode;
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
}
