using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Transform body;

    private float yRotation;

    private void Update()
    {
        Rotate();
    }
    public void Rotate()
    {
        float x_rotation = Input.GetAxisRaw("Mouse X");
        yRotation -= Input.GetAxisRaw("Mouse Y");

        yRotation = Mathf.Clamp(yRotation, -90.0f, 90.0f);

        // Pitch is done on head
        transform.localRotation = Quaternion.Euler(yRotation, 0.0f, 0.0f);

        // Yaw is done on body
        body.Rotate(Vector3.up * x_rotation);
    }
}
