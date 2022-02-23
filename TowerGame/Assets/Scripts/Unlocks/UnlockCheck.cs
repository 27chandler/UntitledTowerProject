using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCheck : MonoBehaviour
{
    [SerializeField] private float checkTime = 5.0f;
    private float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > checkTime)
        {
            UnlockSystem.CheckUnlocks();
        }
    }
}
