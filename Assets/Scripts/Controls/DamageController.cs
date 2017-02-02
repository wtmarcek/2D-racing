using UnityEngine;
using System.Collections;

public class DamageController : MonoBehaviour {

	public int health 		= 4;
	public int resetHealth;

	public static bool playerDamage = false;
	public static bool enemyDamage = false;

	public Transform start;
	private Transform trans;
	private SpawnController spawnController;
	private PlayerMovement playerMovement;
	private SpriteAnimation spriteAnimation;
	private EnemyAnimation enemyAnimation;
	private EnemyMovement enemyMovement;



	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform> ();
		resetHealth = health;

		if (gameObject.tag == "Enemy") {
			health = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ApplyDamage(int damage){

		//Player classes
		spawnController = gameObject.GetComponent<SpawnController> ();
		spriteAnimation = GetComponentInChildren<SpriteAnimation> ();
		playerMovement = GetComponent<PlayerMovement> ();

		//Enemy classes
		enemyMovement = GetComponent<EnemyMovement>();
		enemyAnimation = GetComponentInChildren<EnemyAnimation> ();

		Debug.Log ("Damage has been applied");
		health -= damage;

		if (gameObject.tag == "Player") {
			playerDamage = true;
		} else if (gameObject.tag == "Enemy") {
			enemyDamage = true;
		}

		if (health <= 0) {

			if (gameObject.tag == "Player") {

				spawnController.activeRespawnTimer = true;
				trans.position = start.position;
			}

			if (gameObject.tag == "Enemy") {

				spawnController.activeRespawnTimer = true;
				enemyAnimation.explode = true;
				enemyMovement.aiTurnSpeed = 0.0f;
				enemyMovement.aiSpeed = 0.0f;
				enemyMovement.currentWaypoint = enemyMovement.checkpointWaypoint;


				if (enemyAnimation.explode == false) {
					trans.position = spawnController.currentTrackPosition;

				}
			}
		}
	}
}
