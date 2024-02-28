using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBird : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    private Transform myTransform;

    void Start()
    {
        myTransform = transform;
    }

    private void FixedUpdate()
    {

        myTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
    }
}
