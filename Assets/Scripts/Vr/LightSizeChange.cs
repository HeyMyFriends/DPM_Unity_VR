using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LightSizeChange : MonoBehaviour
{
    public InputActionReference lightSizeReference = null;
    public InputActionReference lightSizeReference2 = null;
    private float temp = 1.00f;
    public DisplayFPS displayFPS;
    public Transform light;

    private void Update()
    {
        float value = lightSizeReference.action.ReadValue<float>();
        float value2 = lightSizeReference2.action.ReadValue<float>();
        UpdateLightSize(value);
        UpdateLightSize2(value2);
    }

    private void UpdateLightSize(float value)
    {
        
        {
            temp -= 0.01f * value;
            if (temp <= 0.00f)
                temp = 0.00f;
            displayFPS.lightSize = temp;
            Shader.SetGlobalFloat("lightsize", temp);
            light.localScale = new Vector3(1.0f + temp, 1.0f + temp, 1.0f + temp);

        }

    }

    private void UpdateLightSize2(float value)
    {
        
        {
            temp += 0.01f * value;
            if (temp >= 1.5f)
                temp = 1.5f;

            displayFPS.lightSize = temp;
            Shader.SetGlobalFloat("lightsize", temp);
            light.localScale = new Vector3(1.0f + temp, 1.0f + temp, 1.0f + temp);
        }

    }
}
