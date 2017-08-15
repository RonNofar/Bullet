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
                Instantiate(wave.array[i]);
                StartCoroutine(Util.Func.WaitAndRunAction(
                    enemyDelay,
                    () => { SpawnEnemies(wave, ++i); }));
            }
            catch (System.IndexOutOfRangeException e)
            {
                Debug.Log("wave : " + wave.gameObject.name + " is done");
            }
        }
    }
} // don't forget try catch
