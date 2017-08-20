using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.nPlayer
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        #region Singleton
        protected static PlayerController _instance;
        [HideInInspector]
        static public PlayerController Instance {
            get { return _instance; }
            set { _instance = value; }
        }
        #endregion

        #region Gameplay
        [Tooltip("DO NOT TOUCH")]
        public float health;

        [Header("Player Stats (Pre-Load)")]
        [SerializeField]
        private float baseSpeed = 5f;
        [SerializeField]
        private float maxSpeed = 10f;
        [SerializeField]
        private float baseBulletDamage = 25f;
        [SerializeField]
        private float maxBulletDamage = 125f;
        [SerializeField]
        private float baseBulletProjectileSpeed = 8f;
        [SerializeField]
        private float maxBulletProjectileSpeed = 16f;
        [SerializeField]
        private float baseBulletFireRate = 0.2f;
        [SerializeField]
        private float minBulletFireRate = 0.1f;
        [SerializeField]
        private float baseHealth = 100f;
        [SerializeField]
        private float maxHealth = 200f;
        [SerializeField]
        private float baseStamina = 100f;
        [SerializeField]
        private float maxStamina = 200f;

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
        private float totalHealth;
        [SerializeField]
        private float totalStamina;
        #endregion

        #region  Maintenance
        [Header("Bullet")]
        public int bulletLevel = 1;
        [SerializeField]
        private GameObject[] bulletPositionL1;
        [SerializeField]
        private GameObject[] bulletPositionL2;
        [SerializeField]
        private GameObject bulletPrefab;

        private float shotTime = 0f;

        [Header("Stamina")]
        [SerializeField]
        private float staminaScaler = 2f;
        [SerializeField]
        private float depleteRate = 1f;
        private float stamina;
        
        [HideInInspector]
        public bool isStamina = false;

        [Header("Explosion")]
        [SerializeField]
        private GameObject explosionPrefab;

        [Header("Death")]
        [SerializeField]
        private float deathDelay;
        [SerializeField]
        private int explosionAmount;
        [SerializeField]
        private float[] delayRange;

        [Header("Maintenance Variables")]
        [SerializeField]
        private float RayRange = 1000f;
        [SerializeField]
        private SpriteRenderer sr;
        [SerializeField]
        private GameObject playerCanvas;

        private Collider2D col;
        private Vector2 colExtent;
        private Vector2 srExtent;

        [HideInInspector]
        public bool isDead = false;

        private int _score = 0;
        [HideInInspector]
        public int score {
            get { return _score; }
            set { _score = value;
                if (_score < 0)    _score = 0;
                if (_score > 9999) _score = 9999;
            }
        }

        private int _money = 0;
        [HideInInspector]
        public int money {
            get { return _money; }
            set { _money = value;
                if (_money > 1000000) _money = 1000000;
                if (_money < 0)       _money = 0;
            }
        }
        #endregion
        #endregion

        #region Functions
        #region Unity
        void Awake()
        {
            _instance = this;

            try
            {
                //score = (int)Bullet.PlayerMaster.Instance.Money;
                LoadItems(PlayerMaster.Instance.itemsUnlocked);
            }
            catch
            {
                Debug.Log("ERROR: No PlayerMaster found.");
            }

            stamina = totalStamina;
            health = totalHealth;

            sr.enabled = true;
            playerCanvas.SetActive(true);

            col = GetComponent<Collider2D>();
            colExtent = col.bounds.extents;
            srExtent = sr.bounds.extents;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.tag == "EnemyShip")
            {
                PlayExplosion();
                Damage(col.gameObject.GetComponent<Enemy.EnemyControl>().damage); // change this
                score -= Random.Range(100, 500);
                //GameObject.Find("PlayerUI").GetComponent<PlayerUI>().hit=true;
            }
            else if (col.tag == "EnemyBullet")
            {
                Damage(col.gameObject.GetComponent<Enemy.EnemyBullet>().damage);
            }
        }
        #endregion

        #region Movement
        public void KeyMove(Vector2 direction)
        {
            if (!isDead)
            {
                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

                max.x = max.x - srExtent.x; // <-- Use half of player bounds.x here
                min.x = min.x + srExtent.x;

                max.y = max.y + 2 * srExtent.y;
                min.y = min.y + srExtent.y;

                Vector2 pos = transform.position;

                float temp_speed = speed;
                if (isStamina && stamina != 0f)
                {
                    temp_speed = speed * staminaScaler;

                    stamina -= depleteRate;
                    if (stamina < 0) stamina = 0;
                }
                else if (!isStamina && stamina < totalStamina)
                {
                    stamina += depleteRate;
                    if (stamina > totalStamina) stamina = totalStamina;
                }

                pos += direction * temp_speed * Time.deltaTime;

                pos.x = Mathf.Clamp(pos.x, min.x, max.x);
                pos.y = Mathf.Clamp(pos.y, min.y, max.y);

                transform.position = pos;
            }
        }

        public void MouseMove()
        {
            if (!isDead)
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
        }
        #endregion

        #region Gameplay
        public void Shoot()
        {
            if (!isDead)
            {
                if (shotTime < Time.time)
                { // Object Pool??
                    shotTime = Time.time + bulletFireRate;

                    if (bulletLevel == 1)
                    {
                        SpawnBullet(new Transform[] { bulletPositionL1[0].transform });
                    }
                    else if (bulletLevel == 2)
                    {
                        SpawnBullet(new Transform[] {
                        bulletPositionL2[0].transform,
                        bulletPositionL2[1].transform });
                    }
                }
            }
        }

        private void SpawnBullet(Transform[] trans)
        {
            for (int i = 0; i < trans.Length; ++i)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.position = trans[i].position;
                bullet.transform.rotation = trans[i].rotation;
                bullet.GetComponent<PlayerBullet>().speed = bulletProjectileSpeed;
            }
        }

        public void Damage(float amount)
        {
            health -= amount;
            if (health < 0)
            {
                health = 0;
                Death();
            }
        }

        void Death()
        {
            Debug.Log("Death");
            //Bullet.PlayerMaster.Instance.Money = score;
            StartCoroutine(Util.Func.WaitAndRunAction(0.5f, 
                () => { sr.color = 
                    new Vector4(sr.color.r, sr.color.g, sr.color.b, 0);
                    playerCanvas.SetActive(false); }));
            PlayExplosion();
            RepeatingExplosion(explosionAmount, delayRange);
            if (!isDead)
            {
                StartCoroutine(Util.Func.WaitAndRunAction(
                    deathDelay, () => { UI.GUIManager.Instance.ActivateGameOver(); }));
                isDead = true;
            }
        }

        public void Heal(float amount)
        {
            health += amount;
            if (health > maxHealth) health = maxHealth;
        }
        #endregion

        #region Animation / FX
        void PlayExplosion()
        {
            GameObject explosion = Instantiate(explosionPrefab);
            explosion.transform.position = transform.position;
        }

        void RepeatingExplosion(int amount, float[] delayRange)
        {
            if (amount != 0)
            {
                StartCoroutine(Util.Func.WaitAndRunAction(
                    Random.Range(delayRange[0], delayRange[1]), 
                    () => { RepeatingExplosion(--amount, delayRange); }));
            }
        }
        #endregion

        #region Utility
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
                        speed = baseSpeed + (arr[i].GetLevel() * (maxSpeed - baseSpeed) / 10); // FOR 0.1f put algorithm later
                        break;
                    case 1: // Bullet Damage
                        bulletDamage = baseBulletDamage + (arr[i].GetLevel() * (maxBulletDamage - baseBulletDamage) / 10); // FOR 0.1f put algorithm later
                        break;
                    case 2: // Bullet Speed
                        bulletProjectileSpeed = baseBulletProjectileSpeed + 
                            (arr[i].GetLevel() * (maxBulletProjectileSpeed - baseBulletProjectileSpeed) / 10); // FOR 0.1f put algorithm later
                        bulletFireRate        = baseBulletFireRate - 
                            (arr[i].GetLevel() * (baseBulletFireRate - minBulletFireRate) / 10); // FOR 0.1f put algorithm later
                        break;
                    case 3: // Health
                        totalHealth = baseHealth + (arr[i].GetLevel() * (maxHealth - baseHealth) / 10);
                        break;
                    case 4: // Stamina
                        totalStamina = baseStamina + (arr[i].GetLevel() * (maxHealth- baseHealth) / 10);
                        break;
                }
            }
        }

        public float GetDamageAmount()
        {
            return bulletDamage; 
        }

        public float GetHealthRatio()
        {
            try { return health / totalHealth; }
            catch { return 0f; }
        }

        public float GetStaminaRatio()
        {
            try { return stamina / totalStamina; }
            catch { return 0f; }
        }
        #endregion
        #endregion
    }
}
