using UnityEngine;
using System.Collections;

public class OpenSceneOnClick : MonoBehaviour {

	public string sceneToOpen="";

	void OnMouseDown() {
		PlayerPrefs.Save();
		Application.LoadLevel(sceneToOpen);
	}
}
