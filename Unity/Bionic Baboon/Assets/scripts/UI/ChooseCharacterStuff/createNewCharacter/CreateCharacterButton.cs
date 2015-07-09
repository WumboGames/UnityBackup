using UnityEngine;
using System.Collections;

public class CreateCharacterButton : MonoBehaviour {
	private CreateCharacterController controller;
	
	private void Start() {
		controller=GameObject.Find("GameController").GetComponent<CreateCharacterController>();
	}
	
	void OnMouseDown() {
		Debug.Log("HERE");
		controller.createCharacterButtonPressed();
	}
}
