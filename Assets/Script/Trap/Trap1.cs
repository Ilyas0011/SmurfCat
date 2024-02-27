using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap1 : MonoBehaviour
{
    private float rotationSpeed = 50.0f;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);  
    }
}
