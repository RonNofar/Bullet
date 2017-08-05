using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "Land_Control")
        {
            // Do something...

            for (int i = 0; i < Item.TotalNumberOfItems; i++)
            {
               // savedItems[i] = Bullet.PlayerMaster.Instance.itemsUnlocked[i];
            }
            //savedMoney = Bullet.PlayerMaster.Instance.Money;

        }
        else if (SceneManager.GetActiveScene().name == "_main")
        {
            // Do something...
        }

    }
}
