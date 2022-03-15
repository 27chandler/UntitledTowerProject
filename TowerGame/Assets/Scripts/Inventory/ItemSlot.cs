using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private RawImage selection;
    [SerializeField] private Text label;
    [SerializeField] private Text amount;

    private int index = -1;
    private bool isSelected = false;

    public RawImage Image { get => image; set => image = value; }
    public Text Label { get => label; set => label = value; }
    public Text Amount { get => amount; set => amount = value; }
    public int Index { get => index; set => index = value; }

    public void Select(bool is_selected)
    {
        isSelected = is_selected;
        if (isSelected)
        {
            selection.color = Color.white;
        }
        else
        {
            selection.color = Color.clear;
        }
    }
}
