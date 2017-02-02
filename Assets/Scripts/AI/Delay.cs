using UnityEngine;
using System.Collections;

public class Delay : MonoBehaviour {

	public float delayTimer;
	private float resetDelayTimer;

	private EnemyMovement enemyMovement;

	// Use this for initialization
	void Start () {
		enemyMovement = GetComponent<EnemyMovement> ();

		enemyMovement.aiSpeed = enemyMovement.aiSpeed * enemyMovement.aiSpeedModerator;

		resetDelayTimer = delayTimer;
	}
	
	// Update is called once per frame
	void Update () {
		delayTimer -= Time.deltaTime;

		if (delayTimer < 0.0f) {
			enemyMovement.aiSpeed = enemyMovement.resetAiSpeed;
		}
	}
}
