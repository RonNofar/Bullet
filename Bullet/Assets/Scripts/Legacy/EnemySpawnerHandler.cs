using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Enemy
{
    public class EnemySpawnerHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private float spawnDelay = 0.5f;
        [SerializeField]
        private float waveDelay = 3f;

        [HideInInspector]
        public int wave = 0;
        private bool isWave = false;

        private new Transform transform;
        private float spawnTime = 0f;

        private void Awake()
        {
            transform = GetComponent<Transform>();
        }

        void FixedUpdate()
        {
            if (!isWave)
            {
                if (wave == 0)
                {
                    wave = 1;
                    StartCoroutine(SpawnWave(wave));
                }
            }
        }

        IEnumerator SpawnWave(int wave)
        {
            int totalEnemies = (int)Mathf.Round(10f + wave * 0.4f);
            for (int i = 0; i < totalEnemies; ++i)
            {
                GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
                enemy.GetComponent<EnemyHandler>().SetGravity( // needs clean up >.<
                    enemy.GetComponent<EnemyHandler>().orgGravity * Mathf.Ceil(wave * 0.25f));
                spawnTime = Time.time + spawnDelay;
                while (Time.time < spawnTime)
                {
                    yield return null;
                }
            }
            yield return new WaitForSeconds(waveDelay);
            StartCoroutine(SpawnWave(wave + 1));
        }
    }
}
