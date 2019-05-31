using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpdeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            PlayerInterface playerInterface = GameObject.Find("Player").GetComponent<PlayerInterface>();
            playerInterface.Hit(transform, 100);
        }
    }
}
