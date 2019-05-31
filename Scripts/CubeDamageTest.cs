using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDamageTest : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInterface playerInterface;
    private void Awake()
    {
        playerInterface = GameObject.Find("Player").GetComponent<PlayerInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            playerInterface.Hit(gameObject.transform,50);
        }
    }
}
