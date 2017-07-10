using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Gameplay flow

public class GameController : MonoBehaviour {

	//Game score
	[Header("Game Settings")]
	public float currentHeroScore = 0f;

	//Game speed
	[Header("Game Speed")]
	public float flowDelay = 1f;
	public float waveDelay = 2f;

	//All game statuses
	[Header("Flow Direction")]
	public gameStatus curretGameStatus = gameStatus.Tutorial;
	public enum gameStatus {Runtime, Pause, Tutorial, Gameover};

	//Array of created spawners
	[Header("Created Spawners")]
	public List<GameObject> createdSpawnersList;

	UIController newUIController;

	//Go
	void Start(){

		//First Game Status Check
		CheckGameStatus ();
	}

	//Set game direction
	public void CheckGameStatus () {

		switch (curretGameStatus) {
		case gameStatus.Tutorial:
			StartCoroutine ("Tutorial");
			break;
		case gameStatus.Runtime:
			StartBattle ();
			break;
		case gameStatus.Pause:
			Pause ();
			break;
		case gameStatus.Gameover:
			StopAllCoroutines ();
			newUIController = gameObject.GetComponent<UIController> ();
			newUIController.SwitchToGameOverPanel ();
			Text finalScore = GameObject.Find ("FinalScoreCount").GetComponent<Text> ();
			finalScore.text = currentHeroScore.ToString();
			break;
		}
	}

	//Tutorial instruction
	IEnumerator Tutorial(){

		yield return new WaitForSeconds (flowDelay);

		//Create hint labels
		Text hint = GameObject.Find("Hint").GetComponent<Text>();

		//Movement education
		hint.text = ("HOLD LEFT SIDE TO MOVE LEFT");
		yield return new WaitForSeconds (flowDelay);

		hint.text = ("HOLD RIGHT SIDE TO MOVE RIGHT");
		yield return new WaitForSeconds (flowDelay);

		//Fire edication
		hint.text = ("YOUR SHIP WILL SHOOT AUTOMATUCALLY");
		yield return new WaitForSeconds (flowDelay);

		//Countdown
		hint.text = ("THE BATTLE BEGIN IN 3...");
		yield return new WaitForSeconds (1);

		hint.text = ("THE BATTLE BEGIN IN 2...");
		yield return new WaitForSeconds (1);

		hint.text = ("THE BATTLE BEGIN IN 1...");
		yield return new WaitForSeconds (1);

		hint.text = ("GO!");
		yield return new WaitForSeconds (1);
	
		hint.text = null;
		curretGameStatus = gameStatus.Runtime;
		CheckGameStatus ();
	}

	//Runtime instruction
	void StartBattle(){

		GameObject.Find ("Hero").GetComponent<HeroController> ().StartCoroutine ("Fire");
		GameObject.Find ("Backgrounds").GetComponent<BackgroundController> ().BoostSpeed (10);

		foreach(GameObject Spawners in createdSpawnersList){
			Spawners.GetComponent<Spawners>().StartCoroutine ("Spawn");
		}
	}

	//Add New Spawner in Inspector
	public void AddNewSpawner(){
		
		//Create New Spawner. Set component, name, parent and add to createdSpawners array
		GameObject newSpawner = new GameObject();
		createdSpawnersList.Add (newSpawner);
		newSpawner.AddComponent<Spawners> ();
		newSpawner.name = ("New Spawner");
		newSpawner.transform.parent = gameObject.transform;
	}

	//Pause Control
	public void Pause(){

		switch (curretGameStatus) {
		case gameStatus.Runtime:
			Time.timeScale = 0;
			curretGameStatus = gameStatus.Pause;
			break;
		case gameStatus.Tutorial:
			Time.timeScale = 0;
			curretGameStatus = gameStatus.Pause;
			break;
		case gameStatus.Pause:
			Time.timeScale = 1;
			curretGameStatus = gameStatus.Runtime;
			break;
		}
	}

	//Get any rewards
	public void GetReward (float Reward){

		currentHeroScore = currentHeroScore + Reward;
		Text score = GameObject.Find("Score").GetComponent<Text>();
		score.text = (currentHeroScore.ToString());
	}
}
