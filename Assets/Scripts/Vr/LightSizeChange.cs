/*
This script is used to implement VR interaction(light size control using the controller buttons).
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightSizeChange : MonoBehaviour
{
    public InputActionReference lightSizeReference = null;
    public InputActionReference lightSizeReference2 = null;

    //The temporary variable for the LightSize
    private float temp = 1.00f;

    //Display UI in VR that shows the FPS and parameter values
    public DisplayFPS displayFPS;
    public Transform light;

    private void Update()
    {
        float value = lightSizeReference.action.ReadValue<float>();
        float value2 = lightSizeReference2.action.ReadValue<float>();
        UpdateLightSize(value);
        UpdateLightSize2(value2);
    }

    //Decrease the value of the LightSize variable
    private void UpdateLightSize(float value)
    {
        temp -= 0.01f * value;
        if (temp <= 0.00f)
            temp = 0.00f;

        displayFPS.lightSize = temp;
        Shader.SetGlobalFloat("lightsize", temp);
        light.localScale = new Vector3(1.0f + temp, 1.0f + temp, 1.0f + temp);
    }

    //Increase the value of the LightSize variable
    private void UpdateLightSize2(float value)
    {
        
        temp += 0.01f * value;
        if (temp >= 1.5f)
            temp = 1.5f;

        displayFPS.lightSize = temp;
        Shader.SetGlobalFloat("lightsize", temp);
        light.localScale = new Vector3(1.0f + temp, 1.0f + temp, 1.0f + temp);

    }
}
