using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityStandardAssets.Characters.FirstPerson;

//该脚本挂载到主角身上
//获取方式
//GameObject.find("Player").getComponent<PlayerInterface>();
public class PlayerInterface : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private new GameObject camera;
    private PlayerController playerController;
    //private Weapon weapon;
    private GameObject damageSourceArrow;
    private GameObject damageIndicatorCanvas;
    private CameraController cameraController;

    private AudioManager audioManager;

    public bool isLockCursor
    {
        get
        {
            return player.GetComponent<RigidbodyFirstPersonController>().mouseLook.lockCursor;
        }
        set
        {
            player.GetComponent<RigidbodyFirstPersonController>().mouseLook.lockCursor = value;
        }
    }


    public void init(GameObject _player)
    {
        player = _player;
        //player = GameObject.Find("Player");
        camera = GameObject.Find("MainCamera");
        playerController = player.GetComponent<PlayerController>();

        damageSourceArrow = Resources.Load("UI/DamageIndicator/DamageSourceArrow") as GameObject;
        damageIndicatorCanvas = GameObject.Find("DamageIndicatorCanvas");
        cameraController = camera.GetComponent<CameraController>();
        audioManager = player.GetComponentInChildren<AudioManager>();
    }

    public string GetWeaponName()
    {
        return playerController.weapon.weaponName;
    }

    //当HP发生调用时playerController会调用该方法
    public void SendHP()
    {
        // playerController.getHP();
        //todo
        GameObject.Find("HP").SendMessage("ShowBlood", playerController.getHP());
        cameraController.DeltaHp();
    }

    public int GetHp()
    {
        return playerController.getHP();
    }

    //通知当前弹匣中子弹的剩余数量
    public void SendNumberOfBulletsInMagazine()
    {
        //weapon.magazineLeft
        //todo
        //while (playerController.weapon == null) 
        GameObject.Find("Left_Bullet").SendMessage("ShowBullet", playerController.weapon.magazineLeft);
    }
    //通知当前携带子弹的剩余数量
    public void SendNumberOfRemainingAmmo()
    {
        //weapon.ammoLeft;
        //todo
        //Debug.Log(playerController.weapon.ammoLeft);
        GameObject.Find("Left_Bullet").SendMessage("ShowAllBullet", playerController.weapon.ammoLeft);
    }

    //当HP==0时playerController会调用该方法
    public void PlayerDie()
    {
        //todo 向GUI发送主角死亡消息
       
    }

    //当主角瞄准时会调用该方法
    public void Aim()
    {
        //todo 向GUI发送主角正在瞄准的状态，更新准星样式
        GameObject.Find("Aim_Point").SendMessage("Point_Method", 1);
    }

    //当主角取消瞄准时会调用该方法
    public void CancelAim()
    {
        //todo
      //  if (GameObject.Find("Aim_Point") == null) Debug.Log("zlc");

        GameObject.Find("Aim_Point").SendMessage("Point_Method", 2);
    }

    //当主角命中目标时会调用该方法
    public void HitTarget()
    {
        //todo 更改准心样式\
        GameObject.Find("Aim_Point").SendMessage("hit_target");
    }

    //当主角受到伤害时会调用该方法
    public void Hit(Transform transformOfDamage,int damage =0)
    {
        //显示伤害来源
        GameObject tmp = Instantiate(damageSourceArrow, damageIndicatorCanvas.transform);
        DamageSourceArrow dsa = tmp.GetComponent<DamageSourceArrow>();
        dsa.Init(gameObject.transform, transformOfDamage);

        //视觉上跳,调整焦距
        playerController.deltaHP(-damage);
        cameraController.Hit();
        playerController.canotRecoverTimer = 0;
    }

    //获取主角朝向
    public Quaternion GetPlayerRotation()
    {
        return camera.transform.rotation;
    }

    //获取主角朝向
    public Vector3 GetPlayerRotation_EulerAngles()
    {
        return camera.transform.eulerAngles;
    }

    //获取主角坐标
    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }

    public void SupplementAmmunition(int num=999)
    {
        playerController.weapon.SupplementAmmo(num);
    }

    public bool IsPlayerAlive()
    {
        return playerController.isAlive;
    }

    public void SetBGMVolume(float volume)
    {
        audioManager.MusicPlayer.volume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        audioManager.SoundPlayer.volume = volume;
    }

    public void SetMouseSensitivityX(float sensitivity)
    {
        player.GetComponent<RigidbodyFirstPersonController>().mouseLook.XSensitivity = sensitivity;
    }

    public void SetMouseSensitivityY(float sensitivity)
    {
        player.GetComponent<RigidbodyFirstPersonController>().mouseLook.YSensitivity = sensitivity;
    }

    public float GetBGMVolume( )
    {
        return audioManager.MusicPlayer.volume;
    }

    public float GetSoundVolume()
    {
        return audioManager.SoundPlayer.volume;
    }

    public float GetMouseSensitivityX( )
    {
        return player.GetComponent<RigidbodyFirstPersonController>().mouseLook.XSensitivity;
    }

    public float GetMouseSensitivityY( )
    {
        return player.GetComponent<RigidbodyFirstPersonController>().mouseLook.YSensitivity;
    }

   
}
