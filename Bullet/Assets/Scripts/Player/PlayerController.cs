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

        public void Move()
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
                bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletForce);
            }

        }
    }
}
