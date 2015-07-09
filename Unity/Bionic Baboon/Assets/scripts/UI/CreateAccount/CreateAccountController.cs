using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateAccountController : MonoBehaviour {
	public Transform usernameTakenTransform, passwordsDoNotMatchTransform, createAccountTransform;
	public string username, password, verifyPassword; //these need to be set

	private Vector3 away=Vector3.up*9000000, usernameTakenPosition, passwordsDoNotMatchPosition, createAccountPosition;

	void Start() {
		usernameTakenPosition=usernameTakenTransform.position;
		passwordsDoNotMatchPosition=passwordsDoNotMatchTransform.position;
		createAccountPosition=createAccountTransform.position;

		passwordsDoNotMatchTransform.position=away;
		createAccountTransform.position=away;
	}

	void Update() {
		if (password.Equals(verifyPassword))
			passwordsDoNotMatchTransform.position=away;
		else
			passwordsDoNotMatchTransform.position=passwordsDoNotMatchPosition;

		if (username.Length==0||PlayerPrefs.HasKey("password for "+username))
			usernameTakenTransform.position=usernameTakenPosition;
		else
			usernameTakenTransform.position=away;

		if (passwordsDoNotMatchTransform.position==away&&usernameTakenTransform.position==away)
			createAccountTransform.position=createAccountPosition;
		else
			createAccountTransform.position=away;
	}

	public void createAccount() {
		PlayerPrefs.SetString(username+" password:", password);
		PlayerPrefs.SetInt(username+" players:", 0);
		PlayerPrefs.Save();
		Debug.Log("Account Created!");
		Application.LoadLevel("LoginScreen");
	}
}
