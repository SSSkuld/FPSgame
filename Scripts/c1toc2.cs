using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class c1toc2 : MonoBehaviour
{
    booktalk x;
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
        x = GameObject.Find("books").GetComponent<booktalk>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && x.cantoc2 == true)
        {
            SceneManager.LoadScene("c2");
        }
    }

}
