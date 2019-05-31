using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryGirl : MonoBehaviour
{

    MissionLocationFlagController mission;

    MissionSet elemission;
    private bool firstin = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        mission = GameObject.Find("MissionGuide").GetComponent<MissionLocationFlagController>();
        elemission = GameObject.Find("spfloorPlane").GetComponent<MissionSet>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            mission.isFlagDisplayed = true;
            mission.isHintDisplayed = true;
            mission.desPos = new Vector3(-14.4f, 0f, 8f);
            mission.hintContent = "离开医院";

            elemission.enabled = true;


            Destroy(gameObject);

        }
    }

}
