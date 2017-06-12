﻿using System.Collections;
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
                player.Move();
                player.Shoot();
            }
        }
    }
}
