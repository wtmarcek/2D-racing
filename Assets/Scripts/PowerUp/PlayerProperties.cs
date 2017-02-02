using UnityEngine;
using System.Collections;

public class PlayerProperties : MonoBehaviour {

	public enum PlayerState{
		CarDead			= 0,
		CarNormal		= 1,
		CarProjectile	= 2,
		CarTrap			= 3,
		CarBoost		= 4
	}

	public PlayerState playerState = PlayerState.CarNormal;

	//PowerUp variables
	public GameObject projectile;
	public GameObject trap;
	public GameObject boost;

	//Power socket varaibles
	public Transform projectileSocket;
	public Transform trapSocket;
	public Transform boostSocket;

	public bool hasProjectile	 = false;
	public bool hasTrap			 = false;
	public bool hasBoost 		 = false;

	public bool changeState		 = false;
	public bool canPickUp		 = true;

	//Booster settings
	public float boostModifier	 = 2.5f;
	public float boostTimer		 = 2.0f;
	public float resetBoostTimer = 2.0f;
	public bool boostTimerActive = false;

	//Projectile settings
	public float projectileSpeed;

	private Transform trans;
	private Transform transTrap;
	private PlayerMovement playerMovement;
	private EnemyMovement enemyMovement;
	private GameObject playerGO;


	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform> ();
		transTrap = trapSocket.GetComponent<Transform> ();
		playerMovement = GetComponent<PlayerMovement> ();
		enemyMovement = GetComponent<EnemyMovement> ();
		playerGO = GameObject.FindGameObjectWithTag ("Player");

		resetBoostTimer = boostTimer;

		if (gameObject.tag == "Player") {
			
			projectileSpeed = playerMovement.speed * 50;
		}

		if (gameObject.tag == "Enemy") {
			
			projectileSpeed = enemyMovement.aiSpeed * 50;
		}
	}
	
	// Update is called once per frame
	void Update () {

		Booster ();

		if (changeState) {
			SetPlayerState ();
		}

		if (hasProjectile && gameObject.tag == "Player") {
			GameObject cloneProjectile;

			if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown(KeyCode.Space)) {

				cloneProjectile = (GameObject)Instantiate (projectile, projectileSocket.transform.position, trans.rotation);
				cloneProjectile.GetComponent<Rigidbody> ().AddForce (trans.forward * projectileSpeed);

				this.playerState = PlayerState.CarNormal;
				this.changeState = true;
			}


		}

		if (hasTrap && gameObject.tag == "Player") {
			GameObject cloneTrap;

			if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown(KeyCode.Space)) {

				cloneTrap = (GameObject)Instantiate (trap, trapSocket.transform.position, transTrap.rotation);
				cloneTrap.GetComponent<Rigidbody> ().AddForce (trans.forward);

				this.playerState = PlayerState.CarNormal;
				this.changeState = true;
			}
		}

		if (hasBoost && gameObject.tag == "Player") {

			GameObject cloneBoost;

			if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown(KeyCode.Space)) {

				cloneBoost = (GameObject)Instantiate (boost, boostSocket.transform.position, trans.rotation);
				cloneBoost.transform.parent = boostSocket;

				boostTimerActive = true;	
				playerMovement.speed = playerMovement.speed * boostModifier;

				this.playerState = PlayerState.CarNormal;
				this.changeState = true;
			}
		}
	}

	void SetPlayerState(){

		switch (playerState) {

		case PlayerState.CarNormal:
			hasProjectile		= false;
			hasTrap 			= false;
			hasBoost 			= false;
			changeState 		= false;
			canPickUp 			= true;
		break;

		case PlayerState.CarProjectile:
			hasProjectile 		= true;
			hasTrap				= false;
			hasBoost 			= false;
			changeState 		= false;
			canPickUp 			= false;
		break;	

		case PlayerState.CarTrap:
			hasProjectile 		= false;
			hasTrap				= true;
			hasBoost 			= false;
			changeState 		= false;
			canPickUp 			= false;
		break;	

		case PlayerState.CarBoost:
			hasProjectile 		= false;
			hasTrap				= false;
			hasBoost 			= true;
			changeState 		= false;
			canPickUp 			= false;
		break;	

		case PlayerState.CarDead:
			hasProjectile 		= false;
			hasTrap				= false;
			hasBoost 			= false;
			changeState 		= false;
			canPickUp 			= false;
		break;	
		
		}
	}


	void Booster(){

		if (boostTimerActive) {

			boostTimer -= Time.deltaTime;

			if (boostTimer <= 0) {
				boostTimerActive = false;
				boostTimer = resetBoostTimer;
				playerMovement.speed = resetBoostTimer;
			}
		}
	}



}
