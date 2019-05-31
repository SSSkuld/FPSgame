using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_zombie : MonoBehaviour
{
    // Start is called before the first frame update
    
    private int flag=0;//判断是否碰撞过

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ZombieRespawnController>().enabled = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag=="Player")
        {
            //如果玩家和cube发生碰撞
            foreach (Transform child in transform)
            {
                child.GetComponent<ZombieRespawnController>().enabled = true;
            }
        }
        //进入触发器执行的代码
    }

    void Update()
    {
        
    }
}
