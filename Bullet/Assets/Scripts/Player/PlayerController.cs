using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController _instance;
        [HideInInspector]
        public PlayerController Instance {
            get { return _instance; }
            set { _instance = value; }
        }

        [Header("Movement")]
        [SerializeField]
        private float speed = 0.1f;

        [Header("Bullet")]
        [SerializeField]
        private GameObject bulletPosition;
        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private float shotDelay = 0.1f;
        [SerializeField]
        private float bulletForce = 1f;
        [SerializeField]
        private float bulletLife = 2f;

        private float shotTime = 0f;

        [Header("Maintenance Variables")]
        [SerializeField]
        private float RayRange = 1000f;

        private int _score = 0;
        [HideInInspector]
        public int score {
            get { return _score; }
            set { _score = value;
                if (_score < 0)    _score = 0;
                if (_score > 9999) _score = 9999;
            }
        }

        void Awake()
        {
            Instance = this;
        }

        public void KeyMove(Vector2 direction)
        {
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            max.x = max.x - 0.225f; // <-- Use half of player bounds.x here
            min.x = min.x + 0.225f;

            max.y = max.y - 0.285f;
            min.y = min.y + 0.285f;

            Vector2 pos = transform.position;

            pos += direction * speed * Time.deltaTime;

            pos.x = Mathf.Clamp(pos.x, min.x, max.x);
            pos.y = Mathf.Clamp(pos.y, min.y, max.y);

            transform.position = pos;
        }

        public void MouseMove()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, RayRange))
            {
                if (hit.transform.tag == "Ground")
                {
                    transform.position =
                        new Vector3(hit.point.x, hit.point.y, transform.position.z);
                }
            }
        }

        public void Shoot()
        {
            if (shotTime < Time.time)
            { // Object Pool??
                shotTime = Time.time + shotDelay;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.position = bulletPosition.transform.position;
                //bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletForce);
            }

        }
    }
}
