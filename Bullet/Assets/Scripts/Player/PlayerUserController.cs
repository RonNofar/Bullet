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
            if (Input.GetMouseButton(0))
            {
                if (GameMaster.Instance.isMouseMovement) player.MouseMove();
                player.Shoot();
            }
            if (!GameMaster.Instance.isMouseMovement)
            {
                float x = Input.GetAxisRaw("Horizontal");
                float y = Input.GetAxisRaw("Vertical");

                Vector2 direction = new Vector2(x, y).normalized;

                player.KeyMove(direction);
            }
        }
    }
}
