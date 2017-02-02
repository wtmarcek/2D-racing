using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

	public int damage = 1;

	public float torque;
	private Vector3 torqueVec;

	private GameObject playerGO;
	private GameObject enemyGO;

	// Use this for initialization
	void Start () {
		playerGO = GameObject.FindGameObjectWithTag ("Player");
		enemyGO = GameObject.FindGameObjectWithTag ("Enemy");

	}
	
	// Update is called once per frame
	void Update () {
	
		torqueVec = new Vector3 (0.0f, torque, 0.0f);
	}

	void OnTriggerEnter(Collider other){
		Collider collider = other.GetComponent<Collider> ();
		Rigidbody rigidbody;
		rigidbody = other.gameObject.GetComponent<Rigidbody> ();



		if (other.tag == "Enemy" || other.tag == "Player") {

			rigidbody.AddTorque(torqueVec, ForceMode.Impulse);
			collider.SendMessage ("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);

			if (other.tag == "Player") {
				DamageController damageController = playerGO.GetComponent<DamageController>();

				if (damageController.health > 0) {
					SpriteAnimation.spin = true;
				}
			}

			if (other.tag == "Enemy") {
				DamageController damageController = enemyGO.GetComponent<DamageController>();

				if (damageController.health > 0) {
					//EnemyAnimation.spin = true;
				}
			}
			Destroy (gameObject);
		}
	}

}



