using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class HeroController : MonoBehaviour {

	//Hero Parameters
	[Header("Hero Stuff")]
	GameObject Hero;
	public float maxHeroHealth = 100f;
	public float currentHeroHealth = 100f;
	public GameObject heroDeathFX;
	
	[Header("Hero Movement")]
	public float heroSpeed = 1f;

	[Header("Hero Weapons")]
	public GameObject Projectile;
	public float weaponRate;

	float cameraWidth;
	public bool isDeath = false;

	void Start (){
	
		Hero = this.gameObject;
		Camera mainCamera = Camera.main;
		cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;

	}
		
	void Update(){
		if (isDeath == false) {
			Move ();
			CheckHealth ();
		}
	}

	//Hero Movement instrution
	public void Move (){
		Vector2 mousePosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

		if(Input.GetMouseButton(0)) {
			if (mousePosition.x < Screen.width / 2) {
				if (-Hero.transform.position.x < cameraWidth) {
					Hero.transform.Translate (-Vector2.right * heroSpeed * Time.deltaTime);
				}
			} 
			else {
				if (Hero.transform.position.x < cameraWidth) {
					Hero.transform.Translate (Vector2.right * heroSpeed * Time.deltaTime);
				}
			}
		}
	}

	//Hero Fire
	IEnumerator Fire (){

		while (true) {
			Instantiate (Projectile, gameObject.transform.position, Quaternion.identity);
			yield return new WaitForSeconds (weaponRate);
			}
		}

	//Health
	void CheckHealth(){
		Slider healthSlider = GameObject.Find ("HealthSlider").GetComponent<Slider>();
		healthSlider.value = currentHeroHealth;
	}

	//Any damage event
	public void GetDamage(float damage){ 
		if (currentHeroHealth <= 0) {
			heroDeath ();
			StartCoroutine ("Shake");
		} 
		else {
			currentHeroHealth = currentHeroHealth - damage;
			StartCoroutine ("Shake");
		}
	}

	//Hero death event
	public void heroDeath(){
		isDeath = true;
		GameObject deathFX = Instantiate (heroDeathFX, gameObject.transform.position, Quaternion.identity) as GameObject;
		Destroy (deathFX, heroDeathFX.GetComponent<ParticleSystem> ().duration);
		Hero.GetComponent<SpriteRenderer> ().enabled = false;
		Hero.GetComponent<BoxCollider2D> ().enabled = false;
		GameController newGameController = (GameController)FindObjectOfType (typeof(GameController));
		newGameController.curretGameStatus = GameController.gameStatus.Gameover;
		newGameController.CheckGameStatus ();
		StopCoroutine ("Fire");
	}

	//Collision with Enemy
	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == ("Enemy")) {
			GetDamage (10);
		}
	}

	//Camera FX
	IEnumerator Shake() {
		
		float duration = 0.25f;
		float magnitude = 0.25f;
		float elapsed = 0.1f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;          

			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}
		Camera.main.transform.position = originalCamPos;
	}
}