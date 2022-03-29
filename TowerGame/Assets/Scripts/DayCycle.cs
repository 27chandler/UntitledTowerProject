using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private Transform sun;
    [SerializeField] private string identifier;
    [SerializeField] private float cycleTime = 600.0f;
    [SerializeField] private float speedMultiplier = 1.0f;
    [SerializeField][ReadOnly] private float timer = 0.0f;
    [SerializeField][ReadOnly] private float nextSegmentTime = 0.0f;
    [SerializeField][ReadOnly] private float segmentProgress = 0.0f;
    [SerializeField] private List<DaySegment> worldCycle = new List<DaySegment>();
    [SerializeField] private UnityEvent onNextSegment;

    private int cycleSegmentIndex = 0;
    private int nextCycleSegmentIndex = 1;
    private Vector3 toRotation;

    public static Dictionary<string,DayCycle> cycles = new Dictionary<string,DayCycle>();

    private void Start()
    {
        cycles.Add(identifier, this);

        sun.eulerAngles = worldCycle[cycleSegmentIndex].startAngle;
        CacheNextValues();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * speedMultiplier;

        SunRotation();

        if (timer > cycleTime)
        {
            timer = 0.0f;
        }

        if (timer >= nextSegmentTime)
        {
            StartNextSegment();
        }

        float left_side = Mathf.Abs(timer - worldCycle[cycleSegmentIndex].startTime);
        float right_side = Mathf.Abs(nextSegmentTime - worldCycle[cycleSegmentIndex].startTime);

        // For handling the rollover back to the starting segment
        if (nextSegmentTime - worldCycle[cycleSegmentIndex].startTime < 0.0f)
        {
            right_side = cycleTime - worldCycle[cycleSegmentIndex].startTime;
            left_side = timer + right_side;
        }
        segmentProgress = left_side / right_side;
    }

    public static string CurrentTime(string ident)
    {
        return cycles[ident].worldCycle[cycles[ident].cycleSegmentIndex].segmentName;
    }

    public void SkipTo(string segment_name)
    {
        int start_index = cycleSegmentIndex;

        StartNextSegment();
        while ((start_index != cycleSegmentIndex) && (worldCycle[cycleSegmentIndex].segmentName != segment_name))
        {
            StartNextSegment();
        }
    }

    public bool IsBefore(string segment_name)
    {
        int index = worldCycle.FindIndex(0, x => x.segmentName == segment_name);

        if (index == -1)
        {
            return false;
        }

        if (index > cycleSegmentIndex)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsAfter(string segment_name)
    {
        int index = worldCycle.FindIndex(0, x => x.segmentName == segment_name);

        if (index == -1)
        {
            return false;
        }

        if (index <= cycleSegmentIndex)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartNextSegment()
    {
        cycleSegmentIndex = nextCycleSegmentIndex;
        nextCycleSegmentIndex++;
        worldCycle[cycleSegmentIndex].onEnter.Invoke();
        timer = worldCycle[cycleSegmentIndex].startTime;

        if (nextCycleSegmentIndex >= worldCycle.Count)
        {
            nextCycleSegmentIndex = 0;
            timer -= cycleTime;
        }

        sun.eulerAngles = worldCycle[cycleSegmentIndex].startAngle;
        CacheNextValues();
        onNextSegment.Invoke();
    }

    private void CacheNextValues()
    {
        nextSegmentTime = worldCycle[nextCycleSegmentIndex].startTime;
        toRotation = worldCycle[nextCycleSegmentIndex].startAngle;
    }

    private void SunRotation()
    {
        sun.rotation = Quaternion.Lerp(Quaternion.Euler(worldCycle[cycleSegmentIndex].startAngle), Quaternion.Euler(toRotation), segmentProgress);
    }

    /// <summary>
    /// Subscribe to the event triggered on entering a
    /// day cycle segment
    /// </summary>
    /// <param name="index"></param> The index of the day segment to subscribe to
    public static UnityEvent Subscribe(string ident, int index)
    {
        return cycles[ident].worldCycle[index].onEnter;
    }

    public static UnityEvent Subscribe(string ident, string segment_name)
    {
        if (segment_name == "TimeAdvance")
        {
            return cycles[ident].onNextSegment;
        }

        int index = cycles[ident].worldCycle.FindIndex(x => x.segmentName == segment_name);
        return cycles[ident].worldCycle[index].onEnter;
    }
}

[Serializable]
public class DaySegment
{
    [SerializeField] public string segmentName;
    [SerializeField] public float startTime;
    [SerializeField] public Vector3 startAngle;
    [SerializeField] public Texture icon;
    [SerializeField] public UnityEvent onEnter;
}
