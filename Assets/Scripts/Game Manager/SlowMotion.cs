using UnityEngine;
using System.Collections;

public class SlowMotion : MonoBehaviour {

	public float timeModerator =1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	

		Time.timeScale = timeModerator;
	}

}
