using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light light;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minIntensity;

    private void Start()
    {
        light = GetComponent<Light>();
    }
    void Update()
    {
        light.range = minIntensity + (Random.value * (maxIntensity - minIntensity));
    }
}
