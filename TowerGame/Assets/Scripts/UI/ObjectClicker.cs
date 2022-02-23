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
        Debug.Log("Rasy fired");

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

    private void TriggerClickBehaviours(IClickable clickable)
    {
        clickable.LeftClicked();
    }
}
