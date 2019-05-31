using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image_Click : MonoBehaviour
{
    public UI_Control m_UI_Control;
    // Start is called before the first frame update
    void Start()
    {
        m_UI_Control = GameObject.Find("UI").GetComponent<UI_Control>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        m_UI_Control.StateChange(UI_Control.GameState.START);
    }
}
