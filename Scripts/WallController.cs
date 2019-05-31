using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject explosionCenter;
    public float explosionPower;
    public float explosionRadius;
    private static GameObject brick;
    GameObject bigExplosionEffect;
    GameObject[] brickArr;

    private void Awake()
    {
        Transform[] son = gameObject.GetComponentsInChildren<Transform>();

        foreach(Transform i in son)
        {
            if(i.name=="ExplosionCenter")
            {
                explosionCenter = i.gameObject;
                break;
            }
        }
        bigExplosionEffect = Resources.Load("Prefabs/BigExplosionEffect") as GameObject;
        if (brick == null) brick = Resources.Load("Prefabs/Brick") as GameObject;
    }

    public void Explode()
    {
        for(int i=0;i<brickArr.Length;i++)
        {
            if(brickArr[i])
            {
                brickArr[i].GetComponent<Rigidbody>().isKinematic = false;
            }
        }

        Collider[] colliders = Physics.OverlapSphere(explosionCenter.transform.position, explosionRadius);
        foreach (Collider hits in colliders)  //遍历碰撞器数组
        {
            //如果这个物体有刚体组件
            if (hits.GetComponent<Rigidbody>())
            {
                //给定爆炸力大小，爆炸点，爆炸半径
                //利用刚体组件添加爆炸力AddExplosionForce
                //hits.GetComponent<Rigidbody>().isKinematic = false;
               hits.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionCenter.transform.position, explosionRadius);
            }
        }

        GameObject tmp = Instantiate(bigExplosionEffect, explosionCenter.transform);

        //***
        //Resource
        //gameObject.AddComponent<audiosource>

        //***
        Destroy(tmp, 5);
    }

    public void GenerateWall()
    {
        Vector3 pos = transform.position;
        float width = brick.transform.lossyScale.x;
        float height = brick.transform.lossyScale.y;

        brickArr = new GameObject[20 * 10];

        for (int i=-5;i<=5;i++)
        {
            for(int j=0;j<10;j++)
            {
                if(j%2==0)
                    brickArr[(i+5)*10+j]= Instantiate(brick, pos + transform.right*width * i + transform.up*  j* height, transform.rotation, transform);  
                else
                    brickArr[(i + 5) * 10 + j]=Instantiate(brick, pos + transform.right * width * i + transform.up * j * height + transform.right*0.25f, transform.rotation, transform);
            }        
        }
    }

    private void Start()
    {
        GenerateWall();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Explode();
        }
    }
}
