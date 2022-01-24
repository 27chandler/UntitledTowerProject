using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private Text label;
    [SerializeField] private Text amount;

    public RawImage Image { get => image; set => image = value; }
    public Text Label { get => label; set => label = value; }
    public Text Amount { get => amount; set => amount = value; }
}
