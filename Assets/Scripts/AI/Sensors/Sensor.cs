using UnityEngine;
using System.Collections;

public class Sensor : MonoBehaviour {

	public bool collisionDetection = false;
	public bool grassDetection = false;
	public bool obstacleDetection = false;

	public float grassDistance;
	public float obstacleDistance;

	private static float detectionDistance = 6.0f;
	public float distance;

	public Vector3 hitPosition;

	private Color rayColor;

	private Transform parentTransform;

	// Use this for initialization
	void Start () {
		parentTransform = GetComponentInParent<Transform> ();

		if (gameObject.name == "LeftSensor" || gameObject.name == "RightSensor") {
			rayColor = Color.green;
		} else {
			rayColor = Color.blue;
		}
	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		Ray ray = new Ray (transform.position, transform.forward);
		Debug.DrawRay (transform.position, transform.forward*detectionDistance, rayColor);

		if (Physics.Raycast (ray, out hit, detectionDistance)) {

			//Mud/Trap detection
			if (hit.collider.tag == "Mud" || hit.collider.tag == "Trap") {
				//print (gameObject.name + ": " + hit.collider.tag);
				//Time.timeScale = 0;

				hitPosition = hit.collider.transform.position;
				distance = Vector3.Distance (transform.position, hitPosition);
				Debug.DrawLine (transform.position, hitPosition);

				collisionDetection = true;

			} else {
				collisionDetection = false;	
			}

			//Grass detection
			if (hit.collider.tag == "Grass") {
				grassDistance = Vector3.Distance (parentTransform.position, hit.collider.transform.position);

				if (grassDistance < 3.0f) {
					grassDetection = true;
				}

			} else {
				grassDetection = false;
			}

			//Obstacle detection

			if (hit.collider.tag == "Obstacle") {
				obstacleDetection = true;
				obstacleDistance = Vector3.Distance (parentTransform.position, hit.collider.transform.position);
			} else {
				obstacleDetection = false;
			}
		}
	}

}
