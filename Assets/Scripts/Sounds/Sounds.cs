using UnityEngine;
using System.Collections;

public class Sounds : MonoBehaviour {

	protected float angularThreshold = 3.1f;

	public AudioSource forward;
	public AudioSource longSlide;
	public AudioSource projectile;
	public AudioSource trap;
	public AudioSource wilhemScream;
	public AudioSource boom;

	private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	protected void DrivingSounds(float currentSpeed, float currentAngularSpeed){

		//Forward - Play on Awake
		forward.volume = currentSpeed/5.0f;


		//Turn
		if (currentSpeed > 3.0f) {

			if (currentAngularSpeed >= angularThreshold && !longSlide.isPlaying) {
				longSlide.volume = 0.5f / currentSpeed;
				longSlide.Play ();
			}			
		}
	}

	protected void BoostSounds(bool hasProjectile){

		if (Input.GetButtonDown ("Fire1") || Input.GetKeyDown (KeyCode.Space)) {

			if (hasProjectile) {
				projectile.Play ();
			}
		}
	}

	protected void PlayTrap(Collider other){

		if (other.tag == "Trap") {
			trap.Play ();
		}
	}

	protected void PlayExplosion(bool explode){

		if (explode) {

			if (!wilhemScream.isPlaying) {
				wilhemScream.Play ();
			}
			if (boom.isPlaying) {
				boom.Play ();
			}
		}
	}
}
