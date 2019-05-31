using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Bullet_Thompson : Bullet
{
   

    private void Awake()
    {
        damage = GlobalVariable.bulletDamage;
        //speed = 880;
        //speed = 90;
        speed = 200;
        //防止重复加载模型
        if (isModelLoaded == false)
        {
            smokePrefab = Resources.Load("Bullets/BulletBlackSmoke") as GameObject;
            holePrefab_Default = Resources.Load("Bullets/BulletImpactMetalEffectPlus") as GameObject;

            holePrefab_BloodSmall = Resources.Load("Bullets/BulletImpactFleshSmallEffect") as GameObject;
            playerInterface = GameObject.Find("Player").GetComponent<PlayerInterface>();

            isModelLoaded = true;
        }
    }
    //打在物体上
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
        {
            return;
        }

        Quaternion dir;
        GameObject tmpSmokePrefab;
        GameObject tmpHolePrefab;

        //溅血特效
        if (collision.collider.tag=="Monster")
        {
             dir = Quaternion.LookRotation(collision.contacts[0].normal);

             tmpSmokePrefab = MonoBehaviour.Instantiate(smokePrefab, collision.contacts[0].point, dir);
             tmpHolePrefab = MonoBehaviour.Instantiate(holePrefab_BloodSmall, collision.contacts[0].point + collision.contacts[0].normal * 0.01f, dir);

            playerInterface.HitTarget();
        }
        else
        {
             dir = Quaternion.LookRotation(collision.contacts[0].normal);

             tmpSmokePrefab = MonoBehaviour.Instantiate(smokePrefab, collision.contacts[0].point, dir);
             tmpHolePrefab = MonoBehaviour.Instantiate(holePrefab_Default, collision.contacts[0].point + collision.contacts[0].normal * 0.01f, dir);
        }

        tmpSmokePrefab.transform.SetParent(collision.transform, true);
        tmpHolePrefab.transform.SetParent(collision.transform,true);

        Destroy(tmpSmokePrefab, 2);

        //废弃该子弹，不再发生碰撞
        gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        Destroy(gameObject,3);
    }


}
