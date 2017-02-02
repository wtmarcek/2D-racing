using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public static bool gameStarted = false;
	public static bool gameFinished = false;
	public static bool gameBeginning = true;

	public float raceStartTimer			= 0.0f;
	private float resetRaceStartTimer	= 0.0f;

	public int totalLaps				= 3;

	public GameObject playerCar;
	public GameObject enemyCar;

	private PlayerMovement playerMovement;
	private EnemyMovement enemyMovement;

	// Use this for initialization
	void Start () {

		resetRaceStartTimer = raceStartTimer;

		playerCar = GameObject.FindGameObjectWithTag ("Player");
		playerMovement = playerCar.GetComponent<PlayerMovement> ();
		playerMovement.enabled = false;	

		enemyCar = GameObject.FindGameObjectWithTag ("Enemy");
		enemyMovement = enemyCar.GetComponent<EnemyMovement> ();

	}
	
	// Update is called once per frame
	void Update () {
		print (CheckpointController.currentLap);

		if (!gameBeginning) {
			EnemyMovement.raceStarted = true;
			playerMovement.enabled = true;
			gameStarted = true;
		} 
		if (gameBeginning) {
			EnemyMovement.raceStarted = false;
			playerMovement.enabled = false;
		}

		raceStartTimer -= Time.deltaTime;

		if (raceStartTimer <= 0 && !gameBeginning) {

			playerMovement.enabled = true;
			EnemyMovement.raceStarted = true;
		}

		if (CheckpointController.currentLap == totalLaps + 1) {

			gameFinished = true;
			EnemyMovement.raceStarted = false;
			playerMovement.enabled = false;
		}
	}
}
