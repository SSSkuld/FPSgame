using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP_UI : MonoBehaviour
{
    private Image Blood;
    private float TransNum = 1;
    public int m_HP = 100;

    private GameObject Blood1, Blood2, Blood3, Blood4, Blood5;
    // Start is called before the first frame update
    void Start()
    {
        Blood1 = GameObject.Find("Blood_Layer_1");
        Blood2 = GameObject.Find("Blood_Layer_2");
        Blood3 = GameObject.Find("Blood_Layer_3");
        Blood4 = GameObject.Find("Blood_Layer_4");
        Blood5 = GameObject.Find("Blood_Layer_5");


    }



    //HP分类管理
    public void ShowBlood(int HP)
    {

        if (HP >= 100)
        {
            CreatBlood(Blood1, 2);
            CreatBlood(Blood2, 2);
            CreatBlood(Blood3, 2);
            CreatBlood(Blood4, 2);
           // CreatBlood(Blood5, 2);
        }
        else if (HP >= 80 && HP < 100)
        {
            CreatBlood(Blood1, 1);
            CreatBlood(Blood2, 2);
            CreatBlood(Blood3, 2);
            CreatBlood(Blood4, 2);
            //CreatBlood(Blood5, 2);
        }
        else if (HP >= 50 && HP < 80)
        {
            CreatBlood(Blood1, 1);
            CreatBlood(Blood2, 1);
            CreatBlood(Blood3, 2);
            CreatBlood(Blood4, 2);
            //CreatBlood(Blood5, 2);
        }
        else if (HP >= 20 && HP < 50)
        {
            CreatBlood(Blood1, 1);
            CreatBlood(Blood2, 1);
            CreatBlood(Blood3, 1);
            CreatBlood(Blood4, 2);
            //CreatBlood(Blood5, 2);
        }
        else if (HP > 0 && HP < 20)
        {
            CreatBlood(Blood1, 1);
            CreatBlood(Blood2, 1);
            CreatBlood(Blood3, 1);
            CreatBlood(Blood4, 1);
           // CreatBlood(Blood5, 2);
        }
        else
        {
            CreatBlood(Blood1, 1);
            CreatBlood(Blood2, 1);
            CreatBlood(Blood3, 1);
            CreatBlood(Blood4, 1);
            //CreatBlood(Blood5, 1);
            Invoke("Show_invalid", 2f);

        }
    }

    //各个血量出现的血痕控制
    public void CreatBlood(GameObject Blood_n, int num)
    {
        Blood_n.SendMessage("BloodState", num);
    }
    //延时函数
    private void Show_invalid()
    {
        // GameObject.Find("UI").SendMessage("StateChange", UI_Control.GameState.START);
        m_HP = 100;
    }
}
