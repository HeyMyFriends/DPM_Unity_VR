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
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();//初始化navMeshAgent
        control = true;
    }
    void Update()
    {
        drag.control = control;
        if (control)
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == false)//EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)==false
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//住摄像机向鼠标位置发射射线 

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))//射线检验 
                {
                    //Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "Plane")
                    {
                        navMeshAgent.SetDestination(hit.point);//mHit.point就是射线和plane的相交点，实为碰撞点
                        temp = Instantiate(robot, hit.point, Quaternion.identity);
                        Destroy(temp, 5);
                    }
                }
            }
            float speed = navMeshAgent.velocity.sqrMagnitude;
            anim.SetFloat("Speed", speed);
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