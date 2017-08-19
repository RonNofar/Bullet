using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet {
    public class Spawner : MonoBehaviour {

        [Header("Waves Array")]
        public Wave[] waves;

        [Header("Variables")]
        [Tooltip("The delay before the very first spawn")]
        [SerializeField]
        private float initialDelay = 10f;
        [Tooltip("The delay between each wave (time from last spawn of wave)")]
        [SerializeField]
        private float waveDelay = 5f;
        [Tooltip("The spawn delay between each enemy in a wave")]
        [SerializeField]
        private float enemyDelay = 2f;

        private float totalWaveDelay;

        private void Start()
        {
            StartCoroutine(Util.Func.WaitAndRunAction(
                initialDelay, 
                () => { SpawnWave(waves); }));
        }

        /// <summary>
        /// A recursive function which spawns a wave and 
        /// when its done spawns the next. If there is no 
        /// next, triggers win game
        /// </summary>
        /// <param name="waveArr"></param>
        private void SpawnWave(Wave[] waveArr, int i = 0)
        {
            try
            {
                Debug.Log("Spawning wave #" + (i + 1));
                totalWaveDelay = waveDelay + (enemyDelay * waveArr[i].array.Length); // gets total length of wave and adds waveDelay
                Debug.Log("totalWaveDelay = " + totalWaveDelay);

                SpawnEnemies(waveArr[i]);
                StartCoroutine(Util.Func.WaitAndRunAction(
                    totalWaveDelay,
                    () => { SpawnWave(waveArr, ++i); }));
            }
            catch (System.IndexOutOfRangeException e)
            {
                Debug.Log("Done spawning waves.");
            }
        }

        private void SpawnEnemies(Wave wave, int i = 0)
        {
            try
            {
                Debug.Log("Spawning enemy #" + (i + 1));
                SpawnEnemy(wave.array[i]);
                StartCoroutine(Util.Func.WaitAndRunAction(
                    enemyDelay,
                    () => { SpawnEnemies(wave, ++i); }));
            }
            catch (System.IndexOutOfRangeException e)
            {
                Debug.Log("wave : " + wave.gameObject.name + " is done");
            }
        }

        private void SpawnEnemy(GameObject enemyPrefab)
        {
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            GameObject enemy = (GameObject)Instantiate(enemyPrefab);
            switch (enemyPrefab.layer)
            {
                case 10: // Single Ship layer
                {
                        float tenth = (max.x - min.x) / 10;
                    enemy.transform.position = new Vector2(Random.Range(min.x + tenth, max.x - tenth), max.y); // Gotta account for bounds of enemy
                    return;
                }
                case 11: // Formation Ship layer
                {
                    float quarter = (max.x - min.x) / 4;
                    enemy.transform.position = new Vector2(min.x + Random.Range(1,3)*quarter, max.y); // 25%, 50%, 75%
                    return;
                }
                case 12: // Cluster Ship layer
                {
                    float average = (max.x + min.x) / 2;
                    enemy.transform.position = new Vector2(average, max.y);
                    return;
                }
            }
        }
    }
}
