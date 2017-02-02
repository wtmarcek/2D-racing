using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Speed : MonoBehaviour {

	private Text speedTxt;
	private GameObject playerObject;
	private PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
		speedTxt = GetComponent<Text> ();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		playerMovement = playerObject.GetComponent<PlayerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		int currentSpeed;
		currentSpeed = (int)playerMovement.currentSpeed * 10;
		string speed = currentSpeed.ToString();
		speedTxt.text = speed;

	}
}
