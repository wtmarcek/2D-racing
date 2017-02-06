using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Menu : MonoBehaviour {

	private bool turnOn;

	public Canvas menu;
	public GameObject hud;
	public GameObject mainMenu;
	public GameObject tutorialMenu;
	public Button start;
	public Button exit;
	private Renderer hudRenderer;
	public Text highscoreTxt;
	public ScoreBoard scoreboard;

	//Audio
	public AudioSource audioSource;
	private AudioClip naglowek;



	// Use this for initialization
	void Start () {
		menu = gameObject.GetComponent<Canvas> ();
		hudRenderer = hud.GetComponent<Renderer> ();
		naglowek = audioSource.GetComponent<AudioClip> ();

		Time.timeScale = 0;
		//hudRenderer.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
		MenuEnable ();
		Highscore ();
	}

	void MenuEnable(){

		if (Input.GetKeyUp (KeyCode.Escape)) {

			menu.enabled = !menu.enabled;

			Cursor.visible = menu.enabled;


			if (menu.enabled) {
				//hudRenderer.enabled = false;
				Cursor.lockState = CursorLockMode.Confined;
				Time.timeScale = 0;
				Cursor.visible = true;
				//Audio
				audioSource.Play();
				audioSource.mute = false;
			} else {

				//hudRenderer.enabled = true;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
				Time.timeScale = 1;
				//Audio
				audioSource.mute = true;
			}
		}
	}

	public void TutorialButton(){
		mainMenu.SetActive (false);
		tutorialMenu.SetActive (true);
	}
	public void BackButton(){
		mainMenu.SetActive (true);
		tutorialMenu.SetActive (false);
	}

	public void StartButton(){

		menu.enabled = false;
		exit.enabled = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1;
		//Audio
		audioSource.mute = true;
	}

	void Highscore(){

		highscoreTxt.text = "Highscore: " + scoreboard.highscore;
	}
}
