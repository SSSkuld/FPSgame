using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class Menu_Manager : MonoBehaviour
{
    private Slider V_Slider,Back_Slider,Mouse_Slider;
    public GameObject Menu_01, Menu_02;
    public bool IsGamePaused;
    public GameObject G_Menu_UI;
    int menu = 0;
    private Text Mouse, Back, Volum;
    private PlayerInterface playerInterface;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInterface = GameObject.Find("Player").GetComponent<PlayerInterface>();
    }
    void Start()
    {
        V_Slider = GameObject.Find("Volume_Change").GetComponent<Slider>();
        Back_Slider = GameObject.Find("BackVolume_Change").GetComponent<Slider>();
        Mouse_Slider = GameObject.Find("Mouse_Sensitivity").GetComponent<Slider>();
        Mouse = GameObject.Find("Text_Mouse_Show").GetComponent<Text>();
        Back = GameObject.Find("Text_Back_Show").GetComponent<Text>();
        Volum = GameObject.Find("Text_Volum_Show").GetComponent<Text>();
        Back.text = Back_Slider.value.ToString("f2");
        Volum.text = V_Slider.value.ToString("f2");
        Mouse.text = Mouse_Slider.value.ToString("f1");



        G_Menu_UI.SetActive(false);
    }
    public void Manager_Choose()
    {
        G_Menu_UI.SetActive(true);
        Menu_01.SetActive(true);
        Menu_02.SetActive(false);

        V_Slider.value = GlobalVariable.SoundVolume;
        Back_Slider.value = GlobalVariable.BGMVolume;
        Mouse_Slider.value = playerInterface.GetMouseSensitivityX();
    }
    public void Continue_Click()
    {
        Menu_01.SetActive(false);
        Menu_02.SetActive(true);
    }
    public void Back_Click()
    {
        Menu_01.SetActive(true);
        Menu_02.SetActive(false);
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu = menu % 2;
            switch (menu)
            {
                case (0):
                    Manager_Choose();
                    PauseGame();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    break;
                case (1):
                    StartGame();
                    G_Menu_UI.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
            }
            menu++;
        }
    }
    public void StartGame()
    {
        IsGamePaused = false;
        Time.timeScale = 1;
    //    GameObject.Find("Player").GetComponent<RigidbodyFirstPersonController>().mouseLook.lockCursor = true;

    }
    public void PauseGame()
    {
        IsGamePaused = true;
        Time.timeScale = 0f;
   //     GameObject.Find("Player").GetComponent<RigidbodyFirstPersonController>().mouseLook.lockCursor = false;
        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void VolumeChange()
    {
        GlobalVariable.SoundVolume = V_Slider.value;
        Volum.text = V_Slider.value.ToString("f2");
    }
    public void BackVolumeChange()
    {
        GlobalVariable.BGMVolume = Back_Slider.value;
        Back.text = Back_Slider.value.ToString("f2");

    }
    public void MouseSenstyChange()
    {
        Mouse.text = Mouse_Slider.value.ToString("f1");
        playerInterface.SetMouseSensitivityX(Mouse_Slider.value);
        playerInterface.SetMouseSensitivityY(Mouse_Slider.value);

    }
}
