using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Enemy
{
    public class EnemyControl : MonoBehaviour
    {

        [SerializeField]
        private GameObject explosionPrefab;
        float speed;
        [SerializeField]
        private float health = 100f;
        public float damage = 20f;

        // Use this for initialization
        void Start()
        {
            speed = 2f;
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 position = transform.position;

            position = new Vector2(position.x, position.y - speed * Time.deltaTime);

            transform.position = position;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            if (transform.position.y < min.y)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if ((col.tag == "PlayerShip") || (col.tag == "PlayerBullet"))
            {
                Damage(Player.PlayerController.Instance.GetDamageAmount());
            }
        }

        void Damage(float amount)
        {
            health -= amount;
            if (health <= 0) Death();
        }

        void Death()
        {
            PlayExplosion();
            Destroy(gameObject);
        }

        void PlayExplosion()
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
        }
    }
}
