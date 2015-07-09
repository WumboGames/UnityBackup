using UnityEngine;
using System.Collections;

public class SwitchScenesOnClick : MonoBehaviour {

	void OnMouseDown() {
		Application.LoadLevel("CharacterControllerTest");	
	}
	
}
