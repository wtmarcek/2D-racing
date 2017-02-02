using UnityEngine;
using System.Collections;

public class SpawnController : MonoBehaviour {

	private DamageController damageController;
	private EnemyMovement enemyMovement;
	private PlayerMovement playerMovement;
	private EnemyAnimation enemyAnimation;

	private Transform trans;
	private Transform spawnPoint;
	public Vector3 currentTrackPosition;

	public bool activeRespawnTimer;
	public float respawnTimer = 1.5f;
	private float resetRespawnTimer;

	// Use this for initialization
	void Start () {
		resetRespawnTimer = respawnTimer;

		trans = GetComponent<Transform>();
		damageController = gameObject.GetComponent<DamageController> ();
		enemyAnimation = GetComponentInChildren<EnemyAnimation> ();



		if (gameObject.tag == "Enemy") {
			enemyMovement = GetComponent<EnemyMovement> ();
			spawnPoint = transform;
		}

		if (gameObject.tag == "Player") {
			playerMovement = GetComponent<PlayerMovement> ();
		}	

		spawnPoint = transform;

	}
	
	// Update is called once per frame
	void Update () {


		//Timer
		if (activeRespawnTimer) {
			respawnTimer -= Time.deltaTime;
		}

		if (respawnTimer <= 0.0f) {
			respawnTimer = resetRespawnTimer;

			trans.position = currentTrackPosition;
			activeRespawnTimer = false;

			damageController.health = damageController.resetHealth;

			if (gameObject.tag == "Enemy") {
				//Time.timeScale = 0.0f;

				enemyMovement.aiSpeed = enemyMovement.resetAiSpeed;
				enemyMovement.aiTurnSpeed = enemyMovement.resetAiTurnSpeed;
				trans.position = currentTrackPosition;
				Vector3 relativePos = enemyMovement.checkpointWaypointPosition - currentTrackPosition;
				Debug.Log ("SpawnController: " + enemyMovement.checkpointWaypointPosition);
				transform.rotation = Quaternion.LookRotation (relativePos);
				enemyMovement.currentWaypoint = enemyMovement.checkpointWaypoint;
				Debug.DrawLine (transform.position, enemyMovement.currentWaypointPosition);
				//Time.timeScale = 0.1f;
			}

			if (gameObject.tag == "Player") {

				playerMovement.speed = playerMovement.resetSpeed;
				playerMovement.turnSpeed = playerMovement.resetTurnSpeed;
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "CheckPoint" && damageController.health > 0) {
			currentTrackPosition = transform.position;
		}

		if (other.tag == "DeadZone") {
			activeRespawnTimer = true;
		}
	}


}
