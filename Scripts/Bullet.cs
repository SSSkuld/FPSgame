using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Bullet: MonoBehaviour
{
    public static GameObject smokePrefab;
    public static GameObject holePrefab_Default;//弹孔特效
    public static GameObject holePrefab_BloodSmall;
    public static PlayerInterface playerInterface;
    
    public int damage;//子弹伤害
    public int speed;//子弹速度

    protected bool isModelLoaded;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed ;
        Destroy(gameObject, 3f);
    }

   
}
