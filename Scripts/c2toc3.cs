using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class c2toc3 : MonoBehaviour
{
    public string c3;
    rotate x;

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
        x = GameObject.Find("Radar").GetComponent<rotate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && x.cantoc3 == true)
        {
            SceneManager.LoadScene("c3");
        }
    }
}
