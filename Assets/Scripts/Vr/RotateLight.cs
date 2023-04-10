using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateLight : MonoBehaviour
{
    public InputActionReference toggleReference = null;

    public Material origin, change;
    public bool control;


    private Vector3 fixedPoint;

    public DragLight dragLight;

    private void Awake()
    {
        toggleReference.action.started += Toggle;
        Shader.SetGlobalFloat("_gLightStrength", 25f);
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    void Start()
    {
        //fixedPoint = transform.position + new Vector3(2, 0, 0);
    }


    private void Toggle(InputAction.CallbackContext context)
    {
        //control = !control;
        //if (control)
        //{
        //    this.GetComponent<MeshRenderer>().material = change;

        //    /*RaycastHit hit;
        //    if (Physics.Raycast(vrTransform.position, vrTransform.forward, out hit))

        //    {
        //        if (hit.collider.gameObject.CompareTag("Top"))
        //        {
        //            transform.position = hit.point;

        //        }
        //    }*/
        //    fixedPoint = transform.position + new Vector3(2, 0, 0);
        //}

        //else
        //{
        //    this.GetComponent<MeshRenderer>().material = origin;

        //}
        dragLight.temp = dragLight.temp + 5f;
        Shader.SetGlobalFloat("_gLightStrength", 25f);

    }

    void Update()
    {
        //if(control)
        //{
            
        //    this.transform.RotateAround(fixedPoint, Vector3.up, 20 * Time.deltaTime);

        //}

    }

}