using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Player
{
    public class PlayerUserController : MonoBehaviour
    {

        [SerializeField]
        private PlayerController player;

        private new Transform transform;

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        void Update()
        {
            if (Input.GetMouseButton(0) || Input.GetButton("Fire1"))
            {
                player.Shoot();
            }
            if (GameMaster.Instance.isMouseMovement)
            {
                player.MouseMove();
            }
            else
            {
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");

                Vector2 direction = new Vector2(x, y).normalized;

                player.KeyMove(direction);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Debug.Log("down");
                player.isStamina = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("up");
                player.isStamina = false;
            }
        }
    }
}
