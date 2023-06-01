﻿/*
This script is used to achieve automatic pathfinding movement of the robot and allows the user to set the destination by clicking.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Nvgt : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Animator anim;
    public GameObject robot;
    private GameObject temp;
    public bool control;
    public Drag drag;

    void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        control = true;
    }

    void Update()
    {
        drag.control = control;

        if (control)
        {
            //Detect click events
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false) //EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)==false
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                //Emits a ray to obtain the position of the clicked point.
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Plane")
                    {
                        //Set the destination for the robot
                        navMeshAgent.SetDestination(hit.point);
                        temp = Instantiate(robot, hit.point, Quaternion.identity);
                        Destroy(temp, 5);
                    }
                }
            }

            //Set the speed of the robot's walking animation
            float speed = navMeshAgent.velocity.sqrMagnitude;
            anim.SetFloat("Speed", speed);

            //Check if the destination has been reached
            if (navMeshAgent.remainingDistance < 0.1)
            {
                Destroy(temp);
            }
        }
    }

    public void ChangeControl()
    {
        control = !control;
    }
}
