using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBoxController : MonoBehaviour
{
    RectTransform imageRec;//2dUI
    GameObject Bulle_Box;
    static PlayerInterface playerInterface;
    private float Fill_Num=0;
    private Image Fill_Image;
    bool Fill_Flag=false;

    Canvas canvas;
   
    private void Awake()
    {
        //获取TEXT
        Fill_Image = GameObject.Find("Box_Progress_bar_01").GetComponent<Image>();
        RectTransform[] imageRecArr = gameObject.GetComponentsInChildren< RectTransform>();
        Fill_Image.fillAmount = 0 ;
        for (int i=0;i<imageRecArr.Length;i++)
        {
            if(imageRecArr[i].name == "Bullet_Box_0")
            {
                imageRec = imageRecArr[i];
                Bulle_Box = imageRec.gameObject;
                break;
            }
        }
        Bulle_Box.SetActive(false);
        canvas = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        if (playerInterface == null)
        {
            playerInterface = GameObject.Find("Player").GetComponent<PlayerInterface>();
        }

        if (Bulle_Box.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.F)) Fill_Flag = false;
            if (Input.GetKey(KeyCode.F))
            {
                if (!Fill_Flag)
                {
                    Fill_Image.fillAmount += 0.02f;
                    if (Fill_Image.fillAmount == 1)
                    {
                        Fill_Image.fillAmount = 0;
                        playerInterface.SupplementAmmunition();
                        Fill_Flag = true;
                    }
                }
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                Fill_Image.fillAmount = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.name == "Player")
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint (transform.position + transform.up);

            imageRec.position = screenPos;

            if (screenPos.z < 0)
                Bulle_Box.SetActive(false);
            else
                Bulle_Box.SetActive(true);

        }      

        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            Bulle_Box.SetActive(false);
        }
    }
}
