using UnityEngine;
using System.Collections;

public class CallsCreateAccount : MonoBehaviour {

	private CreateAccountController controller;

	void Start() {
		controller=GameObject.Find("GameController").GetComponent<CreateAccountController>();
	}

	void OnMouseDown() {
		controller.createAccount();
	}
}
