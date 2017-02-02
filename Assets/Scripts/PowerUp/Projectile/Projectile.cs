using UnityEngine;
using System.Collections;

public class Projectile : TexturesManager {

	public float lifeSpan 		= 8.0f;
	public int damage   		= 1;

	public bool explode 		= false;

	private PlayerProperties playerProperties;
	public Transform explosionTrans;

	// Use this for initialization
	void Start () {
		playerProperties = GetComponent<PlayerProperties> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (explosionTrans != null) {

			transform.position = explosionTrans.position;
		}

	}

	void OnTriggerEnter(Collider other){

		Collider collider = other.GetComponent<Collider> ();


		if (other.tag == "Obstacle" || other.tag == "Player" || other.tag == "Enemy") {
			explosionTrans = other.gameObject.GetComponent<Transform> ();

			explode = true;

			if (other.tag == "Enemy" || other.tag == "Player") {
				collider.SendMessage ("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
			}
		}



	}


}

