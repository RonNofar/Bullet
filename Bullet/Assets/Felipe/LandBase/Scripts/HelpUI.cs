using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpUI : MonoBehaviour
{
    public GameObject player;
    public GameObject enterShip;
    public GameObject enterShop;
    public GameObject arrowKey;
    // Use this for initialization
    void Start()
    {
        enterShip.SetActive(false);
        enterShop.SetActive(false);
        arrowKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (player.GetComponent<Player>().notMoving)
        {
            arrowKey.SetActive(true);

        }else
        if(arrowKey.activeInHierarchy && !player.GetComponent<Player>().notMoving)
        {
            arrowKey.SetActive(false);
            player.GetComponent<Player>().InvokeRepeating("AddValue", 1, 1);
        }
    }
}
