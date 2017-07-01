using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Enemy
{
    public class EnemyGun : MonoBehaviour
    {

        [SerializeField]
        private GameObject EnemyBulletPrefab;

        public float delay = 1f;

        public int repeats = 0;
        public float repeatDelay = 1f;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(Util.Func.WaitAndRunAction(delay, () => { FireEnemyBullet(repeats, repeatDelay); }));
            //Invoke("FireEnemyBullet", 1f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FireEnemyBullet(int repeats = 0, float repeatDelay = 0, int i = 0)
        {
            GameObject playerShip = GameObject.Find("Player");

            if (playerShip != null)
            {
                GameObject bullet = (GameObject)Instantiate(EnemyBulletPrefab);
                bullet.transform.position = transform.position;
                Vector2 direction = playerShip.transform.position - bullet.transform.position;

                bullet.GetComponent<EnemyBullet>().SetDirection(direction);
            }
            else
                Debug.Log("playerShip is null");
            
            if (i < repeats)
            {
                StartCoroutine(Util.Func.WaitAndRunAction(repeatDelay, () => { FireEnemyBullet(repeats, repeatDelay, ++i); }));
            }
        }
    }
}
