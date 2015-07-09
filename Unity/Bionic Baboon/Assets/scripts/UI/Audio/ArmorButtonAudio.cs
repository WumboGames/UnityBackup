using UnityEngine;
using System.Collections;

public class ArmorButtonAudio : MonoBehaviour {

	private ItemButton button;
	private bool initialized;
	private AudioSource hoverNoise, clickNoise, wrongClickNoise;

	void Update() {
		if (!initialized)
			init();
	}

	private void init() {
		button=GetComponent<ItemButton>();
		hoverNoise=((GameObject)GameObject.Find("ButtonHoverSound")).GetComponent<AudioSource>();
		clickNoise=((GameObject)GameObject.Find("ButtonClickSound")).GetComponent<AudioSource>();
		wrongClickNoise=((GameObject)GameObject.Find("ButtonWrongClickSound")).GetComponent<AudioSource>();
		initialized=true; 
	}

	void OnMouseEnter() {
		hoverNoise.Play();
	}

	void OnMouseDown() {
		if (button.canBeClicked()) {
			clickNoise.Play();
		}
		else {
			wrongClickNoise.Play();
		}
	}

}
