using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoBehaviour {

    public GameObject Shop;

    public float playerMoneyNumber;
    public Item[] itemsUnlocked = new Item[5];

    // Use this for initialization
    void Start ()
    { 
        playerMoneyNumber=10000;

        itemsUnlocked[0] = new Item(0,true,7);
        itemsUnlocked[0].printVar();
        itemsUnlocked[1] = new Item(1, true, 2);
        itemsUnlocked[2] = new Item(2, true, 5);
        itemsUnlocked[3] = new Item(3, true, 4);
        itemsUnlocked[4] = new Item(4, true, 10);
    }
	
	// Update is called once per frame
	void Update () {
        //.......................................

        //Shop.GetComponent<Canvas>(PlayerMoneyText).text = PlayerMoneyNumber.ToString(); ;

       //PlayerMoneyText.text = PlayerMoneyNumber.ToString();
        //.......................................

    }
}
