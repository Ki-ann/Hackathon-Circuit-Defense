using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour {
	private float explosionRadius;
	public float ExplosionRadius {
		set { explosionRadius = value; }
	}

	void Update () {
		StartCoroutine (explosionHandler ());
	}
	IEnumerator explosionHandler () {
		int Scale = 1;
		gameObject.transform.localScale = new Vector3 (Scale, Scale, Scale);
		for (int i = 0; i < (int) explosionRadius; i++) {
			Scale++;
		}
		yield return new WaitForSeconds (0f);
		Destroy (gameObject, 0.5f);
	}
}