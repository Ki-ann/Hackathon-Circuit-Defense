using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour {
	[SerializeField] private float timeTillExplode;
	void Update () {
		Countdown ();
	}

	void OnCollisionEnter (Collision other) {
		Explode ();
	}

	void Explode () {
		Debug.Log ("Boom");
		Destroy (gameObject, 0f);
	}

	void Countdown () {
		timeTillExplode -= Time.deltaTime;

		if (timeTillExplode <= 0) {
			Explode();
		}
	}
}