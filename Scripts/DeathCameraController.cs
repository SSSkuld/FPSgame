﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCameraController : MonoBehaviour
{
    private float deathTimer=4.5f;
    public bool death;

    public void Work()
    {
        GameObject player = GameObject.Find("Player");
        gameObject.GetComponent<Camera>().enabled = true;

        Vector3 pos = GameObject.Find("MainCamera").transform.position;
        Quaternion rot = GameObject.Find("MainCamera").transform.rotation;
        Vector3 dir = player.transform.right;

        gameObject.tag = "MainCamera";

        //激活碰撞体和刚体
        gameObject.transform.position = pos;
        gameObject.transform.rotation = rot;
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;

        gameObject.GetComponent<Rigidbody>().velocity = dir * 4;

        //黑白摄像机工作
        gameObject.GetComponent<CameraEffect_GrayScale>().enabled = true;

        death = true;
    }

    #region Variables
    public Shader SCShader;
    private float TimeX = 1.0f;
    private Vector4 ScreenResolution;
    private Material SCMaterial;
    [Range(0, 1000)]
    public float Radius = 150.0f;
    [Range(0, 1000)]
    public float Factor = 200.0f;
    [Range(1, 8)]
    public int FastFilter = 2;

    public static float ChangeRadius;
    public static float ChangeFactor;
    public static int ChangeFastFilter;

    #endregion

    #region Properties
    Material material
    {
        get
        {
            if (SCMaterial == null)
            {
                SCMaterial = new Material(SCShader);
                SCMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return SCMaterial;
        }
    }

    #endregion
    void Start()
    {
        ChangeRadius = Radius;
        ChangeFactor = Factor;
        ChangeFastFilter = FastFilter;
        SCShader = Shader.Find("Custom/CameraEffect_AverageBlur");

        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
            return;
        }
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {

        if (SCShader != null)
        {
            int DownScale = FastFilter;
            TimeX += Time.deltaTime;
            if (TimeX > 100) TimeX = 0;
            material.SetFloat("_TimeX", TimeX);
            material.SetFloat("_Radius", Radius / DownScale);
            material.SetFloat("_Factor", Factor);
            material.SetVector("_ScreenResolution", new Vector2(Screen.width / DownScale, Screen.height / DownScale));
            int rtW = sourceTexture.width / DownScale;
            int rtH = sourceTexture.height / DownScale;

            if (FastFilter > 1)
            {
                RenderTexture buffer = RenderTexture.GetTemporary(rtW, rtH, 0);
                Graphics.Blit(sourceTexture, buffer, material);
                Graphics.Blit(buffer, destTexture);
                RenderTexture.ReleaseTemporary(buffer);
            }
            else
            {
                Graphics.Blit(sourceTexture, destTexture, material);
            }
        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }


    }
    void OnValidate()
    {
        ChangeRadius = Radius;
        ChangeFactor = Factor;
        ChangeFastFilter = FastFilter;
    }
    // Update is called once per frame
    void Update()
    {
        if(death)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer<0)
            {
                SceneManager.LoadScene("game/Start_UI");
            }
        }

        if (Application.isPlaying)
        {
            Radius = ChangeRadius;
            Factor = ChangeFactor;
            FastFilter = ChangeFastFilter;
        }
#if UNITY_EDITOR
        if (Application.isPlaying != true)
        {
            SCShader = Shader.Find("Custom/CameraEffect_AverageBlur");

        }
#endif

    }

    void OnDisable()
    {
        if (SCMaterial)
        {
            DestroyImmediate(SCMaterial);
        }

    }

}
