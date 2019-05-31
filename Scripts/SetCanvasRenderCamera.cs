using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanvasRenderCamera : MonoBehaviour
{
    // Start is called before the first frame update
    Camera UICamera;
    private void Awake()
    {
        UICamera= GameObject.Find("UICamera").GetComponent<Camera>();

        Canvas[] canvases = GetComponentsInChildren<Canvas>();

        foreach(Canvas canvas in canvases)
        {
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = UICamera;
        }
    }

    
}
