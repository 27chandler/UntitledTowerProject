using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralStars : MonoBehaviour
{
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private float starDistance = 200.0f;
    [SerializeField] private int starAmount = 100;
    [SerializeField] private float upThreshold = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < starAmount; i++)
        {
            Vector3 direction = new Vector3();
            direction.x = Random.Range(-1.0f, 1.0f);
            direction.y = Random.Range(-1.0f, 1.0f);
            direction.z = Random.Range(-1.0f, 1.0f);
            direction.Normalize();
            if (Vector3.Dot(Vector3.up,direction) < upThreshold)
            {
                direction = -direction;
            }

            GameObject new_star = Instantiate(starPrefab, transform.position + (direction * starDistance), new Quaternion());
            new_star.transform.SetParent(transform);
        }
    }
}
