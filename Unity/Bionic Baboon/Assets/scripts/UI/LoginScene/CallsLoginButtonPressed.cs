using UnityEngine;
using System.Collections;

public class CallsLoginButtonPressed : MonoBehaviour {
	private LoginController controller;

	void Start() {
		controller=GameObject.Find("GameController").GetComponent<LoginController>();
	}

	void OnMouseDown() {
		controller.onLoginClick();
	}
}
