using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Player
{
    public class PlayerBullet : MonoBehaviour
    {
        
        new Transform transform;

        public float speed = 8f;
        [SerializeField]
        private GameObject explosionPrefab;

        // Use this for initialization
        void Start()
        {
            //speed = 16f;
            transform = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 position = transform.localPosition;

            position = new Vector2(position.x, position.y + speed * Time.deltaTime);

            transform.localPosition = position;

            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if (transform.position.y > max.y)
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag == "EnemyShip")
            {
                GameObject explosion = Instantiate(explosionPrefab);
                explosion.transform.position = transform.position;
                Destroy(gameObject);
                Player.PlayerController.Instance.score += Random.Range(100, 500);
            }
        }
    }
}
