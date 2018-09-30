using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundPlay : MonoBehaviour {
	private AudioSource buttonPress;

	void Start() {
		buttonPress = GetComponent<AudioSource>();
	}

	public void PlayButtonPressSound() {
		if (buttonPress.isPlaying){
			buttonPress.Stop();
			buttonPress.Play();
		}

		if (!buttonPress.isPlaying) 
			buttonPress.Play();
	}
}
