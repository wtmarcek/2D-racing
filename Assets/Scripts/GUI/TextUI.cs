using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextUI : MonoBehaviour {

	private float timer = 1.0f;
	private float beginningTimer = 3.0f;
	private float resetTimer;

	public EnemyAnimation enemyAnimaiton;

	public Text textsUI;
	public Text raceState;

	// Use this for initialization
	void Start () {
		textsUI.enabled = false;
		raceState.enabled = false;

		resetTimer = timer;
	}
	
	// Update is called once per frame
	void Update () {
		DisplayText ();
	}

	void DisplayText() {

		//Damage received
		if (DamageController.playerDamage) {
			textsUI.enabled = true;
			textsUI.text = "WOOPS!";
			timer -= Time.deltaTime;

			if (timer <= 0.0f) {
				timer = resetTimer;
				DamageController.playerDamage = false;
				textsUI.enabled = false;
			}
		} 

		//Damage dealt
		if (DamageController.enemyDamage) {
			textsUI.enabled = true;
			textsUI.text = "HAHA!";
			timer -= Time.deltaTime;

			if (timer <= 0.0f) {
				timer = resetTimer;
				DamageController.enemyDamage = false;
				textsUI.enabled = false;
			}
		}

		//Race beginning
		if (GameState.gameBeginning) {
			beginningTimer -= Time.deltaTime;
			raceState.enabled = true;

			if (beginningTimer < 3.0f) {
				raceState.text = "Ready";
			}
			if (beginningTimer < 2.0f) {
				raceState.text = "Steady";
			}
			if (beginningTimer < 1.0f) {
				raceState.text = "GO!";
			}
			if (beginningTimer < 0.0f) {
				GameState.gameBeginning = false;
				raceState.enabled = false;
				//Debug.Log("tutaj jestem");
			}
		}

		//Race finish
		if (GameState.gameFinished) {
			raceState.enabled = true;
			raceState.text = "RACE FINISHED";
		}
	}
		
} 
