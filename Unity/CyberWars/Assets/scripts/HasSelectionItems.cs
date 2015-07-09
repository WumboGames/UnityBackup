using UnityEngine;
using System.Collections;

public class HasSelectionItems : MonoBehaviour {
	

	public GameObject malwareIconStorage, virusIconStorage, DDOSIconStorage, Hightlight;
	private GameObject malwareIcon, virusIcon, DDOSIcon, highlight;

	private BoardController controller;
	

	void Update() {
		if (controller==null) controller=GameObject.Find("GameController").GetComponent<BoardController>();
		if (controller.selected!=gameObject||controller.getMovesLeft()==0||controller.getPlayerTurn()!=getPlayer()) mouseExited();
		if (gameObject.GetComponent<GamePiece>().getVirusSymbol()!=null) mouseExited();
	}

	void OnMouseDown() {
		controller.selected=gameObject;
		if (malwareIcon == null)
			malwareIcon = (GameObject)Instantiate (malwareIconStorage, transform.position+new Vector3(-0.25f, -0.25f, -1f), Quaternion.identity);
		if (virusIcon == null)
			virusIcon = (GameObject)Instantiate (virusIconStorage, transform.position+new Vector3(0.25f, -0.25f, -1f), Quaternion.identity);
		if (DDOSIcon == null)
			DDOSIcon = (GameObject)Instantiate (DDOSIconStorage, transform.position+new Vector3(0f, 0.25f, -1f), Quaternion.identity);
		if (highlight == null&&gameObject.GetComponent<GamePiece>().targetAttack=="DDOS") {
			float targetX=gameObject.GetComponent<GamePiece>().targetX-0.5f;
			float targetY=gameObject.GetComponent<GamePiece>().targetY-0.5f;
			highlight = (GameObject)Instantiate (Hightlight, new Vector3(targetX, targetY, -1f), Quaternion.identity);
		}
		malwareIcon.GetComponent<DraggableOption>().declareAsOwner(gameObject);
		virusIcon.GetComponent<DraggableOption>().declareAsOwner(gameObject);
		DDOSIcon.GetComponent<DraggableOption>().declareAsOwner(gameObject);
	}

	public void mouseExited() {
		if (malwareIcon != null)
			Destroy (malwareIcon);
		if (virusIcon != null)
			Destroy (virusIcon);
		if (DDOSIcon != null)
			Destroy (DDOSIcon);
		if (highlight!=null)
			Destroy(highlight);
	}

	public void setAttack(string attack, int x, int y) {
		gameObject.GetComponent<GamePiece>().setTarget(attack, x, y);
		mouseExited();
		GameObject.Find("GameController").GetComponent<BoardController>().countMove();
	}

	private int getPlayer() {
		string color=gameObject.GetComponent<GamePiece>().color;
		switch(color) {
		case "red": 
			return 1;
		case "blue": 
			return 2;
		case "gold": 
			return 3;
		case "green": 
			return 4;
		}
		return -1;
	}

}
