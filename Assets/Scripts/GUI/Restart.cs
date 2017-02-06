using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Restart : MonoBehaviour {

	public Button restart;

	public void RestartButton(){
		if(Input.GetKeyDown(KeyCode.R)){
			SceneManager.LoadScene ("Scene1");
		}
	}
}
