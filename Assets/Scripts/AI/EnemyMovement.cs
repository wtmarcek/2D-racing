using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour {

	//Basic variables
	public static bool raceStarted 	= false;
	public float aiSpeed		= 10.0f;
	public float aiTurnSpeed	= 2.0f;
	public float currentSpeed	= 0.0f;
	public float currentAngularSpeed = 0.0f;

	//Reset variables
	public float resetAiSpeed;
	public float resetAiTurnSpeed;

	//Throttle moderator variables
	public float aiSpeedModerator = 0.75f;
	public float aiSpeedGainer = 1.25f;
	private bool isTurning = false;

	//Sensors variables
	public bool mustOvertakeLeft = false;
	public bool mustOvertakeRight = false;
	public bool onGrass = false;

	public GameObject waypointController;
	public List<Transform> waypoints;
	public int currentWaypoint	=	0;
	public int checkpointWaypoint = 0;
	public Vector3 currentWaypointPosition;
	public Vector3 checkpointWaypointPosition;

	private Transform transform;  
	private Rigidbody rigidbody;
	private SpawnController spawnController;

	public Sensor[] sensor;

	// Use this for initialization
	void Start () {
		transform 	= GetComponent<Transform> ();
		rigidbody 	= GetComponent<Rigidbody> ();
		sensor 		= GetComponentsInChildren<Sensor> ();
		spawnController = GetComponent<SpawnController> ();

		resetAiSpeed = aiSpeed;
		resetAiTurnSpeed = aiTurnSpeed;

		GetWaypoints ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (raceStarted) {
			MoveTowardWaypoint ();
			Throttle ();
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

		sensorLogic ();

		//Waypoint Direction / Overtake
		if (!mustOvertakeLeft && !mustOvertakeRight) {
			Quaternion toRotation = Quaternion.LookRotation (currentWaypointPosition - transform.position);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, aiTurnSpeed);
		} else {
			Overtaking ();
		}

		rigidbody.AddRelativeForce (0, 0, aiSpeed);

		if (relativeWaypointPosition.sqrMagnitude < 10.0f) {

			currentWaypoint++;

			if (waypoints.Count <= currentWaypoint) {

				currentWaypoint = 0;
			}
		}

		currentSpeed = Mathf.Abs (transform.InverseTransformDirection (rigidbody.velocity).z);
		currentAngularSpeed = Mathf.Abs (transform.InverseTransformDirection (rigidbody.angularVelocity).y);


		//Angular Drag
		float maxAngularDrag = 1.0f;
		float currentAngularDrag = 1.5f;
		float angularDragLerpTime = 0.2f * currentSpeed;
		//Creating linear change for angular drag
		float myAngularDrag = Mathf.Lerp(currentAngularDrag, maxAngularDrag, angularDragLerpTime);

		rigidbody.angularDrag = myAngularDrag;

		//Drag
		float maxDrag = 3.0f;
		float currentDrag = 1.0f;
		float dragLerpTime = 0.1f * currentSpeed;
		//Creating linear change for drag
		float myDrag = Mathf.Lerp(currentDrag, maxDrag, dragLerpTime);

		rigidbody.drag = myDrag;
	}

	void Overtaking(){

		if (mustOvertakeLeft) {
			Quaternion toRotation = Quaternion.LookRotation (sensor[3].transform.position - transform.position);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, aiTurnSpeed);
		}

		if (mustOvertakeRight) {
			Quaternion toRotation = Quaternion.LookRotation (sensor[0].transform.position - transform.position);
			transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, aiTurnSpeed);
		}
	}

	void sensorLogic () {

		// sensor[0] - left angled
		// sensor[1] - left straight
		// sensor[2] - right straight
		// sensor[3] - right angled
		// sensor[4] - left side
		// sensor[5] - right side

		//mustOvertakeLeft - must overtake object to the left side (turn right)
		//mustOvertakeRight - must overtake object to the right side (turn left)


		//Left
		if (sensor [1].collisionDetection) {
			mustOvertakeLeft =  true;
		} 
			
		if (!sensor [0].collisionDetection && !sensor [1].collisionDetection) {
			mustOvertakeLeft = false;
		}

		//Right
		if(sensor[2].collisionDetection) {
			mustOvertakeRight = true;
		}

		if (!sensor [2].collisionDetection && !sensor [3].collisionDetection) {
			mustOvertakeRight = false;
		}

		//Both
		if(mustOvertakeLeft && mustOvertakeRight){
			if (sensor [5].grassDetection == true) {
				mustOvertakeLeft = false;
			}
			if (sensor [4].grassDetection == true) {
				mustOvertakeRight = false;
			}
		}

		//Hover over grass
		if (onGrass) {

			if (sensor [4].grassDetection) {
				mustOvertakeLeft = true;
			}

			if (sensor [5].grassDetection) {
				mustOvertakeRight = true;
			}
		}

		// sensor[0] - left angled
		// sensor[1] - left straight
		// sensor[2] - right straight
		// sensor[3] - right angled
		// sensor[4] - left side
		// sensor[5] - right side

		//Obstacles
		/*if (sensor [1].obstacleDetection || sensor [2].obstacleDetection) {

			if (sensor [1].obstacleDetection && sensor [1].obstacleDistance < 4.0f) {
				aiSpeed = aiSpeed * aiSpeedModerator;
				mustOvertakeLeft = true;
				Time.timeScale = 0;
			} else {
				aiSpeed = resetAiSpeed;
				mustOvertakeLeft = false;
			}

			if (sensor [2].obstacleDetection && sensor[2].obstacleDistance < 2.0f) {
				aiSpeed = aiSpeed * aiSpeedModerator;
				mustOvertakeRight = true;
			}
		}*/
	}

	//Checking if AI drives straight
	bool NoManeveur(){
		if (!isTurning && !mustOvertakeLeft && !mustOvertakeRight) {
			return true;
		} else {
			return false;
		}
	}

	void Throttle(){

		float distance;

		distance = Vector3.Distance(transform.position, currentWaypointPosition);
		Debug.DrawLine (transform.position, currentWaypointPosition, Color.black);
		aiSpeed = resetAiSpeed;
		if (NoManeveur()) {
			
			aiSpeed = resetAiSpeed;
			aiSpeed = aiSpeedGainer * aiSpeed;
			//Time.timeScale = 1f;
			//Debug.Log ("nakurwiam");

		} else {
			aiSpeed = resetAiSpeed;
			//Time.timeScale = 1f;
			//Debug.Log ("koniec nakurwiania");
			//Debug.Log (distance);
		}
	}


	void OnTriggerEnter(Collider other){

		if (other.tag == "CheckPoint") {
			checkpointWaypoint = currentWaypoint;
			checkpointWaypointPosition = currentWaypointPosition;
		}

		if (other.tag == "DoubleSharp") {
			aiSpeed = aiSpeed * aiSpeedModerator;
		}

		if (other.tag == "Grass") {
			onGrass = true;
		}

		if (other.tag == "DoubleSharp" || other.tag == "Turn") {
			isTurning = true;
		}
	}

	void OnTriggerStay(Collider other){

	}

	void OnTriggerExit(Collider other){

		if (other.tag == "DoubleSharp") {
			aiSpeed = resetAiSpeed;
		}

		if (other.tag == "Grass") {
			onGrass = false;
		}

		if (other.tag == "DoubleSharp" || other.tag == "Turn") {
			isTurning = false;
		}
	}
}
