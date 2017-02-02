using UnityEngine;
using System.Collections;

public class PlayerMovement1 : VehicleMovement {

	public float speed = 10.0f;
	public float reverseSpeed = 5.0f;
	public float turnSpeed = 0.6f;

	public float grassSpeed = 3.0f;
	public float resetSpeed = 0.0f;
	public float resetTurnSpeed = 0.0f;

	public float currentSpeed = 0.0f;
	private float movementDirection = 0.0f;
	private float turnDirection = 0.0f;

	private Rigidbody rigidb;
	private Transform trans;

	// Use this for initialization
	void Start () {
		rigidb = GetComponent<Rigidbody>();
		trans = GetComponent<Transform> ();
		resetSpeed = speed;
		resetTurnSpeed = turnSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		currentSpeed = Mathf.Abs (trans.InverseTransformDirection (rigidb.velocity).z);

		movement (currentSpeed);
		angularDrag (currentSpeed);
		drag (currentSpeed);
	}



	void movement(float currentSp){
		movementDirection = Input.GetAxis ("Vertical");
		turnDirection = Input.GetAxis ("Horizontal");


		//FORWARD MOVEMENT
		if (movementDirection >= 0.0f) {
			movementDirection = movementDirection * speed;
			if (currentSp > 0.5f) {
				rigidb.AddRelativeTorque (0, turnDirection, 0);
			}
		
		// REVERSE MOVEMENT
		} else {
			movementDirection = movementDirection * reverseSpeed;
			if (currentSp > 0.5f) {
				rigidb.AddRelativeTorque (0, -turnDirection, 0);
			}
		}

		rigidb.AddRelativeForce (0, 0, movementDirection);
	}



	float drag(float currentSp){
		float maxDrag = 2.5f;
		float currentDrag = 1.0f;
		float dragLerpTime = 0.1f * currentSp;
		//Creating linear change for drag
		float myDrag = Mathf.Lerp(currentDrag, maxDrag, dragLerpTime);

		rigidb.drag = myDrag;

		return myDrag;
			
	}



	float angularDrag(float currentSp){

		float maxAngularDrag = 1.5f;
		float currentAngularDrag = 2.5f;
		float angularDragLerpTime = 0.2f * currentSp;
		//Creating linear change for angular drag
		float myAngularDrag = Mathf.Lerp(currentAngularDrag, maxAngularDrag, angularDragLerpTime);

		rigidb.angularDrag = myAngularDrag;

		return myAngularDrag;
	}

	void OnTriggerStay(Collider other){

		if (other.tag == "Grass" || other.tag == "DeadZone") {
			speed = grassSpeed;
		}
	}

	void OnTriggerExit(Collider other){
		speed = resetSpeed;
	}

}
