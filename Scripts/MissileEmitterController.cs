using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEmitterController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject missile;
    Vector3 pos;
    void Awake()
    {
        missile = Resources.Load("Prefabs/Missile") as GameObject;
    }

    private void Start()
    {
        Emit();
    }

    // Update is called once per frame
    public void Emit(int count=10,float dis=10)
    {


        for(int i=1;i<=count;i++)
        {
            pos = transform.position + transform.forward*i*dis;

            //Invoke()

            Instantiate(missile, pos, transform.rotation);
        }
    }



}
