using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class START_UI : MonoBehaviour
{
    private Animator A_Animator_0, A_Animator_1, A_Animator_2;
    private GameObject Menu_0, Menu_1,Menu_2;
    private int Choose;

    // Start is called before the first frame update
    void Start()
    {
        A_Animator_0 = GameObject.Find("Start_Button").GetComponent<Animator>();
        A_Animator_1 = GameObject.Find("Chapter_Button").GetComponent<Animator>();
        A_Animator_2 = GameObject.Find("Return_Button").GetComponent<Animator>();
        Menu_0 = GameObject.Find("Menu_0");
        Menu_1 = GameObject.Find("Menu_1");
        Menu_2 = GameObject.Find("Menu_2");
        Menu_1.SetActive(false);
        Menu_0.SetActive(false);
        Menu_2.SetActive(false);
        Menu_0.SetActive(true);
        Choose = 1;//默认第一个关卡
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Botton(int num)
    {
        switch (num)
        {
            case 1://chapter
                A_Animator_1.SetBool("Click",true);
                Invoke("CChapter",0.9f);
                break;
            case 2://start
                A_Animator_0.SetBool("Click", true);
                Invoke("Level_Change", 0.9f);
                break;
            case 3://exit
                A_Animator_2.SetBool("Click", true);
                Invoke("Return",0.9f);
                break;
            case 4:
                SceneManager.LoadScene("game/c1");
                break;
            case 5:
                Menu_0.SetActive(true);
                Menu_1.SetActive(false);
                Menu_2.SetActive(false);
                break;
            case 6:
                Level_Change();
                Choose = 1;
                break;
            case 7:
                Level_Change();
                Choose = 2;
                break;
            case 8:
                Level_Change();
                Choose = 3;
                break;
            case 9://简单关卡
                GlobalVariable.SetDiffculty(1);
                switch (Choose)
                {
                    case 1:
                        SceneManager.LoadScene("game/c1");//关卡1
                        break;
                    case 2:
                        SceneManager.LoadScene("game/c2");//关卡2
                        break;
                    case 3:
                        SceneManager.LoadScene("game/c3");//关卡3
                        break;
                }
                break;
            case 10://一般
                GlobalVariable.SetDiffculty(2);
                switch (Choose)
                {
                    case 1:
                        SceneManager.LoadScene("game/c1");
                        break;
                    case 2:
                        SceneManager.LoadScene("game/c2");
                        break;
                    case 3:
                        SceneManager.LoadScene("game/c3");
                        break;
                }
                break;
            case 11://困难
                GlobalVariable.SetDiffculty(3);
                switch (Choose)
                {
                    case 1:
                        SceneManager.LoadScene("game/c1");
                        break;
                    case 2:
                        SceneManager.LoadScene("game/c2");
                        break;
                    case 3:
                        SceneManager.LoadScene("game/c3");
                        break;
                }
                break;
        }
    }
    public void Return()
    {
        Application.Quit();
    }
    public void Level_Change()
    {
        Menu_0.SetActive(false);
        Menu_1.SetActive(false);
        Menu_2.SetActive(true);
    }
    public void CChapter()
    {
        Menu_0.SetActive(false);
        Menu_1.SetActive(true);
        Menu_2.SetActive(false);
    }
}
