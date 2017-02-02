using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UICanvas : MonoBehaviour {

	private Canvas uiCanvas;

	// Use this for initialization
	void Awake () {
		uiCanvas = GetComponent<Canvas> ();
	
		uiCanvas.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
