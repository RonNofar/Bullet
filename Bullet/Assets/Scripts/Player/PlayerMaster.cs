using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Bullet
{
    public class PlayerMaster : MonoBehaviour
    {
        public Item[] itemsUnlocked = new Item[5];
        public float Money;

        static public PlayerMaster Instance { get { return _instance; } }
        static protected PlayerMaster _instance;

        public bool isMouseMovement = false;

        public void Awake()
        {
            if (_instance != null)
            {
                Debug.LogWarning("PlayerMaster is already in play. Deleting new!", gameObject);
                Destroy(gameObject);
            }
            else
            { _instance = this; }
        }

        void Start()
        {
            /*
            Money = 10000;

            itemsUnlocked[0] = new Item(0, true, 7);
            itemsUnlocked[0].printVar();
            itemsUnlocked[1] = new Item(1, true, 2);
            itemsUnlocked[2] = new Item(2, true, 5);
            itemsUnlocked[3] = new Item(3, true, 4);
            itemsUnlocked[4] = new Item(4, true, 10);
            */
        }
    }
}

