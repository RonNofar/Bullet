using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Items
{
    public class BulletHandler : MonoBehaviour
    {
        private Player.PlayerController player;
    
        [SerializeField]
        private float maxY = 5f;

        private new Transform transform;

        void Awake()
        {
            transform = GetComponent<Transform>();
        }

        void Update()
        {
            if (transform.position.y >= maxY) Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Enemy")
            {
                Player.PlayerController._instance.score += Random.Range(50, 250);
                Destroy(gameObject);
            }
        }
    }
}
