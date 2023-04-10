using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// AndroidController
/// </summary>

public class ControllerBehavior : MonoBehaviour
{
    private Touch _OldTouch1;    
    private Touch _OldTouch2;   

    Vector2 _M_Screenpos = new Vector2();
    bool _bMoveOrRotation;
    Vector3 _OldPosition;
    Quaternion _OldRotation;
    float _OldFOV;

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    public Camera cam;
    public bool control;
    void Start()
    {
        
        _OldPosition = Camera.main.transform.position;
        _OldRotation = Camera.main.transform.rotation;
        cam = GetComponent<Camera>();
        _OldFOV = cam.fieldOfView;
        control = false;
    }
    void Update()
    {
        if(control)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) == true)
            {
                return;
            }

              
            if (Input.touchCount <= 0)
            {
                return;
            }

               
            if (1 == Input.touchCount)
            {
                
                if (_bMoveOrRotation)
                {
                    //水平上下旋转
                    Touch _Touch = Input.GetTouch(0);
                    Vector2 _DeltaPos = _Touch.deltaPosition;
                    transform.Rotate(0.1f * Vector3.up * _DeltaPos.x, Space.World);
                    transform.Rotate(0.1f * Vector3.left * _DeltaPos.y, Space.World);
                }
                else
                {
                    

                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                          
                        _M_Screenpos = Input.touches[0].position;

                    }
                      
                    else if (Input.touches[0].phase == TouchPhase.Moved)
                    {

                         
                        Camera.main.transform.Translate(new Vector3(-Input.touches[0].deltaPosition.x * Time.deltaTime * 0.1f, -Input.touches[0].deltaPosition.y * Time.deltaTime * 0.1f, 0));
                    }
                }
            }
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If the camera is orthographic...
                if (cam.orthographic)
                {
                    // ... change the orthographic size based on the change in distance between the touches.
                    cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                    // Make sure the orthographic size never drops below zero.
                    cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
                }
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 10f, 40f);
                }
            }
            
        }

    }


    
    public void BackPosition()
    {
        
        Camera.main.transform.position = _OldPosition;
        
        Camera.main.transform.rotation = _OldRotation;
        cam.fieldOfView = _OldFOV;
    }

    public void RotationOrMove()
    {
        _bMoveOrRotation = !_bMoveOrRotation;
    }

    public void ChangeControl()
    {
        control = !control;
    }
}


