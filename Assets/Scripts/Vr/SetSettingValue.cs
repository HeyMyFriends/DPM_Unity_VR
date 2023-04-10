using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetSettingValue : MonoBehaviour
{
    public TextMeshProUGUI LightSizeText, BiasText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLightSizeText(float _volume)
    {
        if (LightSizeText != null)
            LightSizeText.text = _volume.ToString("#0.000");
    }

    public void SetBiasText(float _volume)
    {
        if (BiasText != null)
            BiasText.text = _volume.ToString();
    }
}
