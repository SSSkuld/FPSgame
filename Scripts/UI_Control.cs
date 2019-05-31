using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Control : MonoBehaviour
{
    //游戏状态枚举
    public enum GameState
    {
        START,
        GAME,
        END
    }

    public GameState m_State;
   // private GameObject m_Start_UI;
    private GameObject m_Game_UI;
    private GameObject m_End_UI;

   // private GameObject m_SFX;
  //  private GameObject m_Menu_1;
 //   private GameObject m_Menu_0;

    // Start is called before the first frame update
    void Start()
    {
       // m_Start_UI = GameObject.Find("Start_UI");
        m_Game_UI = GameObject.Find("Game_UI");
        m_End_UI = GameObject.Find("End_UI");
        //m_SFX = GameObject.Find("SFX");
       // m_Menu_1 = GameObject.Find("Menu_1");
        //m_Menu_0 = GameObject.Find("Menu_0");
       // m_Menu_0.SetActive(false);
      //  m_Menu_0.SetActive(true);
        //切换游戏状态
        StateChange(GameState.GAME);
    }

    public void StateChange(GameState State)
    {
        m_State = State;
        if (m_State == GameState.START)//开始界面
        {
            /*GameObject.Find("PLAYER").GetComponent<physicWalk>().enabled = false;
            GameObject.Find("PLAYER").GetComponent<physicWalk_MouseLook>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<physicWalk_MouseLook>().enabled = false;*/
         //   m_Start_UI.SetActive(true);
            m_Game_UI.SetActive(false);
            m_End_UI.SetActive(false);
         //   m_Menu_1.SetActive(false);
            //背景音乐切换
            //m_SFX.SetActive(false);
        }
        else if (m_State == GameState.GAME)//游戏界面
        {
            /*GameObject.Find("PLAYER").GetComponent<physicWalk>().enabled = true;
            GameObject.Find("PLAYER").GetComponent<physicWalk_MouseLook>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<physicWalk_MouseLook>().enabled = true;*/
           // m_Start_UI.SetActive(false);
            m_Game_UI.SetActive(true);
            m_End_UI.SetActive(false);
            //背景音乐切换
            // m_SFX.SetActive(true);
        }
        else if (m_State == GameState.END)//结束界面
        {
           // m_Start_UI.SetActive(false);
            m_Game_UI.SetActive(false);
            m_End_UI.SetActive(true);
            //背景音乐切换
            //m_SFX.SetActive(true);
        }
    }

    public void botton_0_click()
    {
        StateChange(GameState.GAME);
    }
    public void botton_1_click()
    {
       // m_Menu_1.SetActive(true);
       // m_Menu_0.SetActive(false);

    }
    public void botton_2_click()
    {
      //  m_Menu_1.SetActive(false);
      //  m_Menu_0.SetActive(false);
    }
    // Update is called once per frame
    public void botton_back_click(int num)
    {
        //m_Menu_0.SetActive(true);
        if (num == 1)
        {
           // m_Menu_1.SetActive(false);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    void Update()
    {

    }
}
