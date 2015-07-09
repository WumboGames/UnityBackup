using UnityEngine;
using System.Collections;

public class LoadLevelOnClick : MonoBehaviour {
	public string levelToLoad="TwoPlayerFair";

	void OnMouseUp() {
		Application.LoadLevel(levelToLoad);
	}
}
