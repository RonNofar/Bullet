using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour {

    public Item[] itemsUnlocked = new Item[5];
    // Use this for initialization
    void Start () {
        itemsUnlocked[0] = new Item(0,true,10);
        itemsUnlocked[0].printVar();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
