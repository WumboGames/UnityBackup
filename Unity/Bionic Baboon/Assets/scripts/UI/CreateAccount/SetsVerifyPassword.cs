using UnityEngine;
using System.Collections;

public class SetsVerifyPassword : MonoBehaviour {

	private CreateAccountController controller;

	void Start() {
		controller=GameObject.Find("GameController").GetComponent<CreateAccountController>();
	}

	public void onEdit(string newText) {
		controller.verifyPassword=newText;
	}
}
