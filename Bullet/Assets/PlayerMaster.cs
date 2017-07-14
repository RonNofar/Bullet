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
    }

}
