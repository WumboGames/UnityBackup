using UnityEngine;
using System.Collections;

public class NormalButtonAudio : MonoBehaviour {

	public bool createsMotion=false;
	private AudioSource buttonClickNoiseMaker, buttonHoverNoiseMaker;

	void Start() {
		buttonHoverNoiseMaker=GameObject.Find("ButtonHoverSound").GetComponent<AudioSource>();
		if (!createsMotion)
			buttonClickNoiseMaker=GameObject.Find("ButtonClickSound").GetComponent<AudioSource>();
		else
			buttonClickNoiseMaker=GameObject.Find("ButtonMovementSound").GetComponent<AudioSource>();
	}

	void OnMouseEnter() {
		buttonHoverNoiseMaker.Play();
	}

	void OnMouseDown() {
		buttonClickNoiseMaker.Play();
	}
}
