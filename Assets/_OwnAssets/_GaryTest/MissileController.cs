using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissileController : MonoBehaviour {
	[SerializeField] private float timeTillExplode;
	[SerializeField] private float explosionRadius;
	private Turret turret;
	public Turret m_Turret {
		set { turret = value; }
	}

	void Update () {
		Countdown ();
	}

	void OnCollisionEnter (Collision other) {
		Explode ();
	}

	void Explode () {
		CheckForSurroundingEnemies ();
		GameObject explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		explosion.transform.position = transform.position;
		
		Destroy (gameObject, 0f);
	}

	// IEnumerator explosionHandler(GameObject explosion) {
	// 	explosion.transform.localScale
	// }	

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