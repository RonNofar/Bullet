using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bullet
{
    public class WinEvent : MonoBehaviour
    {
        private Player.PlayerController player;

        private void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player.PlayerController>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if (player.transform.localPosition.y > max.y)
            {
                SceneManager.LoadScene("Land_Control"); // change to # eventually
            }
        }
    }
}
