using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour {
	private float explosionRadius;
	public float ExplosionRadius {
		set { explosionRadius = value; }
	}
	private AudioSource audioToPlay;

	void Start() {
		audioToPlay = GetComponent<AudioSource>();
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
		if (!audioToPlay.isPlaying)
			audioToPlay.Play();
		yield return new WaitForSeconds (audioToPlay.clip.length);
		Destroy (gameObject);
	}
}