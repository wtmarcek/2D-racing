using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour {

	private Transform playerTransform;
	private Transform enemyTransform;

	private CheckpointController checkpointController;


	// Use this for initialization
	void Start () {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		checkpointController = playerTransform.GetComponent<CheckpointController> ();

		enemyTransform = GameObject.FindGameObjectWithTag ("Enemy").transform;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other){

		if (!other.CompareTag ("Player") /*|| !other.CompareTag("Enemy")*/) {
			return;
		}

		//Debug.Log (playerTransform.GetComponent<CheckpointController> ().checkpointArray.Length);

		if (transform.position == checkpointController.checkpointArray [CheckpointController.currentCheckpoint].transform.position) {
			Debug.Log ("We are at checkpoint = " + CheckpointController.currentCheckpoint);

			if (CheckpointController.currentCheckpoint + 1 < playerTransform.GetComponent<CheckpointController> ().checkpointArray.Length) {

				if (CheckpointController.currentCheckpoint == 0) {

					CheckpointController.currentLap++;
					Debug.Log ("We are at LAP: " + CheckpointController.currentLap);
				} 

				CheckpointController.currentCheckpoint++;

			} else {

				CheckpointController.currentCheckpoint = 0;
			}
		}
	}
}