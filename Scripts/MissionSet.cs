using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSet : MonoBehaviour
{

    MissionLocationFlagController mission;

    //显示任务地点的小旗子是否显示
    public bool isFlagDisplayed;
    //是否显示任务显示
    public bool isHintDisplayed;
    public string Content;
    public Vector3 desPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        mission = GameObject.Find("MissionGuide").GetComponent<MissionLocationFlagController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            if (this.enabled == false)
                return;

            //Debug.Log("nmd, why?");
            mission.isFlagDisplayed = isFlagDisplayed;
            mission.isHintDisplayed = isHintDisplayed;
            mission.desPos = desPos;
            mission.hintContent = Content;

            gameObject.GetComponent<SphereCollider>().enabled = false;

        }
    }
}
