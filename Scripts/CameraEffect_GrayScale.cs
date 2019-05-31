using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class CameraEffect_GrayScale : MonoBehaviour
{

    #region Variables
    public Shader curShader;
    public float grayScaleAmout;
    public float grayScaleSpeed = 1f;

    private Material curMaterial;

    Material material
    {
        get
        {
            if (curMaterial == null)
            {
                curMaterial = new Material(curShader);
                curMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return curMaterial;
        }
    }
    #endregion
    // Use this for initialization
    void Start()
    {
        if (!SystemInfo.supportsImageEffects)
        {
            enabled = false;
        }

        curShader = Shader.Find("Custom/CameraEffect_GrayScale");

        if (!curShader && !curShader.isSupported)
        {
            enabled = false;
        }

        grayScaleAmout = 0;
    }

    // Update is called once per frame
    void Update()
    {
        grayScaleAmout += Time.deltaTime * grayScaleSpeed;
        grayScaleAmout = Mathf.Max(0f, grayScaleAmout);
        grayScaleAmout = Mathf.Min(1.0f, grayScaleAmout);
    }

    void OnRenderImage(RenderTexture source, RenderTexture target)
    {
        if (curShader != null)
        {
            material.SetFloat("_LuminosityAmount", grayScaleAmout);
            Graphics.Blit(source, target, material);
            //Debug.Log("OnRenderImage: " + grayScaleAmout);
        }
        else
        {
            Graphics.Blit(source, target);
        }
    }

    void OnDisable()
    {
        if (curMaterial)
        {
            DestroyImmediate(curMaterial);
        }
    }
}
