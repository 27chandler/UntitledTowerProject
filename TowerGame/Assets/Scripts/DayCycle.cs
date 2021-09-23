using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private float cycleTime = 1.0f;
    private List<LightFlicker> stars = new List<LightFlicker>();

    [SerializeField] private float nightThreshold = 0.2f;
    [SerializeField] private bool isNight;

    private void Start()
    {
        LightFlicker[] found_stars = FindObjectsOfType<LightFlicker>();

        foreach (LightFlicker star in found_stars)
        {
            stars.Add(star);
        }
    }
    // Update is called once per frame
    void Update()
    {
        float cycle_speed = 360.0f / cycleTime;
        transform.Rotate(cycle_speed * Time.deltaTime,0.0f,0.0f);

        if (Vector3.Dot(transform.forward,Vector3.up) > nightThreshold)
        {
            isNight = true;
        }
        else
        {
            isNight = false;
        }
    }
}
