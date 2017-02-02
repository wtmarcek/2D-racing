using UnityEngine;
using System.Collections;

public class SpriteAnimation : MonoBehaviour {

	//Sprite net
	public int columns	= 8;
	public int rows 	= 8;

	//Animation control variables
	private Vector2 frameSize;
	private Vector2 frameOffset;
	private Vector2 framePosition;

	public float spinTimer		= 1.0f;
	private float resetSpinTimer;
	public int currentFrame 	= 1;
	public int currentAnim 		= 0;
	public float animTime 		= 0.0f;
	public float maxFps			= 1.0f;
	public float minFps 		= 10.0f;
	private float fps			= 10.0f;
	private float carVelocity	= 0.0f;
	private int i;

	public bool explode			= false;
	public static bool spin		= false;

	//Animation frames
	private int idle        	= 17;
	private int idleLeft 		= 1;
	private int idleRight		= 2;
	private int driveMin		= 3;
	private int driveMax 		= 4;
	private int driveLeftMin 	= 5;
	private int driveLeftMax 	= 6;
	private int driveRightMin 	= 7;
	private int driveRightMax 	= 8;
	private int spinFrame		= 9;
	private int explosionMin 	= 10;
	private int explosionMax	= 16;

	//Animation ID variables
	private int animIdle		=0;
	private int animIdleLeft	=1;	
	private int animIdleRight	=2;	
	private int animDrive		=3;
	private int animDriveLeft	=4;
	private int animDriveRight	=5;
	private int animSpin		=6;
	private int animExplosion	=7;

	private PlayerMovement playerMovement;
	private Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		playerMovement = transform.parent.GetComponent<PlayerMovement> ();

		resetSpinTimer = spinTimer;
	}
	
	// Update is called once per frame
	void Update () {
		HandleAnimation ();
	}

	void HandleAnimation(){
		FindAnimation ();
		PlayAnimation ();
	}

	void FindAnimation(){
		carVelocity = playerMovement.currentSpeed;

		//In motion
		if (carVelocity > 0.3f) {
			currentAnim = animDrive;

			//Turning while drive
			if (Input.GetAxis ("Horizontal") < 0.0f) {
				currentAnim = animDriveLeft;
			} else if (Input.GetAxis ("Horizontal") > 0.0f) {
				currentAnim = animDriveRight;
			}
		}

		// Idle
		if (carVelocity <= 0.3f) {
			currentAnim = animIdle;

			if (Input.GetAxis ("Horizontal") < 0.0f) {
				currentAnim = animIdleLeft;
			} else if (Input.GetAxis ("Horizontal") > 0.0f) {
				currentAnim = animIdleRight;
			}
		}

		//Explosion
		if (explode) {
			currentAnim = animExplosion;
		}

		if (spin) {
			currentAnim = animSpin;
		}
	}

	void PlayAnimation(){

		//Animation timer
		fps = Mathf.Lerp(minFps, maxFps, 1/carVelocity);
		animTime -= Time.deltaTime;

		if (animTime <= 0) {
			currentFrame += 1;
			animTime += 1.0f / fps/1.5f;
		}

		//Non-Looping animations

		if (currentAnim == animExplosion) {
			currentFrame = Mathf.Clamp (currentAnim, explosionMin, explosionMax + 1);
			if (currentFrame > explosionMax) {
				explode = false;
			}
		}

		if (currentAnim == animSpin) {
			currentFrame = spinFrame;
			spinTimer -= Time.deltaTime;

			if (spinTimer <= 0) {
				spinTimer = resetSpinTimer;
				spin = false;
			}
		}

		//Looping animations
		if (currentAnim == animIdle) {
			currentFrame = Mathf.Clamp (currentFrame, idle, idle);
		}

		if (currentAnim == animIdleLeft) {
			currentFrame = Mathf.Clamp (currentFrame, idleLeft, idleLeft);
		}

		if (currentAnim == animIdleRight) {
			currentFrame = Mathf.Clamp (currentFrame, idleRight, idleRight);
		}

		if (currentAnim == animDrive) {
			currentFrame = Mathf.Clamp (currentFrame, driveMin, driveMax+1);
			if (currentFrame > driveMax) {
				currentFrame = driveMin;
			}
		}

		if (currentAnim == animDriveLeft) {
			currentFrame = Mathf.Clamp (currentFrame, driveLeftMin, driveLeftMax+1);
			if (currentFrame > driveLeftMax) {
				currentFrame = driveLeftMin;
			}
		}

		if (currentAnim == animDriveRight) {
			currentFrame = Mathf.Clamp (currentFrame, driveRightMin, driveRightMax+1);
			if (currentFrame > driveRightMax) {
				currentFrame = driveRightMin;
			}
		}


		framePosition.y = 1;
		for(i = currentFrame; i > columns; i -= rows){
			framePosition.y += 1;
		}
		framePosition.x = i - 1;

		frameSize = new Vector2 (1.0f / columns, 1.0f / rows);
		frameOffset = new Vector2 (framePosition.x / columns, 1.0f - (framePosition.y / rows));

		rend.material.SetTextureScale ("_MainTex", frameSize);
		rend.material.SetTextureOffset ("_MainTex", frameOffset);
	}



}
