using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    
    public string weaponName;//武器名称

    public int magazineLeft;//弹夹内子弹剩余数量
    public int magazineCapacity;//弹夹内子弹数目上限
    public int ammoLeft;//总子弹剩余数目
    public int ammoCapacity;

    public float shotInterval;
    public float timer;

    protected GameObject weaponPrefab;//枪械模型预制体
    public Animator modelAnimator;//负责枪械各个动画的动画控制体
    protected Animator cameraAnimator;//相机动画

    protected AudioManager audioManager;
    protected AudioClip shotSound;//射击音效
    protected AudioClip reloadSound;//装填音效
    protected AudioClip emptyShoutSound;//无子弹时射击音效

    protected GameObject muzzleSmokeNoAiming;//枪口烟雾
    protected GameObject muzzleSmokeAiming;//枪口烟雾
    protected GameObject muzzleFlashNoAiming;//枪口火焰
    protected GameObject muzzleFlashAiming;//枪口火焰

    protected GameObject camera;
    protected GameObject bulletPrefab;//子弹预制体

    protected GameObject shotPosition;//子弹出膛点所在位置物件

    protected RaycastHit raycastHit;
    public CameraController cameraController;
    protected PlayerInterface playerInterface;

    public bool firing
    {
        get
        {
            return modelAnimator.GetBool("fire");
        }
        set
        {
            //if (modelAnimator.GetBool("fire") && value) return;
      
            modelAnimator.SetBool("fire",value);
        }
    }

    public bool aiming
    {
        get
        {
            return modelAnimator.GetBool("aim");
        }
        set
        {
            modelAnimator.SetBool("aim", value);
           // cameraController.setAimingStatus(value);
        }
    }

    public bool reloading
    {
        get
        {
            return modelAnimator.GetBool("reload");
        }
        set
        {
            modelAnimator.SetBool("reload", value);
        }
    }

    public bool walking
    {
        get
        {
            return modelAnimator.GetBool("walk")  ;
        }
        set
        {
            modelAnimator.SetBool("walk", value);
        }
    }

    public bool running//running=true时walking=true
    {
        get
        {
            return modelAnimator.GetBool("run") ;
        }
        set
        {
            modelAnimator.SetBool("run", value);
        }
    }

    public bool weaponReady//为了鉴别武器是否播完开头的展示动画
    {
        get
        {
            return modelAnimator.GetBool("weaponReady");
        }
        set
        {
            modelAnimator.SetBool("weaponReady",value);
        }
    }

    protected Weapon()
    {     
        muzzleSmokeNoAiming = Resources.Load("Weapons/MuzzleBlackSmoke") as GameObject;
        muzzleSmokeAiming = Resources.Load("Weapons/MuzzleBlackSmoke") as GameObject;

        muzzleFlashAiming = Resources.Load("Weapons/MuzzleFlashEffect") as GameObject;
        muzzleFlashNoAiming = Resources.Load("Weapons/MuzzleFlashEffect") as GameObject;


        camera = Camera.main.gameObject;
        cameraController = camera.GetComponent<CameraController>();

        playerInterface = GameObject.Find("Player").GetComponent<PlayerInterface>();
    }

    //无法开火返回false，否则返回true
    public virtual bool Fire()
    {
        if (reloading || running )
            return false;

        //if (weaponReady == false) return false;

        if (magazineLeft == 0 )
        {
            Reload();
            return false;
        }


        //判断开火间隔
        timer += Time.deltaTime;
        if (timer < shotInterval)
            return false;
        else timer = 0;

        
        magazineLeft--;
        playerInterface.SendNumberOfBulletsInMagazine();
        
        //开火动画
        firing = true;

        //开火音效
        audioManager.PlaySound(shotSound);


        //子弹射击
        Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward), 
            out raycastHit,Mathf.Infinity,1, QueryTriggerInteraction.Ignore);
        //Physics.Raycast(camera.transform.position, camera.transform.TransformDirection(Vector3.forward),
          // out raycastHit, Mathf.Infinity, 0, QueryTriggerInteraction.Ignore);


        return true;
    }

    public virtual void SupplementAmmo(int num)
    {
        ammoLeft += num;
        ammoLeft = Mathf.Min(ammoLeft, ammoCapacity);

        playerInterface.SendNumberOfRemainingAmmo();
    }

    public virtual bool Reload()
    {
        if (ammoLeft == 0 || magazineLeft == magazineCapacity ) return false;

        //播放reload动画，更改子弹等逻辑由动画触发
        reloading = true;

        return true;
    }

    public virtual void StopFire()
    {
        firing = false;
        //camera.GetComponent<Animator>().SetBool("fire", false);
    }

    public virtual void ZoomIn() {
        if (reloading || firing) return;
        aiming = true;
    }

    public virtual void ZoomOut()
    {
        if (reloading || firing) return;
        aiming = false;
    }

    public virtual void Run()
    {
        if (reloading) return;
        running = true;
    }

    public virtual void StopRun()
    {
        running = false;
    }

    public virtual void Walk()
    {
        walking = true;
    }

    public virtual void StopWalk()
    {
        walking = false;
        running = false;
    }

}
