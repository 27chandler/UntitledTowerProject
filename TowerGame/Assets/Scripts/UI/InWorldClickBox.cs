using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldClickBox : MonoBehaviour, IClickable
{
    void IClickable.Hover()
    {
        Debug.Log("hover");
    }

    void IClickable.LeftClicked()
    {
        Debug.Log("click");
    }
}
