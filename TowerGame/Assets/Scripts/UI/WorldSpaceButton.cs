using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class WorldSpaceButton : MonoBehaviour, IClickable
{
    private Color defaultColour;
    [SerializeField] private Color pressedColour;
    [SerializeField] private Color hoverColour;
    [SerializeField] private float highlightResetTime = 0.2f;

    private RawImage image;
    private float highlightTimer = 0.0f;

    private void Start()
    {
        image = GetComponent<RawImage>();

        if (image != null)
            defaultColour = image.color;
    }

    private void Update()
    {
        if (highlightTimer > 0.0f)
        {
            highlightTimer -= Time.deltaTime;
        }
        else
        {
            image.color = defaultColour;
        }
    }

    public void LeftClicked()
    {
        image.color = pressedColour;
    }

    public void Hover()
    {
        highlightTimer = highlightResetTime;
        image.color = hoverColour;
    }
}
