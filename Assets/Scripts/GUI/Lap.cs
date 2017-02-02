using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Lap : MonoBehaviour {

	private Text lapTxt;

	// Use this for initialization
	void Start () {
		lapTxt = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {

		string lap = CheckpointController.currentLap.ToString();
		lapTxt.text = lap;
	}
}
