using UnityEngine;
using System.Collections;

public class Boost : MonoBehaviour {

	public float lifeSpan = 3.0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifeSpan);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
