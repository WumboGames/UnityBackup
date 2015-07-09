using UnityEngine;
using System.Collections;

public class SetsPasswordLogin : MonoBehaviour {

	private LoginController controller;

	void Start() {
		controller=GameObject.Find("GameController").GetComponent<LoginController>();
	}

	public void onEdit(string newPassword) {
		controller.password=newPassword;
	}
}
