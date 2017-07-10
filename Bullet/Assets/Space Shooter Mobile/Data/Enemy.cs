using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	//Enemy Information
	public GameObject enemy;
	public string enemyName = ("Enemy");
	public GameObject enemyDamageFX;
	public GameObject enemyDeathFX;

	//Enemy Parameters
	public float speed = 1f;
	public float deviationMovement = 0.3f;
	private float deviation;
	public float health = 100f;
	public float fireRate = 1f;
	public float damage = 10f;
	public float reward = 10f;

	void Start (){
		deviation = Random.Range (-deviationMovement, deviationMovement);
	}

	void FixedUpdate(){
		enemy.transform.Translate (-Vector2.up * speed * Time.deltaTime);
		enemy.transform.Translate (-Vector2.right * deviation * speed * Time.deltaTime);
		Destroy (gameObject, 10);
	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == ("HeroProjectile")) {
			if (health <= 0) {
				GameObject.Find ("Game Controller").GetComponent<GameController> ().GetReward (reward);
				DeathFX ();
				Destroy (gameObject);
			} 
			else {
				health = health - other.GetComponent<HeroProjectile> ().power;
				DamageFX ();
			}
		}
	}

	void DamageFX(){
		GameObject damageFX = Instantiate (enemyDamageFX, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (damageFX, enemyDamageFX.GetComponent<ParticleSystem> ().duration);
	}

	void DeathFX(){
		GameObject deathFX = Instantiate (enemyDeathFX, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (deathFX, enemyDeathFX.GetComponent<ParticleSystem> ().duration);
	}
}