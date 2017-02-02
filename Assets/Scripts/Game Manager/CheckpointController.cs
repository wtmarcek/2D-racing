using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour {

	public Transform[] checkpointArray;
	public static int currentCheckpoint	 = 0;
	public static int currentLap 		 = 0;
	public static Vector3 startPosition;

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (currentLap);
	}
}
