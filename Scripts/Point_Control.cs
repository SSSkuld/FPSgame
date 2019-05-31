using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Point_Control : MonoBehaviour
{
    private GameObject Point_star_1, Point_star_2,Point_star_3;
    private GameObject P_camera;
    private float TransNum = 1;
    int Point_State = 0;
    // Start is called before the first frame update
    void Start()
    {
        Point_star_1 = GameObject.Find("Aim Point 1");
        Point_star_2 = GameObject.Find("Aim Point 2");
        Point_star_3 = GameObject.Find("Aim Point 3");
        //P_camera = GameObject.Find("Main Camera");
        Point_star_2.SetActive(false);
        Point_star_3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      
        Point_Method(Point_State);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            Point_star_1.GetComponent<Animation>().Play("Point_1_01");
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            Point_star_1.GetComponent<Animation>().Play("Point_1_02");
        }
        /*if (Input.GetKey(KeyCode.K))
        {
            hit_target();
        }*/
    }
    public void Point_Method(int Point_State)
    {
        if (Point_State == 1)
        {
            Point_star_1.SetActive(false);
            Point_star_2.SetActive(true);
            //           P_camera.GetComponent<Camera>().fieldOfView = 20;
          

        }
        if (Point_State == 2)
        {
            Point_star_1.SetActive(true);
            Point_star_2.SetActive(false);
            //          P_camera.GetComponent<Camera>().fieldOfView = 60;
           
        }
    }
    public void hit_target()
    {
        Point_star_3.SetActive(true);
        TransNum = 1;
        InvokeRepeating("Game_Disapper", 0.01f, 0.08f);
    }
    
    public void Game_Disapper()
    {

        TransNum -= 0.2f;
        Point_star_3.GetComponent<Image>().color = new Color(1, 1, 1, TransNum);
        if (TransNum <= 0)
        {
            CancelInvoke();
        }
    }
}
