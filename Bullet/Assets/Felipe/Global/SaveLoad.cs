using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public GameObject Player;
    //private PlayerItems ItemsOnPlayer;

    public Item[] savedItems = new Item[5];
    public float savedMoney;
    //PlayerItems.itemsUnlocked[5];
    void start()
    {
    }
    void Update()
    {
        //Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "Land_Control")
        {
            // Do something...
        
        for (int i = 0; i < Item.TotalNumberOfItems; i++)
        {
            savedItems[i] = Bullet.PlayerMaster.Instance.itemsUnlocked[i];
        }
        savedMoney = Bullet.PlayerMaster.Instance.Money;

        }
        else if (SceneManager.GetActiveScene().name == "_main")
        {
            // Do something...
        }
    }

    public void Saving()
    {
        //Clear List

        //ADD all wepon from the wepon shop list

    }
}
