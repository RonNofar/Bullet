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

        [Header("Player Stats (Pre-Load)")]
        [SerializeField]
        private float baseSpeed = 5f;
        [SerializeField]
        private float baseBulletDamage;
        [SerializeField]
        private float baseBulletProjectileSpeed;
        [SerializeField]
        private float baseBulletFireRate;
        [SerializeField]
        private float baseHealth;
        [SerializeField]
        private float baseStamina;

        [Header("Player Stats (Post-Load)")]
        [SerializeField]
        private float speed;
        [SerializeField]
        private float bulletDamage;
        [SerializeField]
        private float bulletProjectileSpeed;
        [SerializeField]
        private float bulletFireRate;
        [SerializeField]
        private float health;
        [SerializeField]
        private float stamina;

        [Header("Bullet")]
        public int bulletLevel = 1;
        [SerializeField]
        private GameObject[] bulletPositionL1;
        [SerializeField]
        private GameObject[] bulletPositionL2;
        [SerializeField]
        private GameObject bulletPrefab;
        [SerializeField]
        private float shotDelay = 0.1f;
        [SerializeField]
        private float bulletForce = 1f;
        [SerializeField]
        private float bulletLife = 2f;

        private float shotTime = 0f;

        [Header("Stamina")]
        [SerializeField]
        private float staminaScaler = 2f;
        private float staminaRatio = 1f;
        [HideInInspector]
        public bool isStamina = false;

        [Header("Explosion")]
        [SerializeField]
        private GameObject explosionPrefab;

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
            score = (int)Bullet.PlayerMaster.Instance.Money;
            LoadItems(PlayerMaster.Instance.itemsUnlocked);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if((col.tag == "EnemyShip") || (col.tag == "EnemyBullet"))
            {
                PlayExplosion();
                score -= Random.Range(100, 500);
            }
        }

        void PlayExplosion()
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
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

            pos += direction * (isStamina ? speed * staminaScaler : speed) * Time.deltaTime;

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

                if (bulletLevel == 1)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet.transform.position = bulletPositionL1[0].transform.position;
                }
                else if (bulletLevel ==2)
                {
                    GameObject bullet1 = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet1.transform.position = bulletPositionL2[0].transform.position;
                    GameObject bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation);
                    bullet2.transform.position = bulletPositionL2[1].transform.position;
                }
                //bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bulletForce);
            }

        }

        public GameObject[] GetBulletPositionFromLevel(int level)
        {
            switch (level)
            {
                case 0:
                    Debug.Log("ERROR: level must be greater than 0.");
                    return null;
                case 1:
                    return bulletPositionL1;
                case 2:
                    return bulletPositionL2;
            }
            Debug.Log("ERROR: invalid level.");
            return null;
        }

        private void LoadItems(Item[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
            {
                switch(i)
                {
                    case 0: // Base Speed
                        speed = baseSpeed + (arr[i].GetID() * 0.1f); // FOR 0.1f put algorithm later
                        break;
                    case 1: // Bullet Damage
                        bulletDamage = baseBulletDamage + (arr[i].GetID() * 0.1f); // FOR 0.1f put algorithm later
                        break;
                    case 2: // Bullet Speed
                        bulletProjectileSpeed = baseBulletProjectileSpeed + (arr[i].GetID() * 0.1f); // FOR 0.1f put algorithm later
                        bulletFireRate        = baseBulletFireRate + (arr[i].GetID() * 0.1f); // FOR 0.1f put algorithm later
                        break;
                    case 3: // Health
                        health = baseHealth + (arr[i].GetID() * 0.1f);
                        break;
                    case 4: // Stamina
                        stamina = baseStamina + (arr[i].GetID() * 0.1f);
                        break;
                }
            }
        }
    }
}
