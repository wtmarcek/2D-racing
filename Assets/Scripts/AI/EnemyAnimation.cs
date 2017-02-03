using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour {

	//Sprite net
	public int columns	= 8;
	public int rows 	= 8;

	//Animation control variables
	private Vector2 frameSize;
	private Vector2 frameOffset;
	private Vector2 framePosition;

	//Animation control variables
	private float spinTimer;
	private float resetSpinTimer;
	public int currentFrame 	= 1;
	public int currentAnim 		= 0;
	public float animTime 		= 0.0f;
	public float maxFps			= 1.0f;
	public float minFps 		= 10.0f;
	private float fps			= 10.0f;
	private float carVelocity;
	private int i;
	private Vector3 currentWaypointPosition;
	public float angle			= 0.0f;

	//Bool variables
	public bool explode			= false;
	public bool turn			= false;
	public static bool spin		= false;

	//Animation frames
	private int idle        	= 41;
	private int driveMin		= 27;
	private int driveMax 		= 28;
	private int driveLeftMin 	= 29;
	private int driveLeftMax 	= 30;
	private int driveRightMin 	= 31;
	private int driveRightMax 	= 32;
	private int spinFrame		= 33;
	private int explosionMin 	= 34;
	private int explosionMax	= 40;

	//Animation ID variables
	private int animIdle		=0;
	private int animDrive		=1;
	private int animDriveLeft	=2;
	private int animDriveRight	=3;
	private int animSpin		=4;
	private int animExplosion	=5;

	private EnemyMovement enemyMovement;
	private Renderer rend;
	private SpriteAnimation spriteAnimation;
	private GameObject playerGO;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		enemyMovement = transform.parent.GetComponent<EnemyMovement> ();
		playerGO = GameObject.FindGameObjectWithTag ("Player");
		spriteAnimation = playerGO.GetComponent<SpriteAnimation> ();

		if (spriteAnimation != null) {
			spinTimer = spriteAnimation.spinTimer;
			resetSpinTimer = spinTimer;
		}

	}

	// Update is called once per frame
	void Update () {
		HandleAnimation ();

		angle = FindAngle (transform.forward, currentWaypointPosition - transform.parent.position, transform.up);
	}

	void HandleAnimation(){
		FindAnimation ();
		PlayAnimation ();
	}


	float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector){

		if (toVector == Vector3.zero) {
			return 0.0f;
		}

		float angle = Vector3.Angle (fromVector, toVector);
		Vector3 normal = Vector3.Cross (fromVector, toVector);


		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
		angle *= Mathf.Deg2Rad;

		return angle;
	}

	void FindAnimation(){

		float angle = FindAngle (transform.forward, currentWaypointPosition - transform.parent.position, transform.up);

		carVelocity = enemyMovement.currentSpeed;
		currentWaypointPosition = enemyMovement.currentWaypointPosition;

		//In motion
		if (carVelocity > 0.3f) {

			currentAnim = animDrive;

			if (turn) {
				if(angle > 0.0f)			{
					currentAnim = animDriveRight;
				} 
				else if (angle < 0.0f)			{
					currentAnim = animDriveLeft;
				}
			}


		}

		// Idle
		if (carVelocity <= 0.3f) {
			currentAnim = animIdle;
		}

		//Explosion
		if (explode) {
			currentAnim = animExplosion;
		}

		//Spin
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
			currentFrame = Mathf.Clamp (currentFrame, explosionMin, explosionMax + 1);

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

	void OnTriggerStay(Collider other){

		if (other.tag == "Turn" || other.tag == "DoubleSharp") {
			turn = true;
		} 			
	}

	void OnTriggerExit(Collider other ){

		if (other.tag == "Turn" || other.tag == "DoubleSharp") {

			turn = false;
		}
	}


}
