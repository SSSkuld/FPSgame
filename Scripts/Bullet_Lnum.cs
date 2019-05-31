using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bullet_Lnum : MonoBehaviour
{
    private GameObject All_Bullet;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            this.GetComponent<Text>().color = new Color(1, 1, 1);
        }
    }


    public void ShowBullet(int L_Bullet)
    {
        this.GetComponent<Text>().text = "" + L_Bullet;
        if (L_Bullet <= 10)
            this.GetComponent<Text>().color = new Color(0.8f, 0, 0);
    }
    public void ShowAllBullet(int Bullet_Num)
    {
        if(All_Bullet==null)
            All_Bullet =GameObject.Find("All_Bullet");

        All_Bullet.GetComponent<Text>().text = "" + Bullet_Num;
    }

}
