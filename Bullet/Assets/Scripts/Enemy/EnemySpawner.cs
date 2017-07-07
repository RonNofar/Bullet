using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private GameObject enemyPrefab;

    float maxSpawnRateInSeconds = 5f;

	// Use this for initialization
	void Start () {
        Invoke("SpawnEnemy", maxSpawnRateInSeconds); // Every instance of this should be replaced with Util.Tools.RunActionAfterSecs

        InvokeRepeating("IncreaseSpawnRate", 0f, 30f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnEnemy()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        GameObject enemy = (GameObject)Instantiate(enemyPrefab);
        enemy.transform.position = new Vector2(Random.Range(min.x, max.x), max.y); // Gotta account for bounds of enemy

        ScheduleNextEnemySpawn();
    }

    void ScheduleNextEnemySpawn()
    {
        float spawninNSeconds;
        if (maxSpawnRateInSeconds > 1f)
        {
            spawninNSeconds = Random.Range(1f, maxSpawnRateInSeconds);
        }
        else
            spawninNSeconds = 1f;

        Invoke("SpawnEnemy", spawninNSeconds);
    }

    void IncreaseSpawnRate()
    {
        if (maxSpawnRateInSeconds > 1f)
            maxSpawnRateInSeconds--;
        if (maxSpawnRateInSeconds == 1f)
            CancelInvoke("IncreaseSpawnRate");
    }


}
