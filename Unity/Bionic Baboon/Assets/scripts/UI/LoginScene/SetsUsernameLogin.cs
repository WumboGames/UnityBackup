using UnityEngine;
using System.Collections;

public class SetsUsernameLogin : MonoBehaviour {

	private LoginController controller;

	void Start() {
		controller=GameObject.Find("GameController").GetComponent<LoginController>();
	}

	public void onEdit(string newUsername) {
		controller.username=newUsername;
	}
}
