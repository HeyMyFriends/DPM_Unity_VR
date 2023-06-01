/*
This script is used to achieve simple rotation around a point.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform targetTrans;
    public float speed, turnRate;
    public Animator anim;
    public Quaternion angle;

    void Start()
    {
        angle = transform.rotation;
    }

    void Update()
    {
        // Rotate the object around the target
        // transform.RotateAround(targetTrans.position, Vector3.up, turnRate * Time.deltaTime);

        // Reset the rotation to the original angle
        // transform.rotation = angle;

        // Move the object forward
        transform.position += transform.forward * Time.deltaTime * speed;

        // Set the animation speed
        anim.SetFloat("Speed", 1.0f);

        // Rotate the object around its own axis
        transform.Rotate(0f, turnRate * Time.deltaTime, 0f);
    }
}
