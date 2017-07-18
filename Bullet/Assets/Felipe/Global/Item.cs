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
    private float itemPricemultiplier;
    public static int TotalNumberOfItems=5;

    public Item(int id)
    {
        this.itemUnlocked = false;
        this.itemLevel = 0;
        this.itemID = id;
        for (int i = 0; i < TotalNumberOfItems; i++)
        {
            if (id == 0)
            {
                this.itemName = "Energy Cristal";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPricemultiplier = 1.2f;
            }
            else if (id == 1)
            {
                this.itemName = "Laser Canon";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPricemultiplier = 1.5f;
            }
            else if (id == 2)
            {
                this.itemName = "Power3";
                this.itemDescription = "Power 3 Description";
                this.itemPricemultiplier = 1.7f;
            }
            else if (id == 3)
            {
                this.itemName = "Power 4";
                this.itemDescription = "Power 4 Description";
                this.itemPricemultiplier = 1.4f;
            }
            else if (id == 4)
            {
                this.itemName = "Power 5";
                this.itemDescription = "Power 5 Description";
                this.itemPricemultiplier = 2f;
            }
        }
    }
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
                this.itemPricemultiplier = 1.2f;
            }else if (id == 1)
            {
                this.itemName = "Laser Canon";
                this.itemDescription = "Empowers the basic wepon with a more powerfull attack";
                this.itemPricemultiplier = 1.5f;
            }
            else if (id == 2)
            {
                this.itemName = "Power3";
                this.itemDescription = "Power 3 Description";
                this.itemPricemultiplier = 1.7f;
            }
            else if (id == 3)
            {
                this.itemName = "Power 4";
                this.itemDescription = "Power 4 Description";
                this.itemPricemultiplier = 1.4f;
            }
            else if (id == 4)
            {
                this.itemName = "Power 4";
                this.itemDescription = "Power 4 Description";
                this.itemPricemultiplier = 1.4f;
            }
            else if (id == 5)
            {
                this.itemName = "Power 4";
                this.itemDescription = "Power 4 Description";
                this.itemPricemultiplier = 2f;
            }

        }
    }

    public void printVar()
    {
        Debug.Log("Item ID "+ itemID);
        Debug.Log("Name "+ itemName);
        Debug.Log("Description " + itemDescription);
        Debug.Log("Unlocked " + itemUnlocked);
        Debug.Log("Level " + itemLevel);
    }
    public void printLevel()
    {
        Debug.Log("Level " + itemLevel);
    }

    public void Unlock()
    {
        itemUnlocked = true;
    }
    public void Lock()
    {
        itemUnlocked = false;
    }

    public void SetLevel(int level)
    {
        this.itemLevel = level;
    }

    public float GetMultiplier()
    {

        return this.itemPricemultiplier;
    }
    public int GetID()
    {

        return this.itemID;
    }
    public int GetLevel()
    {

        return this.itemLevel;
    }
}
