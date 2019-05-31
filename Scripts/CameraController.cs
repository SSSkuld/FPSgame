using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    //武器开火后座力系统
    //
    float curAngleX;
    float curAngleY;

    float destinationAngleX;
    float destinationAngleY;

    /////////////////////射击
    float offsetAngleX_OneShot;//一次射击所要改变的X轴的角度
    float offsetAngleY_OneShot;

    float offsetAngleX_Shot;
    float offsetAngleY_Shot;

    ////////////////////受伤
    float offsetAngleX_Hit;
    float offsetAngleY_Hit;

    const float eps = 1e-3f;

    Weapon weapon;
    public new GameObject  camera;
    bool aiming;

    float curFieldOfView;
    float destinationFieldOfView;
    const float speedOfChangeFieldOfView = 62;//92

    private GameObject modelCamera;
    private GameObject preUICamera;
    private PlayerController playerController;
    private PostProcessVolume postProcessVolume;
    private DepthOfField depthOfField;

    private float curFocusDistance;
    private float destinationFocusDistance;

    private float curFocalLength;
    private float desFocalLength;

    private void Awake()
    {
        curFieldOfView = destinationFieldOfView = 60;
        modelCamera = GameObject.Find("ModelCamera");
        preUICamera = GameObject.Find("PreUICamera");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        //postProcessVolume = gameObject.GetComponent<PostProcessVolume>();
        depthOfField = Resources.Load<PostProcessProfile>("Camera/CameraEffect").GetSetting<DepthOfField>();

        curFocusDistance = 5;
        destinationFocusDistance = 5;
        curFocalLength = 30;
        desFocalLength = 30;

        depthOfField.focusDistance.Interp(0, curFocusDistance, 1);
        depthOfField.focalLength.Interp(0, curFocalLength, 1);
    }

    public void setAimingStatus (bool status)
    {
        aiming = status;
        if (aiming) destinationFieldOfView = 50;
        else destinationFieldOfView = 60;
    }

    void GenerateDeltaAngle_OneShot()
    {
        if(aiming)
        {
            offsetAngleY_OneShot = Random.Range(-0.7f, 0.2f);
            offsetAngleX_OneShot = 0.3f;
        }
        else
        {
            offsetAngleY_OneShot = Random.Range(-2.0f, 0.6f);
            offsetAngleX_OneShot = 0.7f;
        }
        offsetAngleX_OneShot *= (Random.Range(0.0f, 1.0f) > 0.5f) ? -1.0f : 1.0f;

    }

    public void Hit()
    {
        offsetAngleX_Hit = 0;
        offsetAngleY_Hit = -20;
    }

    public void Shoot()
    {

        GenerateDeltaAngle_OneShot();

        offsetAngleX_Shot += offsetAngleX_OneShot;
        offsetAngleY_Shot += offsetAngleY_OneShot;

        if(aiming)
        {
            offsetAngleY_Shot = Mathf.Max(offsetAngleY_Shot, -2.0f);
            offsetAngleY_Shot = Mathf.Min(offsetAngleY_Shot, 0);

            offsetAngleX_Shot = Mathf.Max(offsetAngleX_Shot, -1.5f);
            offsetAngleX_Shot = Mathf.Min(offsetAngleX_Shot, 1.5f);
        }
        else
        {
            offsetAngleY_Shot = Mathf.Max(offsetAngleY_Shot, -4.0f);
            offsetAngleY_Shot = Mathf.Min(offsetAngleY_Shot, -2f);

            offsetAngleX_Shot = Mathf.Max(offsetAngleX_Shot, -3.0f);
            offsetAngleX_Shot = Mathf.Min(offsetAngleX_Shot, 3.0f);
        }


    }

    public void DeltaHp()
    {
        //32 30

        int hp = playerController.getHP();
        if (hp >= 80)
        {
            destinationFocusDistance = 5;
            desFocalLength = 30;
        }
        else if (60 <= hp && hp < 80)
        {
            destinationFocusDistance = 2f;
            desFocalLength = 95;
        }
        else if (40 <= hp && hp < 60)
        {
            destinationFocusDistance = 1.5f;
            desFocalLength = 100;
        }
        else if (20 <= hp && hp < 40)
        {
            destinationFocusDistance = 1.3f;
            desFocalLength = 110;
        }
        else
        {
            destinationFocusDistance = 1.3f;
            desFocalLength = 120;
        }
    }

    private void CameraEffect_DepthOfField()
    {
        float focusSpeed = 10;
        curFocusDistance = Mathf.Lerp(curFocusDistance, destinationFocusDistance, Time.deltaTime * focusSpeed);
        if (Mathf.Abs(curFocusDistance - destinationFocusDistance) < 0.01) curFocusDistance = destinationFocusDistance;
        //Debug.Log(curFocusDistance);
        depthOfField.focusDistance.Interp(0, 100, (float)(1.0 * curFocusDistance / 100));

        float focalLengthSpeed = 10;
        curFocalLength = Mathf.Lerp(curFocalLength, desFocalLength, Time.deltaTime * focalLengthSpeed);
        if (Mathf.Abs(curFocalLength - desFocalLength) < 0.01) curFocalLength = desFocalLength;
        //Debug.Log(curFocusDistance);
        depthOfField.focalLength.Interp(0, 100, (float)(1.0 * curFocalLength / 100));
    }

 

    private void Update()
    {
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;
        //计算焦距的线性回复
        CameraEffect_DepthOfField();

        //depthOfField.focusDistance.Interp(0, 100, (float)(1.0 * 0.3 / 100));
        //分别计算射击和被击导致的屏幕跳动的线性回复
        float recoverSpeed = 10;

        if (Mathf.Abs(offsetAngleX_Shot) > eps)
            offsetAngleX_Shot = Mathf.Lerp(offsetAngleX_Shot, 0, Time.deltaTime * recoverSpeed);
        else offsetAngleX_Shot = 0;

        if (Mathf.Abs(offsetAngleY_Shot) > eps)
            offsetAngleY_Shot = Mathf.Lerp(offsetAngleY_Shot, 0, Time.deltaTime * recoverSpeed);
        else offsetAngleY_Shot = 0;

        //////////////////

        if (Mathf.Abs(offsetAngleX_Hit) > eps)
            offsetAngleX_Hit = Mathf.Lerp(offsetAngleX_Hit, 0, Time.deltaTime * recoverSpeed);
        else offsetAngleX_Hit = 0;

        if (Mathf.Abs(offsetAngleY_Hit) > eps)
            offsetAngleY_Hit = Mathf.Lerp(offsetAngleY_Hit, 0, Time.deltaTime * recoverSpeed);
        else offsetAngleY_Hit = 0;

        //更新destination
        destinationAngleX = offsetAngleX_Hit + offsetAngleX_Shot;
        destinationAngleY = offsetAngleY_Hit + offsetAngleY_Shot;

        //更新cur
        float moveSpeed = 10;

        if (Mathf.Abs(curAngleX - destinationAngleX) > eps)
            curAngleX = Mathf.Lerp(curAngleX, destinationAngleX, Time.deltaTime * moveSpeed);
        else curAngleX = destinationAngleX;

        if (Mathf.Abs(curAngleY - destinationAngleY) > eps)
            curAngleY = Mathf.Lerp(curAngleY, destinationAngleY, Time.deltaTime * moveSpeed);
        else
            curAngleY = destinationAngleY;

        camera.transform.eulerAngles = new Vector3(camera.transform.eulerAngles.x + curAngleY,
            camera.transform.eulerAngles.y + curAngleX,
            camera.transform.eulerAngles.z);

        ////////////更改视野大小
        if (destinationFieldOfView > curFieldOfView)
        {
            curFieldOfView += speedOfChangeFieldOfView * Time.deltaTime;
            if (curFieldOfView > destinationFieldOfView) curFieldOfView = destinationFieldOfView;
        }
        else
        {
            curFieldOfView -= speedOfChangeFieldOfView * Time.deltaTime;
            if (curFieldOfView < destinationFieldOfView) curFieldOfView = destinationFieldOfView;
        }
        camera.GetComponent<Camera>().fieldOfView = curFieldOfView;
    }

}
