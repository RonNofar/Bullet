using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawners : MonoBehaviour {

	[Header("Spawner settings")]
	public Spawner[] spawnersArray;

	//Other var
	private int index;
	private float outScreenOffset = 1f;
	Vector2 bornPosition;

	//Custom class Spawner
	[System.Serializable]
	public class Spawner {
		public string name;
		public GameObject enemy;
		public float spawnRate;
		public int maxEnemiesPerWave;

		[HideInInspector]
		public int currentEnemies;
	}
		
	IEnumerator Spawn(){

		//Spawn wave
		while (true) {
			
			//Get random spawner
			index = Random.Range (0, spawnersArray.Length);
			Spawner OnlineSpawner = spawnersArray [index];
			GameController newGameController = (GameController)FindObjectOfType (typeof(GameController));

			//Get points position
			Camera mainCamera = Camera.main;
			float cameraHeight = mainCamera.orthographicSize / 2f;
			float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
			bornPosition = new Vector2 (Random.Range(-cameraWidth +1,cameraWidth-1), cameraHeight * 2) * outScreenOffset;

			while (OnlineSpawner.currentEnemies < OnlineSpawner.maxEnemiesPerWave) {

				//Instantiate new enemy
				GameObject newEnemy = Instantiate (OnlineSpawner.enemy, bornPosition, Quaternion.identity) as GameObject;

				//Set new name
				newEnemy.name = (OnlineSpawner.name);

				//Set "Enemy" tag
				newEnemy.tag = ("Enemy");

				//++ to max enemies
				OnlineSpawner.currentEnemies++;

				yield return new WaitForSeconds (OnlineSpawner.spawnRate);
		
			}
			OnlineSpawner.currentEnemies = 0;
			yield return new WaitForSeconds (newGameController.waveDelay);
		}
	}
}

