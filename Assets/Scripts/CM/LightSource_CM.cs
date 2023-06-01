/*
This script is used to set cameras for generating the cubemap shadow map.
*/


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class LightSource_CM : MonoBehaviour
{
    //Used to decide whether to debug from the editor
    public bool control = false;

    [Range(0, 4)] public int CubeMapIndex = 0;

    public float zFar = 10f;
    public float zNear = 0.01f;
    [Range(0f, 30f)] public float lightsize = 10f;
    [Range(-1f, 1f)] public float Bias = 0.005f;
    [Range(0, 0.5f)] public float CutOff = 0.1f;
    public int ShadowMapSize = 1024;

    public Color LightColor = Color.white;
    public float lightstrength;

    private bool isOrthographic = false;
    private int FieldOfView = 90;
    private GameObject[] Cameras = new GameObject[5];
    private Camera[] cameras = new Camera[5];
    public Shader[] Shaders = new Shader[5];
    private RenderTextureDescriptor rtd;
    private RenderTexture[] tempRts = new RenderTexture[5];
    private RenderTexture tempRt;

    //Define the background colors of shadow maps
    private Color[] backgroundColors =
    {
        new (1f,1f,1f,1f),
        new (0f,0f,0f,0f),
        new (0.5f,0.5f,0.5f,0.5f),
        new (0f,0f,0f,0f),
        new (0.5f,0.5f,0.5f,0.5f)
    };
    
    //Create cameras and set their properties(nearPlane, farPlane, FOV, etc.)
    private void CreateCameras()
    {
        for (int i = 0; i < 5; i++)
        {
            Cameras[i] = new GameObject("Camera" + i);
            Cameras[i].transform.SetParent(transform);
            Cameras[i].AddComponent<Camera>();
            Camera camera = Cameras[i].GetComponent<Camera>();
            camera.orthographic = isOrthographic;
            camera.nearClipPlane = zNear;
            camera.farClipPlane = zFar;
            camera.fieldOfView = FieldOfView;
            camera.transform.localPosition = Vector3.zero;
            camera.aspect = 1.0f;
        }
    }
    //Create temporary cameras
    private bool needCreate()
    {
        bool isMissing = false;
        for (int i = 0; i < 5; i++)
        {
            if (transform.Find("Camera" + i) != null)
            {
                Cameras[i] = transform.Find("Camera" + i).gameObject;
            }
            else
            {
                isMissing = true;
                break;
            }
        }

        if (isMissing)
        {
            while (true)
            {
                if (transform.childCount != 0)
                    DestroyImmediate(transform.GetChild(0).gameObject);
                else
                    break;
            }
        }
        return isMissing;
    }

    //Release the temporary RenderTexture memory
    private void OnDestroy()
    {
        for (int i = 0; i < 5; i++) RenderTexture.ReleaseTemporary(tempRts[i]);
    }

    void Start()
    {
        //Create temporary cameras if there are no cameras available
        if (needCreate()) CreateCameras();
        for (int i = 0; i < 5; i++)
        {
            cameras[i] = Cameras[i].GetComponent<Camera>();
        }
    }

    void Update()
    {
        rtd = new RenderTextureDescriptor(ShadowMapSize, ShadowMapSize,
            RenderTextureFormat.ARGB32);
        rtd.useMipMap = true;
        rtd.dimension = TextureDimension.Cube;

        for (int i = 0; i < 5; i++)
        {
            //Release the current temporary RenderTexture and obtain a new one
            RenderTexture.ReleaseTemporary(tempRts[i]);
            tempRts[i] = RenderTexture.GetTemporary(rtd);
            //Set cameras’ properties in the Update function
            tempRts[i].wrapMode = TextureWrapMode.Clamp;
            tempRts[i].filterMode = FilterMode.Trilinear;
            cameras[i].nearClipPlane = zNear;
            cameras[i].farClipPlane = zFar;
            cameras[i].backgroundColor = backgroundColors[i];
            //Set cameras’ render target and shader
            cameras[i].RenderToCubemap(tempRts[i], 63);
            cameras[i].SetReplacementShader(Shaders[i], null);
            Shader.SetGlobalTexture("_CubeMap" + i, tempRts[i]);
        }
        //Set the shaders' uniform variables
        Shader.SetGlobalFloat("ShadowMapSize", ShadowMapSize);
        Shader.SetGlobalFloat("_gShadowBias", Bias);
        Shader.SetGlobalFloat("_clipValue", CutOff);
        Shader.SetGlobalFloat("farPlane", zFar);
        if (control)
        {
            Shader.SetGlobalFloat("lightsize", lightsize);
            Shader.SetGlobalFloat("_gLightStrength", lightstrength);
        }
        Shader.SetGlobalVector("_l", transform.position);
        Shader.SetGlobalMatrix("_gWorldToLightCamera", cameras[0].worldToCameraMatrix);
        Shader.SetGlobalMatrix("_gWorldToLightCamera_back", cameras[1].worldToCameraMatrix);
        //Get the camera's projection matrixs
        Matrix4x4 projectionMatrix = GL.GetGPUProjectionMatrix(cameras[0].projectionMatrix, true);
        Matrix4x4 projectionMatrix_back = GL.GetGPUProjectionMatrix(cameras[1].projectionMatrix, true);
        //Set the shaders' projection matrixs
        Shader.SetGlobalMatrix("_gProjectionMatrix", projectionMatrix * cameras[0].worldToCameraMatrix);
        Shader.SetGlobalMatrix("_gProjectionMatrix_back", projectionMatrix_back * cameras[1].worldToCameraMatrix);
    }
}
