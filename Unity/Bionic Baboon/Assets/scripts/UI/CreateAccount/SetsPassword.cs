using UnityEngine;
using System.Collections;

public class SetsPassword : MonoBehaviour {

	private CreateAccountController controller;

	void Start() {
		controller=GameObject.Find("GameController").GetComponent<CreateAccountController>();
	}

	public void onEdit(string newText) {
		controller.password=newText;
	}
}
