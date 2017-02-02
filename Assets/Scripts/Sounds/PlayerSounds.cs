using UnityEngine;
using System.Collections;

public class PlayerSounds : Sounds {


	public float timer = 3.0f;
	private float resetTimer;
	private bool timerStart = false;


	private PlayerMovement playerMovement;
	private PlayerProperties playerProperties;
	private SpriteAnimation spriteAnimation;

	// Use this for initialization
	void Start () {
		playerMovement = GetComponent<PlayerMovement> ();
		playerProperties = GetComponent<PlayerProperties> ();
		spriteAnimation = GetComponentInChildren<SpriteAnimation> ();


		resetTimer = timer;
	}
	
	// Update is called once per frame
	void Update () {
		
		DrivingSounds (playerMovement.currentSpeed, playerMovement.currentAngularVelocity);
		BoostSounds (playerProperties.hasProjectile);
		PlayExplosion (spriteAnimation.explode);
	}

	bool Timer (){

		if (timerStart) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				timer = resetTimer;
				timerStart = false;
			}
		}

		if (timer == resetTimer) {
			return true;
		} else {
			return false;
		}

	}

	void OnTriggerEnter(Collider other){

		PlayTrap (other);
	}
}
