using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Thompson : Weapon
{
    private GameObject shotPositionAiming;
    private GameObject shotPositionNoAiming;

    public Weapon_Thompson(int _magazineLeft = 30, int _magazineCapacity = 30, int _ammoLeft = 600, int _ammoCapacity=600, float _shotInterval = 0.05f)
    {
        magazineLeft = _magazineLeft;
        magazineCapacity = _magazineCapacity;
        ammoLeft = _ammoLeft;
        shotInterval = _shotInterval;
        weaponName = "Thompson";
        ammoCapacity = _ammoCapacity;

        //获取玩家组件
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        //加载模型
        weaponPrefab = (GameObject)Resources.Load("Weapons/Weapon_Thompson");
        weaponPrefab = MonoBehaviour.Instantiate(weaponPrefab, camera.transform);
        //对模型位置进行微调
        //        weaponPrefab.transform.position = camera.transform.position + camera.transform.forward * -0.25f + camera.transform.up * -1.685f;
        weaponPrefab.transform.position = camera.transform.position + camera.transform.forward * -0.25f + camera.transform.up * -1.685f;

        //指定动画控制器
        modelAnimator = weaponPrefab.GetComponent<Animator>();
        modelAnimator.runtimeAnimatorController = (RuntimeAnimatorController)Resources.Load("Weapons/Animator_Controller_Thompson");

        //todo:加载声音组件
        audioManager = player.GetComponentInChildren<AudioManager>();
        shotSound = Resources.Load<AudioClip>("Weapons/Sound_Shot_Thompson");

        //加载子弹模型
        bulletPrefab = (GameObject)Resources.Load("Bullets/Bullet_Thompson");

        //寻找子弹发射点
        shotPositionAiming = GameObject.Find("ShotPositionAiming");
        shotPositionNoAiming = GameObject.Find("ShotPositionNoAiming");

        muzzleSmokeNoAiming= MonoBehaviour.Instantiate(muzzleSmokeNoAiming,shotPositionNoAiming.transform);
        muzzleSmokeAiming = MonoBehaviour.Instantiate(muzzleSmokeAiming, shotPositionAiming.transform);

        muzzleFlashAiming = MonoBehaviour.Instantiate(muzzleFlashAiming, shotPositionAiming.transform);
        muzzleFlashNoAiming = MonoBehaviour.Instantiate(muzzleFlashNoAiming, shotPositionNoAiming.transform);

        //遍历所有子组件，去除阴影效果
        for(int i=0;i<player.GetComponentsInChildren<SkinnedMeshRenderer>(true).Length;i++)
            player.GetComponentsInChildren<SkinnedMeshRenderer>(true)[i].shadowCastingMode 
                = UnityEngine.Rendering.ShadowCastingMode.Off;

        //playerInterface.SendNumberOfBulletsInMagazine();
        //playerInterface.SendNumberOfRemainingAmmo();
    }

    public override bool Fire()
    {
        //判断是否能发射子弹了
        if (base.Fire() == false) return false;

        /*Vector3 vShotPosition = shotPosition.GetComponent<SkinnedMeshRenderer>().bounds.center + 
            shotPosition.transform.forward*0.20f + shotPosition.transform.up * 0.05f + shotPosition.transform.right * 0.011f;*/
        //0.38 -0.12
        shotPosition = aiming ? shotPositionAiming : shotPositionNoAiming;

        Vector3 vHitPosition;

        if (raycastHit.collider == null)
            vHitPosition = camera.transform.forward * 10000000;
        else
            vHitPosition = raycastHit.point;



        //在枪口的位置实例化一颗子弹，按子弹发射点出的旋转，进行旋转

        GameObject bullet = MonoBehaviour.Instantiate(bulletPrefab, shotPosition.transform.position, Quaternion.LookRotation(vHitPosition - shotPosition.transform.position));

        cameraController.Shoot();

        //枪口烟雾
        if (aiming)
        {
            muzzleSmokeAiming.GetComponent<ParticleSystem>().Play();
            muzzleFlashAiming.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            muzzleSmokeNoAiming.GetComponent<ParticleSystem>().Play();
            muzzleFlashNoAiming.GetComponent<ParticleSystem>().Play();
        }



        return true;
    }

    
}
