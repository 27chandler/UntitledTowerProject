using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyAffector : MonoBehaviour
{
    protected Rigidbody rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetRigidbody(Rigidbody rigidbody)
    {
        rb = rigidbody;
    }
}
