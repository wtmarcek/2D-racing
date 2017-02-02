using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

	private Transform trans;
	private Transform transPlayer;
	private GameObject playerObject;
	private DamageController damageController;

	// Use this for initialization
	void Start () {
		trans = GetComponent<Transform> ();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		transPlayer = playerObject.GetComponent<Transform> ();
		damageController = playerObject.GetComponent<DamageController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (damageController.health == 0) {
			transPlayer = trans;
		}
	}
}
