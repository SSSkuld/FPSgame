using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieRespawnController : MonoBehaviour
{
    static GameObject zoombieGirl;
    static GameObject zoombieBoy;

    int zombieOnlineCnt;//自身刷新的僵尸数量
    public int zombieOfflineCnt;//该点还需刷新的僵尸数量

    public int zombieOnlineCntUpperLimit=5;//僵尸数量上限

    public float respawnCD = 5;//刷新CD
    float respawnTimer;

    bool respawnEnable=true;

    public bool canstart = false;

    public bool IgnoreDistanceAndBarrier;//刷新的僵尸是否无视距离追击主角

    private void Awake()
    {
        if(zoombieGirl == null)
        {
            zoombieGirl = Resources.Load("Prefabs/zombiegirl") as GameObject;
        }
    }


    void Update()
    {
        //判断是否满足刷新条件
        if(zombieOnlineCnt < zombieOnlineCntUpperLimit && zombieOfflineCnt>0 && respawnEnable && canstart)
        {
            if (respawnTimer <= 0)
            {
                //实例化僵尸
                Foundnattact zombieCur = Instantiate(zoombieGirl, gameObject.transform.position, gameObject.transform.rotation).GetComponent<Foundnattact>();
                zombieCur.SetSource(this);
                zombieCur.SetIgnoreDistanceAndBarrier(IgnoreDistanceAndBarrier);
                zombieOnlineCnt++;
                zombieOfflineCnt--;
                respawnTimer = respawnCD;
            }
            else
                respawnTimer -= Time.deltaTime;
        }
    }

    public void zoombieDie()
    {
        zombieOnlineCnt--;
        if (zombieOnlineCnt == 0) respawnEnable = true;
    }

    public void AddZombie(int cnt)
    {
        if(cnt>=0)
            zombieOfflineCnt += cnt;
    }

    public void ClearOfflineZombie()
    {
        zombieOfflineCnt = 0;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster") respawnEnable = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Monster") respawnEnable = true;
    }
}
