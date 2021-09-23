using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light light;
    private float maxIntensity;
    private float minIntensity;

    private void Start()
    {
        light = GetComponent<Light>();
    }
    void Update()
    {
        //light.intensity = 
    }
}
