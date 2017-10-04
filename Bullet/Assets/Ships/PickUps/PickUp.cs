using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet
{
    public class PickUp : MonoBehaviour
    {
        private GameObject EnAudioObject;//<--Felipe(trigger audio on shot)
        enum PType {
            NULL = 0,
            HEALTH = 1,
            MONEY = 2
        }

        [SerializeField]
        private PType pickupType;
        [Tooltip("May not be applicable")]
        [SerializeField]
        private float amount;

        void Start()
        {
            EnAudioObject = GameObject.Find("Audio3");
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "PlayerShip")
            {
                switch ((int)pickupType)
                {
                    case 0: // NULL
                        {
                            Debug.Log("PickUp: " + gameObject.name + " is NULL. Destroying...");
                            Destroy(gameObject);
                            return;
                        }
                    case 1: // HEALTH
                        {
                            col.gameObject.GetComponent<nPlayer.PlayerController>().Heal(amount);
                            EnAudioObject.GetComponent<AudioScript3>().HealthPick();//<----felipe audio HealthPickUp
                            Destroy(gameObject);
                            return;
                        }
                    case 2: // MONEY
                        {
                            col.gameObject.GetComponent<nPlayer.PlayerController>().money += (int)amount;
                            EnAudioObject.GetComponent<AudioScript3>().MoneyPick();//<----felipe audio Coindrop
                            Destroy(gameObject);
                            return;
                        }
                }
            }
        }
    }
}
