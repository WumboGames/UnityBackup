using UnityEngine;
using System.Collections;

public class ShowsCharacterOnMouseOver : MonoBehaviour {
	
	public SpriteRenderer deleteSpriteRenderer;
	public Sprite deleteSprite;
	private DeletesCharacterData deleteButton;
	
	private ChooseCharacterController controller;
	private int playerIndex;
	
	private bool initialized=false;
	
	
	void Start() {
		init ();
	}
	
	private void init() {
		if (initialized)
			return;
		controller=GameObject.Find("GameController").GetComponent<ChooseCharacterController>();
		deleteButton=deleteSpriteRenderer.GetComponent<DeletesCharacterData>();
	}
	
	public void setPlayerIndex(int playerIndex) {
		init();
		this.playerIndex=playerIndex;
		deleteButton.setPlayerIndex(playerIndex);
	}
	
	void OnMouseEnter() {
		controller.showCharacter(playerIndex);
	}
	
	void OnMouseDown() {
		controller.characterClicked(playerIndex);
	}
	
	public void showDeleteButton() {
		deleteSpriteRenderer.sprite=deleteSprite;
	}
	
	public void hideDeleteButton() {
		deleteSpriteRenderer.sprite=null;
	}
}
