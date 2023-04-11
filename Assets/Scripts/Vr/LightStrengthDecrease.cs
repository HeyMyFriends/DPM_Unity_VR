using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightStrengthDecrease : MonoBehaviour
{
    public InputActionReference toggleReference = null;


    private void Awake()
    {
        toggleReference.action.started += Toggle;
        Shader.SetGlobalFloat("_gLightStrength", 25f);
    }

    private void OnDestroy()
    {
        toggleReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        Shader.SetGlobalFloat("_gLightStrength", 25f);
    }


}
