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
        [SerializeField]
        private float lifeTime = 3f;

        private new Transform transform;

        void Awake()
        {
            transform = GetComponent<Transform>();
            StartCoroutine(Util.Func.WaitAndRunAction(lifeTime, () => { Destroy(gameObject); }));
        }

        void Update()
        {
            if (transform.position.y >= maxY) Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Enemy")
            {
                Player.PlayerController.Instance.score += Random.Range(50, 250);
                Destroy(gameObject);
            }
        }
    }
}
