using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//该脚本挂载到主角身上
//获取方式
//GameObject.find("Player").getComponent<PlayerInterface>();
public class PlayerInterface1 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private new GameObject camera;
    private PlayerController playerController;
    private Weapon weapon;

    //todo
    //添加GUI的接口

    private void Awake()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("MainCamera");
        playerController = player.GetComponent<PlayerController>();
        weapon = playerController.weapon;
        //todo 初始化GUI的接口
    }

    public string GetWeaponName()
    {
        return weapon.weaponName;
    }

    //当HP发生调用时playerController会调用该方法
    public void SendHP()
    {
        // playerController.getHP();
        GameObject.Find("HP").SendMessage("ShowBlood", playerController.getHP());
        //todo
    }

    public int GetHp()
    {
        return playerController.getHP();
    }

    //通知当前弹匣中子弹的剩余数量
    public void SendNumberOfBulletsInMagazine()
    {
        //weapon.magazineLeft
        GameObject.Find("Left_Bullet").SendMessage("ShowBullet", weapon.magazineLeft);
        //todo
    }
    //通知当前携带子弹的剩余数量
    public void SendNumberOfRemainingAmmo()
    {
        //weapon.ammoLeft;
        GameObject.Find("Left_Bullet").SendMessage("ShowAllBullet", weapon.ammoLeft);
        //todo
    }

    //当HP==0时playerController会调用该方法
    public void PlayerDie()
    {
        //todo 向GUI发送主角死亡消息
    }

    //当主角瞄准时会调用该方法
    public void Aim()
    {
        GameObject.Find("Aim_Point").SendMessage("Point_Methord",1);
        //todo 向GUI发送主角正在瞄准的状态，更新准星样式
        
    }

    //当主角取消瞄准时会调用该方法
    public void CancelAim()
    {
        GameObject.Find("Aim_Point").SendMessage("Point_Methord", 0);
        //todo
    }

    //当主角命中目标时会调用该方法
    public void HitTarget()
    {
        //todo 更改准心样式
    }

    //当主角受到伤害时会调用该方法
    public void Hit(Vector3 PositionOfDamage)
    {
        //PositionOfDamage 为伤害来源的世界坐标
        //todo 向GUI发送伤害来源
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

}
