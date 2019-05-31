using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    // Start is called before the first frame update
    static GameObject  explosionEffect;


    private void Awake()
    {
        if(explosionEffect==null)
        {
            explosionEffect = Resources.Load("Prefabs/BigExplosionEffect") as GameObject;
        }
    }

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.right * 80;
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {

        GameObject tmp = Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject, 10);

    }



}
