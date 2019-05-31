using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Blood_Window_UI : MonoBehaviour
{
    private Image Blood;
    private float TransNum = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        Blood = this.GetComponent<Image>();
        Blood.color = new Color(135/255f, 49/255f, 49/255f, TransNum);
    }

    public void BloodState(int Blood_State)
    {
        if (Blood_State == 1)
        {
            if (TransNum == 1) {; }
            else
            {
                InvokeRepeating("AddBlood", 0.01f, 0.01f);
            }
            
            
        }
        if (Blood_State == 2)
        {
            if (TransNum == 0) {; }
            else
            {
                InvokeRepeating("MinusBlood", 0.01f, 0.01f);
            }
            
        }
    }
    public void MinusBlood()
    {
        TransNum -= 0.01f;
        Blood.color = new Color(135 / 255f, 49 / 255f, 49 / 255f, TransNum);
        if (TransNum <= 0)
        {
            CancelInvoke();
        }
    }

    public void AddBlood()
    {
        TransNum += 0.01f;
        Blood.color = new Color(135 / 255f, 49 / 255f, 49 / 255f, TransNum);
        if (TransNum >= 1)
        {
            CancelInvoke();
        }
    }
}
