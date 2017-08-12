using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    public GameObject player;
    public GameObject enterShip;
    public GameObject enterShop;
    public GameObject enterTutorial;
    public GameObject arrowKey;
    public GameObject SpaceBar;

    bool Helptimer;
    public bool notMoving;
    public bool notJumping;
    int numberOfTimesMovePress;
    int numberOfTimesJumpPress;
    int numberOfYielTimes;
    public int timeyield;
    public int SetHelpTime;
    // Use this for initialization
    void Start()
    {
        notMoving = false;
        notJumping=false;
        Helptimer = false;
        //you can cancel invokerepeating using CancelInvoke(), but if you have many of them in one script, it will stop all of them
        InvokeRepeating("AddValue", 1, 1);
        timeyield = 0;
        numberOfYielTimes = 0;
        numberOfTimesJumpPress = 0;
        numberOfTimesMovePress = 0;

        SpaceBar.SetActive(false);
        enterTutorial.SetActive(false);
        enterShip.SetActive(false);
        enterShop.SetActive(false);
        arrowKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (timeyield >= SetHelpTime)
        {
            Helptimer = true;
        }
        if (Helptimer)
        {
            Helptimer = false;
            numberOfYielTimes++;
            if (numberOfTimesJumpPress == 0 && numberOfYielTimes > 3)
            {
                notJumping = true;
                numberOfTimesJumpPress = 0;
                numberOfYielTimes = 0;
            }
            else
            {
                numberOfTimesJumpPress = 0;
            }

            CancelInvoke();
            //Debug.Log("now we will see if you need help /" + timeyield + " : " + numberOfTimesMovePress);
            if (numberOfTimesMovePress == 0)
            {
                //Invoke Help UI
                // print("user wasd arrow keys");
                notMoving = true;
            }
            else
            {
                // print("Time : " + timeyield + " numberOfKeyP: " + numberOfTimesMovePress);
                InvokeRepeating("AddValue", 1, 1);
            }
            numberOfTimesMovePress = 0;
            timeyield = 0;


        }
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            numberOfTimesMovePress++;
            notMoving = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            numberOfTimesMovePress++;
            notJumping = false;
        }

        //------------------------
        if (player.GetComponent<Player>().stepingOnShop)
        {
            enterShop.SetActive(true);
        }
        else
            enterShop.SetActive(false);
        if (player.GetComponent<Player>().ReadyToLeave)
        {
            enterShip.SetActive(true);
        }
        else
            enterShip.SetActive(false);
        if (player.GetComponent<Player>().stepingOnTutorialDoor)
        {
            enterTutorial.SetActive(true);
        }
        else
            enterTutorial.SetActive(false);
        if (notMoving)
        {
            arrowKey.SetActive(true);
            SpaceBar.SetActive(true);

        } else
        if ((arrowKey.activeInHierarchy || SpaceBar.activeInHierarchy) && !notMoving)
        {
            if (arrowKey.activeInHierarchy)
            {
                arrowKey.SetActive(false);
            }
            if (SpaceBar.activeInHierarchy)
            {
                SpaceBar.SetActive(false);
            }
            
            InvokeRepeating("AddValue", 1, 1);
        }
        if (notJumping)
        {
            SpaceBar.SetActive(true);

        }
        else
        if ( SpaceBar.activeInHierarchy && !notJumping)
        {
            SpaceBar.SetActive(false);
        }

    }
    void AddValue()
    {
        timeyield++;
    }
}
