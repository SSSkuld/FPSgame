using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaptionDisplayController : MonoBehaviour
{
    // Start is called before the first frame update

    public struct caption
    {
        public string str;
        public float timer;
    }

    Text text;
    public Queue<caption> q;
    caption now;


    private void Awake()
    {
        now = new caption();
        now.str = "123";
        now.timer = -1;

        text = gameObject.GetComponent<Text>();
        q = new Queue<caption>();
    }
   

    // Update is called once per frame
    void Update()
    {
        text.text = now.str;

        now.timer -= Time.deltaTime;
        now.timer = Mathf.Max(now.timer, -1f);

        if (now.timer <= 0)
        {
            now.str = "123";
            if (q.Count > 0)
                now = q.Dequeue();
        }
    }

    public void push(string str,float timer=2.5f)
    {
        caption c;
        c.str = str;
        c.timer = timer;
        q.Enqueue(c);
    }

}
