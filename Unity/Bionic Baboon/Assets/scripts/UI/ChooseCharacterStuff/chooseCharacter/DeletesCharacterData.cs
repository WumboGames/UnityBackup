using UnityEngine;
using System.Collections;

public class DeletesCharacterData : MonoBehaviour {
	private int playerIndex;
	private ChooseCharacterController controller;
	
	public void setPlayerIndex(int playerIndex) {
		this.playerIndex=playerIndex;
		controller=GameObject.Find("GameController").GetComponent<ChooseCharacterController>();
	}
	
	void OnMouseDown() {
		controller.deleteCharacter(playerIndex);
	}
}
