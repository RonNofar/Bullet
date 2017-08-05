using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Enemy
{
    public class EnemyGun : MonoBehaviour
    {

        [SerializeField]
        private GameObject EnemyBulletPrefab;

        [SerializeField]
        private bool isAtPlayer = true;
        [SerializeField]
        private bool isSpreadShot = false;

        public float delay = 1f; // Delay for first shot

        public int repeats = 0; // Extra bullets being fired
        public float repeatDelay = 1f; // Time between extra bullet firing

        public int rounds = 0; // Amount of extra rounds to fire (a round will go through repeates again)
        public float roundDelay = 1f; // Amount of time between rounds

        public float damage = 10f;

        // Use this for initialization
        void Start()
        {
            roundDelay += repeats * repeatDelay;
            StartCoroutine(Util.Func.WaitAndRunAction(delay, () => { FireRound(rounds, roundDelay); }));//FireEnemyBullet(repeats, repeatDelay); }));
            //Invoke("FireEnemyBullet", 1f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void FireRound(int rounds = 0, float roundDelay = 0f, int i = 0)
        {
            FireEnemyBullet(repeats, repeatDelay);
            if (i < rounds)
            {
                StartCoroutine(Util.Func.WaitAndRunAction(roundDelay, () => { FireRound(rounds, roundDelay, ++i); }));
            }
        }

        void FireEnemyBullet(int repeats = 0, float repeatDelay = 0, int i = 0)
        {
            SpawnBullet();
            if (i < repeats)
            {
                StartCoroutine(Util.Func.WaitAndRunAction(repeatDelay, () => { FireEnemyBullet(repeats, repeatDelay, ++i); }));
            }
        }

        void SpawnBullet() {
            bool b = true;
            for (int i = 0; i < 1; ++i)
            {
                GameObject playerShip = GameObject.Find("Player");

                if (playerShip != null)
                {
                    GameObject bullet = (GameObject)Instantiate(EnemyBulletPrefab);
                    bullet.transform.position = transform.position;
                    Vector2 direction;
                    if (isAtPlayer) direction = playerShip.transform.position - bullet.transform.position;
                    else if (isSpreadShot)
                    {
                        if (b) { direction = new Vector2(-1, -1); --i; b = false; }
                        else direction = new Vector2(1, -1);
                    }
                    else direction = new Vector2(0, -1);

                    EnemyBullet eb = bullet.GetComponent<EnemyBullet>();
                    eb.SetDirection(direction);
                    eb.damage = damage;
                }
                else
                    Debug.Log("playerShip is null");
            }
        }
    }
}
