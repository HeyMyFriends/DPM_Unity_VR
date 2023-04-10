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
        //transform.RotateAround(targetTrans.position, Vector3.up, turnRate * Time.deltaTime);
        //transform.rotation = angle;
        transform.position += transform.forward * Time.deltaTime * speed; 
        anim.SetFloat("Speed", 1.0f);
        transform.Rotate(0f, turnRate * Time.deltaTime, 0f);

        
    }
}
