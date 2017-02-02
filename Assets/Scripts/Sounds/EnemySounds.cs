using UnityEngine;
using System.Collections;

public class EnemySounds : Sounds {

	private EnemyAnimation enemyAnimation;
	private EnemyMovement enemyMovement;
	private PlayerProperties playerProperties;

	// Use this for initialization
	void Start () {

		enemyAnimation = GetComponentInChildren<EnemyAnimation> ();
		enemyMovement = GetComponent<EnemyMovement> ();
		playerProperties = GetComponent<PlayerProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		PlayExplosion (enemyAnimation.explode);
		//DrivingSounds (enemyMovement.currentSpeed, enemyMovement.currentAngularSpeed);
		BoostSounds (playerProperties.hasProjectile);
	}

	void OnTriggerEnter(Collider other){

		PlayTrap (other);
	}
}
