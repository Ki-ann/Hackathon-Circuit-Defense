using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour, ITakeDamage {
	[SerializeField] private float maxHealth;
	private float currentHealth;
	public bool GameRun = true;

	public void TakeDamage (float amount) {
		currentHealth -= maxHealth;
		if (currentHealth <= 0) {
			GameOver ();
		}
	}

	void GameOver () {
		//reset
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Debug.Log ("You lose");
		GameRun = false;
		gameObject.SetActive(false);
	}
}