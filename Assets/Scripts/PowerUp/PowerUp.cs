using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public enum PowerType {
		Projectile		= 0,
		Trap			= 1,
		Boost			= 2
	}

	public PowerType powerUpType = PowerType.Projectile;
	private GameObject playerGameObject;
	private GameObject enemyGameObject;
	private Renderer rend;
	private Collider coll;


	public float respawnTimer 		= 10.0f;
	public float resetRespawnTimer  = 10.0f;
	public bool respawnTimerActive  = false;

	// Use this for initialization
	void Start () {
		enemyGameObject = GameObject.FindGameObjectWithTag ("Enemy");
		playerGameObject = GameObject.FindGameObjectWithTag ("Player");
		rend = GetComponent<Renderer> ();
		coll = GetComponent<Collider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (respawnTimerActive) {

			respawnTimer -= Time.deltaTime;

			if (respawnTimer <= 0) {

				respawnTimer = resetRespawnTimer;
				respawnTimerActive = false;
				rend.enabled = true;
				coll.enabled = true;

			}
		}
	}

	void OnTriggerEnter(Collider other){

		PlayerProperties playerProperties = playerGameObject.GetComponent<PlayerProperties> ();
		PlayerProperties playerPropertiesEnemy =  enemyGameObject.GetComponent<PlayerProperties> ();

		if (other.tag == "Player" && playerProperties.canPickUp) {
			ApplyPowerUp (playerProperties);

			respawnTimerActive = true;
			rend.enabled = false;
			coll.enabled = false;
		}

		if (other.tag == "Enemy" && playerPropertiesEnemy.canPickUp) { 
			ApplyPowerUp (playerPropertiesEnemy);

			respawnTimerActive = true;
			rend.enabled = false;
			coll.enabled = false;
		}
	}

	public int ApplyPowerUp(PlayerProperties playerStatus){

		switch (powerUpType) {
		case PowerType.Projectile:
			if (playerStatus.playerState == PlayerProperties.PlayerState.CarNormal) {

				playerStatus.playerState = PlayerProperties.PlayerState.CarProjectile;
				playerStatus.hasProjectile = true;
				playerStatus.changeState = true;

				print ("PROJECTILE!");
			}
			break;
		
		case PowerType.Trap:
			if (playerStatus.playerState == PlayerProperties.PlayerState.CarNormal) {

				playerStatus.playerState = PlayerProperties.PlayerState.CarTrap;
				playerStatus.hasTrap = true;
				playerStatus.changeState = true;

				print ("Trap!");
			}
			break;
		
		case PowerType.Boost:

			if (playerStatus.playerState == PlayerProperties.PlayerState.CarNormal) {

				playerStatus.playerState = PlayerProperties.PlayerState.CarBoost;
				playerStatus.hasBoost = true;
				playerStatus.changeState = true;

				print ("Boost!");
			}
			break;
		}

		return (int)powerUpType;
	}


}