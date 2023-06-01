/*
This script is used to implement VR interaction(set the light strength to a low value using the controller button).
*/

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
