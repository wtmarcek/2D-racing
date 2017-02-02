using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Items : MonoBehaviour {

	public Sprite none;
	public Sprite projectile;
	public Sprite trap;
	public Sprite boost;

	private Image itemImage;
	private GameObject playerObject;
	private PlayerProperties playerProperties;

	// Use this for initialization
	void Start () {
		itemImage = GetComponent<Image> ();
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		playerProperties = playerObject.GetComponent<PlayerProperties> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (playerProperties.canPickUp) {
			itemImage.sprite = none;
		}
		if (playerProperties.hasBoost) {
			itemImage.sprite = boost;
		}
		if (playerProperties.hasTrap) {
			itemImage.sprite = trap;
		}
		if (playerProperties.hasProjectile) {
			itemImage.sprite = projectile;
		}
	}
}
