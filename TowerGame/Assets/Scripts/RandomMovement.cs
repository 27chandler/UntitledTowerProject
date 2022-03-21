using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    [SerializeField] private float lerpTime = 5.0f;
    [SerializeField] private float randomAddedTimeMax = 1.0f;
    [SerializeField] private AnimationCurve lerpCurve = new AnimationCurve();
    [SerializeField] private List<Vector3> positions = new List<Vector3>();
    private Vector3 originPosition;
    private Vector3 oldPosition;
    private Vector3 selectedPosition;
    private float lerpProgress = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        lerpTime = lerpTime + Random.Range(0.0f, randomAddedTimeMax);
        originPosition = transform.position;
        NextPosition();
    }

    // Update is called once per frame
    void Update()
    {
        lerpProgress += Time.deltaTime;
        transform.position = Vector3.Lerp(oldPosition, selectedPosition, lerpCurve.Evaluate(lerpProgress/lerpTime));

        if  (lerpProgress > lerpTime)
        {
            NextPosition();
        }
    }

    private void NextPosition()
    {
        lerpProgress = 0.0f;
        oldPosition = transform.position;
        selectedPosition = originPosition + positions[Random.Range(0, positions.Count - 1)];
    }
}
