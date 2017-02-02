using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

	public Sprite health_100;
	public Sprite health_75;
	public Sprite health_50;
	public Sprite health_25;
	public Sprite health_00;

	public int healthUI = 4;
	private Image img;
	private GameObject playerObject;
	private DamageController damageController;

	// Use this for initialization
	void Start () {		
		playerObject = GameObject.FindGameObjectWithTag ("Player");	
		damageController = playerObject.GetComponent<DamageController> ();
		img = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {

		healthUI = damageController.health;

		switch (healthUI) {

		case 4:
			img.sprite = health_100;
		break;

		case 3:
			img.sprite = health_75;
		break;

		case 2:
			img.sprite = health_50;
		break;

		case 1:
			img.sprite = health_25;
		break;

		case 0:
			img.sprite = health_00;
		break;
		}
	
	}


}
