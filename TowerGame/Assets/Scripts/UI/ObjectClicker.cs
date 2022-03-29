using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float clickDistance = 5.0f;
    public void Click()
    {
        RaycastHit hit;
        Debug.Log("Ray fired");

        Physics.Raycast(transform.position, transform.forward, out hit, clickDistance, mask);

        if (hit.collider != null)
        {
            IClickable click_behaviour = hit.collider.GetComponent<IClickable>();

            if (click_behaviour != null)
            {
                TriggerClickBehaviours(click_behaviour);
            }
        }
    }

    public void HoldClick()
    {
        RaycastHit hit;

        Physics.Raycast(transform.position, transform.forward, out hit, clickDistance, mask);

        if (hit.collider != null)
        {
            IClickable hold_click_behaviour = hit.collider.GetComponent<IClickable>();

            if (hold_click_behaviour != null)
            {
                TriggerHoldClickBehaviours(hold_click_behaviour);
            }
        }
    }

    private void TriggerClickBehaviours(IClickable clickable)
    {
        clickable.LeftClicked();
    }

    private void TriggerHoldClickBehaviours(IClickable clickable)
    {
        clickable.LeftClickHold();
    }
}
