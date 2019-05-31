using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class booktalk : MonoBehaviour
{
    public string ChatName;

    private bool canChat = false;

    public bool cantoc2 = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canChat = true;
            cantoc2 = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            canChat = false;
    }

    // Update is called once per frame
    void Update()
    {
        Say();
    }

    void Say()
    {
        if (canChat)
        {
            Flowchart flowchart = GameObject.Find("Flowchart").GetComponent<Flowchart>();
            if (flowchart.HasBlock(ChatName))
            {
                flowchart.ExecuteBlock(ChatName);
            }
        }
    }
}
