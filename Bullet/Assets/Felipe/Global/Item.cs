using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Item{

    private bool itemUnlocked;
    private int itemLevel;
    private int itemID;
    private string itemName;
    private string itemDescription;
    private float itemPriceMultiplayer;
    static int TotalNumberOfItems=5;


    public Item(int id,bool unlocked,int level) {
        this.itemUnlocked = unlocked;
        this.itemLevel = level;
        this.itemID = id;
        for (int i = 0;i< TotalNumberOfItems; i++)
        {
            if (id == 0)
            {
                this.itemName = "Energy Cristal";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPriceMultiplayer = 1.4f;
            }else if (id == 1)
            {
                this.itemName = "Energy Cristal";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPriceMultiplayer = 1.4f;
            }
            else if (id == 2)
            {
                this.itemName = "Energy Cristal";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPriceMultiplayer = 1.4f;
            }
            else if (id == 3)
            {
                this.itemName = "Energy Cristal";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPriceMultiplayer = 1.4f;
            }
            else if (id == 4)
            {
                this.itemName = "Energy Cristal";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPriceMultiplayer = 1.4f;
            }
            else if (id == 5)
            {
                this.itemName = "Energy Cristal";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPriceMultiplayer = 1.4f;
            }

        }
    }
    public void printVar()
    {
        Debug.Log(itemID);
        Debug.Log(itemName);
        Debug.Log(itemDescription);
        Debug.Log(itemUnlocked);
        Debug.Log(itemLevel);
    }

    public void Unlock()
    {
        itemUnlocked = true;
    }
    public void Lock()
    {
        itemUnlocked = false;
    }


}
