using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UICanvas : MonoBehaviour {

	public Button restart;
	private Canvas uiCanvas;

	// Use this for initialization
	void Awake () {
		uiCanvas = GetComponent<Canvas> ();
	
		uiCanvas.enabled = true;
	}

}
