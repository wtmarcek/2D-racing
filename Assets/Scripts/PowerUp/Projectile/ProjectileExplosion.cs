using UnityEngine;
using System.Collections;

public class ProjectileExplosion : TexturesManager {

	private int projectileFrame	= 48;
	private int explosionMin 	= 34; 
	private int explosionMax	= 40;

	private int currentAnim 	= 0;
	private int projectileAnim  = 1;
	private int explosionAnim	= 2;

	private float animTime;

	private Projectile projectile;

	// Use this for initialization
	void Start () {

		rend = GetComponent<Renderer> ();
		projectile = GetComponentInParent<Projectile> ();
		currentFrame = projectileFrame;

	}
	
	// Update is called once per frame
	void Update () {

		FindAnimation ();
		Explosion ();
		HandleMaterial ();
	}

	void FindAnimation(){

		if (projectile.explode) {

			currentAnim = explosionAnim;
			if (currentFrame != Mathf.Clamp (currentFrame, explosionMin, explosionMax + 1)) {
				currentFrame = explosionMin;
			}
		}
	}

	void Explosion () {
		if (currentAnim == explosionAnim) {

			animTime -= Time.deltaTime;

			if (animTime <= 0) {
				currentFrame += 1;
				animTime += 1.0f / 10.0f;
			}
			currentFrame = Mathf.Clamp (currentFrame, explosionMin, explosionMax + 1);

			if (currentFrame > explosionMax) {

				Destroy (projectile.gameObject);
			}
		}
	}
}
