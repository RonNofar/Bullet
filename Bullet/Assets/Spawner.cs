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
        [Tooltip("The delay between each wave")]
        [SerializeField]
        private float waveDelay = 5f;
        [Tooltip("The spawn delay between each enemy in a wave")]
        [SerializeField]
        private float enemyDelay = 2f;

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
            SpawnEnemies(waveArr[i]);
            StartCoroutine(Util.Func.WaitAndRunAction(
                waveDelay, 
                () => { SpawnWave(waveArr, ++i); })); 
        }

        private void SpawnEnemies(Wave wave, int i = 0)
        {
            Instantiate(wave.array[i]);
            StartCoroutine(Util.Func.WaitAndRunAction(
                enemyDelay,
                () => { SpawnEnemies(wave, ++i); }));
        }
    }
} // don't forget try catch
