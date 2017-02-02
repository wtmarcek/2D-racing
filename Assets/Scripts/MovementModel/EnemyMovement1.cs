using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement1 : VehicleMovement {

	public bool raceStarted 	= false;
	public float aiSpeed		= 10.0f;
	public float aiTurnSpeed	= 2.0f;
	public float currentSpeed	= 0.0f;

	public float resetAiSpeed;
	public float resetAiTurnSpeed;

	public GameObject waypointController;
	public List<Transform> waypoints;
	public int currentWaypoint	=	0;
	public Vector3 currentWaypointPosition;

	private Transform transform;
	private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
		rigidbody = GetComponent<Rigidbody> ();

		resetAiSpeed = aiSpeed;
		resetAiTurnSpeed = aiTurnSpeed;

		GetWaypoints ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (raceStarted) {
			MoveTowardWaypoint ();
		}
	}

	void GetWaypoints(){
		Transform[] potentialWaypoints = waypointController.GetComponentsInChildren<Transform> ();

		waypoints = new List<Transform> ();

		foreach (Transform potentialWaypoint in potentialWaypoints) {

			if (potentialWaypoint != waypointController.transform) {

				waypoints.Add (potentialWaypoint);
			}
		}
	}

	public void MoveTowardWaypoint(){

		float currentWaypointX = waypoints [currentWaypoint].position.x;
		float currentWaypointY = waypoints [currentWaypoint].position.y;
		float currentWaypointZ = waypoints [currentWaypoint].position.z;

		Vector3 relativeWaypointPosition = transform.InverseTransformPoint (new Vector3 (currentWaypointX, currentWaypointY, currentWaypointZ));
		currentWaypointPosition = new Vector3 (currentWaypointX, currentWaypointY, currentWaypointZ);

		Quaternion toRotation = Quaternion.LookRotation (currentWaypointPosition - transform.position);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, aiTurnSpeed);

		rigidbody.AddRelativeForce (0, 0, aiSpeed);

		if (relativeWaypointPosition.sqrMagnitude < 10.0f) {

			currentWaypoint++;

			if (waypoints.Count <= currentWaypoint) {

				currentWaypoint = 0;
			}
		}

		currentSpeed = Mathf.Abs (transform.InverseTransformDirection (rigidbody.velocity).z);

		//Angular Drag
		float maxAngularDrag = 1.0f;
		float currentAngularDrag = 1.5f;
		float angularDragLerpTime = 0.2f * currentSpeed;
		//Creating linear change for angular drag
		float myAngularDrag = Mathf.Lerp(currentAngularDrag, maxAngularDrag, angularDragLerpTime);

		rigidbody.angularDrag = myAngularDrag;

		//Drag
		float maxDrag = 3.5f;
		float currentDrag = 1.0f;
		float dragLerpTime = 0.1f * currentSpeed;
		//Creating linear change for drag
		float myDrag = Mathf.Lerp(currentDrag, maxDrag, dragLerpTime);

		rigidbody.drag = myDrag;
	}


}
