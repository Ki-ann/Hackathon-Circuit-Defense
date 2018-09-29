using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissileController : MonoBehaviour {
	[SerializeField] private float timeTillExplode;
	[SerializeField] private float explosionRadius;
	private bool isExploded = false;
	private Turret turret;
	public Turret m_Turret {
		set { turret = value; }
	}

	void Update () {
		if (!isExploded)
			Countdown ();
	}

	void OnCollisionEnter (Collision other) {
		Explode ();
	}

	void Explode () {
		isExploded = true;
		CheckForSurroundingEnemies ();
		Destroy (gameObject, 0.1f);
	}

	void OnDisable () {
		GameObject explosion = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		explosion.transform.position = transform.position;
		explosion.GetComponent<Collider>().enabled = false;
		ExplosionHandler explosionHandler = explosion.AddComponent<ExplosionHandler>();
		explosionHandler.ExplosionRadius = explosionRadius;
	}

	void CheckForSurroundingEnemies () {
		Collider[] surroundingColliders = Physics.OverlapSphere (transform.position, explosionRadius);
		foreach (Collider col in surroundingColliders) {
			EnemyAI enemy = col.GetComponent<EnemyAI> ();
			if (enemy != null) {
				enemy.TakeDamage (turret.AttackDamage);
			}
		}
	}

	void Countdown () {
		timeTillExplode -= Time.deltaTime;

		if (timeTillExplode <= 0) {
			Explode ();
		}
	}
}