using UnityEngine;
using System.Collections;

public class Predictor : MonoBehaviour {

	public bool action = false;
	public bool mudAlarm = false;

	public float parentDistance;
	private Vector3 parentPosition;
	public Vector3 alarmPosition;

	public Vector3 mudPosition;

	public Vector3 enterPosition;
	public Vector3 exitPosition;

	private Transform basicTransform;

	// Use this for initialization
	void Start () {
		basicTransform = GetComponent<Transform> ();
		parentDistance = transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		if (other.tag == "Mud") {
			mudAlarm = true;

			enterPosition = transform.position;
			mudPosition = other.gameObject.transform.position;
		}
	}

	void OnTriggerStay(Collider other){
		Vector3 moveVec;

		moveVec = new Vector3 (0.1f, 0, 0);

		if (other.tag == "Mud") {
			transform.localPosition = transform.localPosition + moveVec;
		}
	}

	void OnTriggerExit(Collider other){

		if (other.tag == "Mud") {			
			exitPosition = transform.position;
			//Time.timeScale = 0;
			transform.localPosition = new Vector3(0,0,3);
			Debug.Log ("exit = " + exitPosition);

		}
	}
}
