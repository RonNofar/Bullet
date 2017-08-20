using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInGame : MonoBehaviour
{
    public Transform buttonSave;
    public Transform buttonOption;

    public GameObject AbortMissionButton;
    public GameObject SkipButton;
    // Use this for initialization
    void Start()
    {
        string sceneName;
        sceneName=SceneManager.GetActiveScene().name;
        print(sceneName);
        buttonSave.GetComponent<Button>().interactable = false;
        buttonOption.GetComponent<Button>().interactable = false;

        if (sceneName == "Land_Control")
        {
            // Do something...
            SkipButton.SetActive(false);
            AbortMissionButton.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "_main")
        {
            SkipButton.SetActive(false);
            // Do something...
        }
        else if (SceneManager.GetActiveScene().name == "101")
        {
            AbortMissionButton.SetActive(false);
            SkipButton.SetActive(true);
            // Do something...
        }
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        //Debug.Log(SceneManager.GetActiveScene().name);
    }
    public void ButtonExitF()
    {
        Application.Quit();
    }
    public void ButtonContinue()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        if(SceneManager.GetActiveScene().name != "101")
        Cursor.visible = false;
    }
    public void ButtonSkipIntro()
    {
        GameObject.Find("MenuUI").GetComponent<MenuController>().myState = MenuController.States.initial_10;
        this.gameObject.SetActive(false);
    }
    public void ButtonAbortMission()
    {
        SceneManager.LoadScene("Land_Control");
    }
}
