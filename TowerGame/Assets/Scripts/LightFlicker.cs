using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public enum FLICKERSTATE
    {
        LOW,
        HIGH
    }

    private Light light;
    [SerializeField] private List<Color> starTypes = new List<Color>();
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minIntensity;
    [SerializeField] private float flickerTime = 15.0f;
    [SerializeField] private float flickerTransition = 2.0f;

    private FLICKERSTATE flickerState = FLICKERSTATE.LOW;
    private float transitionTimer = 0.0f;
    private float flickerTimer = 0.0f;

    /// <summary>
    /// The randomized multiplier to make some stars flicker slower
    /// / faster than others
    /// </summary>
    private float speedMultiplier = 1.0f;
    /// <summary>
    /// The randomized multiplier to make some stars flicker brighter
    /// than others
    /// </summary>
    private float shineMultiplier = 1.0f;

    private void Start()
    {
        light = GetComponent<Light>();
        if (flickerState == FLICKERSTATE.LOW)
        {
            light.range = minIntensity;
        }
        else
        {
            light.range = maxIntensity * shineMultiplier;
        }

        /// Randomization section
        speedMultiplier = Random.Range(0.1f, 20.0f);
        shineMultiplier = Random.Range(1.0f, 2.0f);

        if (starTypes.Count > 0)
        {
            int random_index = Random.Range(0, starTypes.Count);
            light.color = starTypes[random_index];
        }
        else
        {
            Debug.LogError("No star colours added");
        }
    }
    void Update()
    {
        if (transitionTimer > flickerTime)
        {
            if (flickerState == FLICKERSTATE.LOW)
            {
                IncreaseBrightness();
            }
            else
            {
                DecreaseBrightness();
            }
        }
        else
        {
            transitionTimer += Time.deltaTime * speedMultiplier;
        }
    }

    private void IncreaseBrightness()
    {
        if (flickerTimer < flickerTransition)
        {
            flickerTimer += Time.deltaTime * speedMultiplier;
            light.range = Mathf.Lerp(minIntensity, maxIntensity * shineMultiplier, flickerTimer / flickerTransition);
        }
        else
        {
            SwapState(FLICKERSTATE.HIGH, flickerTime);
        }
    }

    private void DecreaseBrightness()
    {
        if (flickerTimer > 0.0f)
        {
            flickerTimer -= Time.deltaTime * speedMultiplier;
            light.range = Mathf.Lerp(minIntensity, maxIntensity * shineMultiplier, flickerTimer / flickerTransition);
        }
        else
        {
            SwapState(FLICKERSTATE.LOW, 0.0f);
        }
    }

    private void SwapState(FLICKERSTATE state, float timer_start)
    {
        flickerState = state;
        transitionTimer = 0.0f;
        flickerTimer = timer_start;
    }
}
