using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionLocationFlagController : MonoBehaviour
{
    // Start is called before the first frame update
    Transform playerTransform;


    RectTransform Flag_RectTransform;

    Text Flag_Text;
    Image Flag_Image;

    Text Hint_Text;
    Image   Hint_BackGroundImage;

    bool init;

    //显示任务地点的小旗子是否显示
    public bool isFlagDisplayed;
    //是否显示任务显示
    public bool isHintDisplayed;
    //任务提示
    public string hintContent
    {
        get
        {
            Hint_Text.enabled = true;
            string str = Hint_Text.text;
             Hint_Text.enabled = isHintDisplayed;
            return str;
        }
        set
        {
            Hint_Text.enabled = true;
            Hint_Text.text = value;
            Hint_Text.enabled = isHintDisplayed;           
        }
    }
    //目标地点
    public Vector3 desPos;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();


        RectTransform[] RectArr = gameObject.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < RectArr.Length; i++)
        {
            if (RectArr[i].name == "Flag")
                Flag_RectTransform = RectArr[i].gameObject.GetComponent<RectTransform>();

            if (RectArr[i].name == "Flag_Text")
                Flag_Text = RectArr[i].gameObject.GetComponent<Text>();

            if (RectArr[i].name == "Flag_Image")
                Flag_Image = RectArr[i].gameObject.GetComponent<Image>();

            if (RectArr[i].name == "Hint_Text")
                Hint_Text = RectArr[i].gameObject.GetComponent<Text>();

            if (RectArr[i].name == "Hint_BackGroundImage")
                Hint_BackGroundImage = RectArr[i].gameObject.GetComponent<Image>();


        }


        if (playerTransform == null || Flag_RectTransform ==null || Flag_Text == null || Flag_Image==null || Hint_Text==null || Hint_BackGroundImage==null)
        {
            Debug.LogError("MissionLocationFlagController: Init Failure");
            init = false;
        }
        else init = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (init == false) return;

        //显示小旗子
        if(isFlagDisplayed)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(desPos);

            if (screenPos.z < 0)
            {
                Flag_Text.enabled = false;
                Flag_Image.enabled = false;
            }
            else
            {
                Flag_RectTransform.position = screenPos;

                int dis = (int)Vector3.Distance(playerTransform.position, desPos);
                Flag_Text.text = dis.ToString();

                Flag_Text.enabled = true;
                Flag_Image.enabled = true;
            }
        }
        else
        {
            Flag_Text.enabled = false;
            Flag_Image.enabled = false;
        }

        //显示任务提示
        if(isHintDisplayed)
        {


            Hint_Text.enabled = true;
            Hint_BackGroundImage.enabled = true;
        }
        else
        {
            Hint_Text.enabled = false;
            Hint_BackGroundImage.enabled = false;
        }
    
    }
    
}
