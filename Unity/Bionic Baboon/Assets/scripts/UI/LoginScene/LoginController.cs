using UnityEngine;
using System.Collections;

public class LoginController : MonoBehaviour {

	public Transform passwordTransform;
	public Transform loginTransform, noUsernameTransform, wrongPasswordTransform;
	public string username, password;

	private Vector3 away=Vector3.up*900000000, passwordPosition, loginPosition, noUsernamePosition, wrongPasswordPosition;
	private Vector3 left=new Vector3(-11, 0, 0), down=new Vector3(4.22f, -6, 0);

	void Start() {
		passwordPosition=passwordTransform.position;
		loginPosition=loginTransform.position;
		noUsernamePosition=noUsernameTransform.position;
		wrongPasswordPosition=wrongPasswordTransform.position;


		passwordTransform.position=left;
		loginTransform.position=down;
		noUsernameTransform.position=away;
		wrongPasswordTransform.position=away;
	}

	void FixedUpdate() {
		Vector3 targetPosition=Vector3.zero;
		if (PlayerPrefs.GetString(username+" password:", "").Equals(""))//either not entered or no password
			targetPosition=left;
		else
			targetPosition=passwordPosition;
		passwordTransform.position=lerp(passwordTransform.position, targetPosition, 0.2f);

		if (username.Equals(""))
			targetPosition=down;
		else
			targetPosition=loginPosition;
		loginTransform.position=lerp(loginTransform.position, targetPosition, 0.2f);
	}

	private Vector3 lerp(Vector3 from, Vector3 to, float time) {
		float x=Mathf.Lerp(from.x, to.x, time);
		float y=Mathf.Lerp(from.y, to.y, time);
		float z=Mathf.Lerp(from.z, to.z, time);
		return new Vector3(x, y, z);
	}

	public void onLoginClick() {
		if (PlayerPrefs.HasKey(username+" password:")&&PlayerPrefs.GetString(username+" password:").Equals(password)) {
			noUsernameTransform.position=away;
			wrongPasswordTransform.position=away;
			Debug.Log("Matching Username and Password.");
			PlayerPrefs.SetString("current username", username);
			Application.LoadLevel("ChooseCharacterScreen");
		}
		else {
			if (PlayerPrefs.HasKey(username+" password:")) {
				noUsernameTransform.position=away;
				wrongPasswordTransform.position=wrongPasswordPosition;
			}
			else {
				noUsernameTransform.position=noUsernamePosition;
				wrongPasswordTransform.position=away;
			}
		}
	}
}
