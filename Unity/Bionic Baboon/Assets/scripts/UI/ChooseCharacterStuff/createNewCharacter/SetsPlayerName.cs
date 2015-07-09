using UnityEngine;
using System.Collections;

public class SetsPlayerName : MonoBehaviour {
	private CreateCharacterController controller;
	
	private void Start() {
		controller=GameObject.Find("GameController").GetComponent<CreateCharacterController>();
	}
	
	public void onEdit(string newText) {
		controller.playerName=newText;
	}
}
