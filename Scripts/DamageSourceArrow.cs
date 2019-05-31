using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSourceArrow : MonoBehaviour
{
    private Transform playerTransform;
    private Transform damageSourceTransform;
    private bool isInit;
    private const float fadeSpeed=0.5f;
    private float alpha = 1;
    private int diameter;
    Image image;

    public void Init(Transform _playerTransform,   Transform _damageSourceTransform)
        //初始化
    {
        playerTransform = _playerTransform;
        damageSourceTransform = _damageSourceTransform;

        isInit = true;
    }

    private void Awake()
    {
        image = gameObject.GetComponent<Image>();
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        //Sprite sp = Resources.Load("Resources/UI/DamageIndicator/arrow", typeof(Sprite)) as Sprite;
        //imgArrow.sprite = sp;
        diameter = Screen.height * 2 / 3;
        rect.sizeDelta = new Vector2(diameter, diameter);
        alpha = 1;
        image.color = new Color(0f, 0f, 0f, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (isInit == false) return;

        image.color = new Color(0.7924528f, 0f, 0f, alpha);
        Vector3 fromVector, toVector;
        toVector = playerTransform.forward.normalized;
        fromVector = (damageSourceTransform.position - playerTransform.position).normalized;

        fromVector.y = 0;

        float angle = Vector3.Angle(fromVector, toVector); //求出两向量之间的夹角 

        //Debug.Log(angle);

        Vector3 normal;
        if (angle < 1)
            normal = playerTransform.up;
        else
            normal = Vector3.Cross(fromVector, toVector).normalized;//叉乘求出法线向量 
        //angle *= Mathf.Sign(Vector3.Dot(normal, playerTransform.up));  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向 

        angle *= Mathf.Sign(Vector3.Dot(normal, playerTransform.up));
        if (angle < 0) angle += 360;
        
        gameObject.transform.localEulerAngles = new Vector3(0, 0, angle);

        fadeOut();
    }

    void fadeOut()
    {
        alpha -= Time.deltaTime * fadeSpeed;
        alpha = Mathf.Max(0f, alpha);

        image.color = new Color(0.7924528f, 0f, 0f, alpha);
        if (alpha < 0.01) Destroy(gameObject);
    }
}
