using UnityEngine;
using System.Collections;

public class BoostAI : MonoBehaviour {

	//Distances
	public float distance;
	public float properDistTrap = 5.0f;
	public float properDistProj = 5.0f;

	//Angles
	private float angleToward;
	private float angleBackward;
	public float properAngleTrap = 30.0f;
	public float properAngleProj = 15.0f;

	public int waypointNum = 1;
	public float timer = 0.25f;

	//Vectors
	public Vector3 targetDirection;

	private PlayerProperties playerProperties;
	private EnemyMovement enemyMovement;
	private GameObject playerGO;	 

	// Use this for initialization
	void Start () {
		playerProperties = GetComponent<PlayerProperties> ();
		enemyMovement = GetComponent<EnemyMovement>();
		playerGO = GameObject.FindGameObjectWithTag ("Player");


	}	
	
	// Update is called once per frame
	void Update () {

		distance 		= Vector3.Distance (transform.position, playerGO.transform.position);
		targetDirection = playerGO.transform.position - transform.position;
		angleToward 	= Vector3.Angle (transform.forward, targetDirection);
		angleBackward 	= Vector3.Angle(-transform.forward, targetDirection);

		Debug.DrawRay (transform.position, targetDirection, Color.blue);
		Debug.DrawRay (transform.position, transform.forward*20.0f, Color.red);

		TrapRelease ();
		ProjectileRelease ();
		//BoostRelease ();
	}

	void TrapRelease() {

		GameObject cloneTrap;

		if (playerProperties.hasTrap && distance < properDistTrap && angleBackward < properAngleTrap) {

			cloneTrap = (GameObject)Instantiate (playerProperties.trap, playerProperties.trapSocket.transform.position, playerProperties.trapSocket.transform.rotation);
			cloneTrap.GetComponent<Rigidbody> ().AddForce (transform.forward);

			playerProperties.playerState = PlayerProperties.PlayerState.CarNormal;
			playerProperties.changeState = true;
		}
	}

	void ProjectileRelease(){

		GameObject cloneProjectile;

		if (playerProperties.hasProjectile && distance < properDistProj && angleToward < properAngleProj) {

			cloneProjectile = (GameObject)Instantiate (playerProperties.projectile, playerProperties.projectileSocket.transform.position, playerProperties.projectileSocket.transform.rotation);
			cloneProjectile.GetComponent<Rigidbody> ().AddForce (transform.forward * playerProperties.projectileSpeed);

			playerProperties.playerState = PlayerProperties.PlayerState.CarNormal;
			playerProperties.changeState = true;
		}
	}

	void BoostRelease(){
		GameObject cloneBoost;

		cloneBoost = (GameObject)Instantiate (playerProperties.boost, playerProperties.boostSocket.transform.position, playerProperties.boostSocket.transform.rotation);
		cloneBoost.transform.parent = playerProperties.boostSocket;

		playerProperties.boostTimerActive = true;	
		enemyMovement.aiSpeed = enemyMovement.aiSpeed * playerProperties.boostModifier;

		playerProperties.playerState = PlayerProperties.PlayerState.CarNormal;
		playerProperties.changeState = true;
	}

	float waypointDistance(){

		//for (waypointNum; waypointNum < enemyMovement.waypoints; waypointNum++) {


		//}

		return 0.0f;
	}

	void TimerStart(){

		timer -= Time.deltaTime;

	}
}
