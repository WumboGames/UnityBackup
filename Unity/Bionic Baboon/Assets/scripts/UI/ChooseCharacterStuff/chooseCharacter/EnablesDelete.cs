using UnityEngine;
using System.Collections;

public class EnablesDelete : MonoBehaviour {

	private TextMesh myTextMesh;
	private ChooseCharacterController controller;
	
	void Start() {
		controller=GameObject.Find("GameController").GetComponent<ChooseCharacterController>();
		myTextMesh=GetComponentInChildren<TextMesh>();
		myTextMesh.text="Delete";
	}
	
	void OnMouseDown() {
		if (myTextMesh.text.Equals("Delete"))
			myTextMesh.text="Cancel";
		else 
			myTextMesh.text="Delete";
		
		controller.deleteButtonClicked();
	}
}
