using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;

public class ScoreBoard : MonoBehaviour {

	public Text highscoreMenu;

	private Text text;
	private float time;
	private int timeInt;
	public int highscore;
	private GameState gameState;


	private string readText;
	private string appendText;
	private string path = @"G:\Projektowanie Gier\2D Racing\Assets\Scoreboard\Scoreboard.txt";

	// Use this for initialization
	void Start () {	
		text = GetComponent<Text> ();
		gameState = GetComponentInParent<GameState> ();

		if (text != null) {
			text.text = " ";
		}

		readText = File.ReadAllText (path);
	}
	
	// Update is called once per frame
	void Update () {
		
		timeInt = (int)time;

		Int32.TryParse(readText, out highscore);

		if (text != null) {
			text.text = timeInt.ToString ();

			appendText = text.text;

			if (CheckpointController.currentLap == gameState.totalLaps + 1) {

				Debug.Log ("timeInt " + timeInt);
				Debug.Log ("highscore " + highscore);


				if (highscore > timeInt) {
					File.WriteAllText (path, String.Empty);
					File.AppendAllText (path, appendText);
					timeInt = highscore;
				}
			}
		}

		if (GameState.gameStarted && CheckpointController.currentLap < gameState.totalLaps + 1) {
			time += Time.deltaTime;
		}
	}
}


