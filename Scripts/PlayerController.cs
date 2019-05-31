using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Weapon weapon;
    public Light spotLight;
    private CameraController cameraController;
    private PlayerInterface playerInterface;

    private int HP;
    private const int HPUpperLimit = 100;
    const float recoverHPCD = 2;
    const float canotRecoverCD = 3;
    public float canotRecoverTimer = 0;
    float HPTimer;

    public bool isAlive;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraController = GameObject.Find("MainCamera").GetComponent<CameraController>();
        playerInterface = GameObject.Find("Player").GetComponent<PlayerInterface>();
        weapon = new Weapon_Thompson();
        spotLight = GameObject.Find("Spot Light").GetComponent<Light>();

        HP = 100;
        isAlive = true;

        
        GetComponentInChildren<AudioManager>().MusicPlayer.volume = GlobalVariable.BGMVolume;
        GetComponentInChildren<AudioManager>().SoundPlayer.volume = GlobalVariable.SoundVolume;

        playerInterface.init(gameObject);
        playerInterface.SendNumberOfBulletsInMagazine();
        playerInterface.SendNumberOfRemainingAmmo();
    }


    void Update()
    {
        if (isAlive == false) return;
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

        if (Input.GetMouseButton(1))
            weapon.ZoomIn();
        else 
            weapon.ZoomOut();

        if (Input.GetMouseButton(0))
            weapon.Fire();
        else
            weapon.StopFire();



        if (Input.GetKeyDown(KeyCode.R))
            weapon.Reload();

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0)
            weapon.Run();
        else
            weapon.StopRun();

        if (Input.GetKeyDown(KeyCode.E))
            spotLight.enabled = !spotLight.enabled;

       if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.1 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1)
            weapon.Walk();
        else
            weapon.StopWalk();


        //自动恢复血量
        if(isAlive)
        {
            if (canotRecoverTimer >= canotRecoverCD)
            {     
                if(HP < HPUpperLimit)
                {
                    if (HPTimer >= recoverHPCD)
                    {                     
                        deltaHP(20);
                        HPTimer = 0;                      
                    }
                    else
                        HPTimer += Time.deltaTime;
                }
                
                canotRecoverTimer = canotRecoverCD;
            }
            else
                canotRecoverTimer += Time.deltaTime;
        }
        
    }
    
    public int getHP()
    {
        return HP;
    }

    public void deltaHP(int delta)
    {
        HP += delta;
        HP = Mathf.Min(HP, HPUpperLimit);
        HP = Mathf.Max(0, HP);

        playerInterface.SendHP();

        //Debug.Log(HP);

        if (HP<=0 && isAlive)
        {
            isAlive = false;
            playerInterface.PlayerDie();
            
            //关闭自身摄像头
            Camera[] cameras = GetComponents<Camera>();
            foreach (Camera c in cameras)
                c.enabled = false;

            cameras = GetComponentsInChildren<Camera>();
            foreach (Camera c in cameras)
                c.enabled = false;

            cameraController.gameObject.tag = "Untagged";
            
            //启动死亡视角摄像头
            GameObject.Find("DeathCamera").GetComponent<DeathCameraController>().Work();
            

            //播放死亡动画
            //通知游戏结束
        }
    }
    
    

}
