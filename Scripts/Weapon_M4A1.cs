using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_M4A1 : Weapon
{
    public Weapon_M4A1(int _magazineLeft=30, int _magazineCapacity = 30, int _ammoLeft=300, float _shotInterval = 0.08f)
    {
        magazineLeft = _magazineLeft;
        magazineCapacity = _magazineCapacity;
        ammoLeft = _ammoLeft;
        shotInterval = _shotInterval;
        weaponName = "M4A1";

        //获取玩家组件
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //加载模型
        GameObject camera = GameObject.Find("MainCamera");
        weaponPrefab = (GameObject)Resources.Load("Weapons/Weapon_M4A1");
        weaponPrefab = MonoBehaviour.Instantiate(weaponPrefab, camera.transform);
        //对模型位置进行微调
        weaponPrefab.transform.position = camera.transform.position + camera.transform.forward * -0.15f + camera.transform.up * -2.372f;

        //指定动画控制器
        modelAnimator = weaponPrefab.GetComponent<Animator>();
        modelAnimator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Weapons/Animator_Controller_M4A1");

        //todo:加载声音组件
        audioManager = player.GetComponentInChildren<AudioManager>();
        shotSound = Resources.Load< AudioClip>("Weapons/Sound_Shot_M4A1");

        //加载子弹模型
        bulletPrefab = (GameObject)Resources.Load("Bullets/Bullet_M4A1");

        //设定消音器为子弹发射点
        shotPosition = GameObject.Find("m4a1_silencer");
    }

    public override bool Fire()
    {
        //判断是否能发射子弹了
        if (base.Fire() == false) return false;


        //实例化子弹 //子弹从消音器渲染中心点发射
        MonoBehaviour.Instantiate(bulletPrefab, shotPosition.GetComponent<SkinnedMeshRenderer>().bounds.center
            , shotPosition.transform.rotation );
        return true;
    }

}
