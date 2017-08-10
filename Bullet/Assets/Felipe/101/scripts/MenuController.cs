﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //-----Audio
    public GameObject AudioControler;
    private bool requestedChange;
    //------------------------------------Storry Part
    public Text text;
    public bool ButtonPress = false;
    public GameObject ButtonNext;
    public GameObject KardashevImage;
    public GameObject ImageStorry;
    public enum States { initial_0, initial_1, initial_2, initial_3, initial_4, initial_5, initial_6, initial_7, initial_8, initial_9, initial_10};
    public States myState;
    //-------------------------------------
    //-------------------------------Main_Menu
    public Transform buttonOption;

    public GameObject ButtonNewGame;
    public GameObject ButtonOptions;
    public GameObject ButtonExit;
    public GameObject ButtonsMenuBackground;

    //-----------------------------------
    public bool audioFadeTime;
    public bool kardashevMenuMusicTime;

    //--------------------------------
    public Image imageGo;



    // Use this for initialization
    void Start()
    {
        //-----Image

        //------Audio'
        requestedChange = false;
        //------Storry
        ButtonNext.SetActive(true);
        ImageStorry.SetActive(true);
        myState = States.initial_0;
        //--------------
        //-----Main_Menu
        buttonOption.GetComponent<Button>().interactable = false;

        ButtonsMenuBackground.SetActive(false);
        KardashevImage.SetActive(false);
        ButtonNewGame.SetActive(false);
        ButtonOptions.SetActive(false);
        ButtonExit.SetActive(false);
        //----------------
    }

    // Update is called once per frame

    //button press-----------------------
    public void ButtonNextF()
    {
        ButtonPress = true;
        Invoke("NextStorryTextTime", 0.2f);
    }

    public void NextStorryTextTime()
    {
        ButtonPress = false;
    }
    //-----------------------------------

    void Update(){
        if (myState==States.initial_0) {
            state_initial_0();
        }
        else if (myState == States.initial_1){
            state_initial_1();
        }
        else if (myState == States.initial_2)
        {
            state_initial_2();
        }
        else if (myState == States.initial_3)
        {
            state_initial_3();
        }
        else if (myState == States.initial_4)
        {
            state_initial_4();
        }
        else if (myState == States.initial_5)
        {
            state_initial_5();
        }
        else if (myState == States.initial_6)
        {
            state_initial_6();
        }
        else if (myState == States.initial_7)
        {
            state_initial_7();
        }
        else if (myState == States.initial_8)
        {
            state_initial_8();
        }
        else if (myState == States.initial_9)
        {
            state_initial_9();
        }
        else if (myState == States.initial_10)
        {
            state_initial_10();
        }
    }

    void state_initial_0(){
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "In a galaxy far, far away...";
        imageGo.sprite = Resources.Load<Sprite>("KImage1and2");
        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_1;
        }
    }

    void state_initial_1()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "No wait. Someone did that already.";
        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_2;
        }
    }
    void state_initial_2()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "Earth: 30,000 A.D.";
        imageGo.sprite = Resources.Load<Sprite>("KImage3");
        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_3;
        }
    }
    void state_initial_3()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "Look. We’ve conquered the land.";
        imageGo.sprite = Resources.Load<Sprite>("KImage4");
        if (Input.GetKeyDown(KeyCode.Space)|| ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_4;
        }
    }
    void state_initial_4()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "Conquered the skies.";
        imageGo.sprite = Resources.Load<Sprite>("KImage5");

        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_5;
        }
    }
    void state_initial_5()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "And, well, the sea is scary.";
        imageGo.sprite = Resources.Load<Sprite>("KImage6");

        audioFadeTime = true;
        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_6;
        }
    }
    void state_initial_6()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "It’s time for us to move on. Onto the next frontier.";
        imageGo.sprite = Resources.Load<Sprite>("KImage7");
        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_7;
        }
    }
    void state_initial_7()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "The final frontier. Space.";
        imageGo.sprite = Resources.Load<Sprite>("KImage8");
        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_9;
        }
    }
    void state_initial_8()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "state_8";
        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            ButtonPress = false;
            myState = States.initial_9;
        }
    }
    void state_initial_9()
    {
        text.alignment = TextAnchor.MiddleCenter;
        text.text = "It’s time to grow our civilization. Growth on the Kardashev scale.";
        imageGo.sprite = Resources.Load<Sprite>("KImage9");
        if (Input.GetKeyDown(KeyCode.Space) || ButtonPress)
        {
            myState = States.initial_10;
        }
    }
    public void state_initial_10()
    {
        ImageStorry.SetActive(false);
        ButtonNext.SetActive(false);
        ButtonPress = false;
        if (!requestedChange)
        {
            requestedChange = true;
            AudioControler.GetComponent<AudioScript>().timeToChangeTrack = true;
        }

        text.alignment = TextAnchor.MiddleCenter;
        //MENU
        kardashevMenuMusicTime = true;
        text.text = "";
        ButtonsMenuBackground.SetActive(true);
        KardashevImage.SetActive(true);
        ButtonNewGame.SetActive(true);
        ButtonOptions.SetActive(true);
        ButtonExit.SetActive(true);
    }

    public void ButtonExitF() {
        Application.Quit();
    }
    public void ButtonStartF()
    {
        SceneManager.LoadScene("Land_Control");

    }
    public void ButtonNewGameF()
    {
        for (int i = 0; i < Item.TotalNumberOfItems; i++)
        {
            Bullet.PlayerMaster.Instance.itemsUnlocked[i] = new Item(i);
        }
        Bullet.PlayerMaster.Instance.MaxLifes = 10;
        Bullet.PlayerMaster.Instance.Money=100000;
        Bullet.PlayerMaster.Instance.Lifes = 6;
        SceneManager.LoadScene("Land_Control");
    }

    public void ClickToChange1()
    {
        imageGo.sprite = Resources.Load<Sprite>("KImage1and2");
        imageGo.sprite = Resources.Load<Sprite>("KImage3");
        imageGo.sprite = Resources.Load<Sprite>("KImage4");
        imageGo.sprite = Resources.Load<Sprite>("KImage5");
        imageGo.sprite = Resources.Load<Sprite>("KImage6");
        imageGo.sprite = Resources.Load<Sprite>("KImage7");
        imageGo.sprite = Resources.Load<Sprite>("KImage8");
        imageGo.sprite = Resources.Load<Sprite>("KImage9");
    }
}