using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {
    private int NumberOfElementsShop=5;
    public Item[] itemsStatus = new Item[5];
    //PlayerItems.itemsUnlocked[5];

    public void Saving()
    {
        //Clear List

        //ADD all wepon from the wepon shop list
        for (int i=0;i< NumberOfElementsShop;i++) {

        }
    }
}
