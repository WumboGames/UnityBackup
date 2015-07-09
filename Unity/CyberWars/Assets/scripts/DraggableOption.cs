using UnityEngine;
using System;
using System.Collections;

public class DraggableOption : MonoBehaviour {

	private Vector3 screenPoint, offset;
	private GameObject owner;
	private bool beingDragged=false;

	public string attackType;
	
	public void declareAsOwner(GameObject owner) {
		this.owner=owner;
	}

	void OnMouseDown() {
		beingDragged=true;
		screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		offset = gameObject.transform.position - 
			Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag() {
		Vector3 currentScreenPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		transform.position = Camera.main.ScreenToWorldPoint (currentScreenPosition)+offset;
	}

	void OnMouseUp() {
		if (!beingDragged) return;
		owner.GetComponent<HasSelectionItems>().mouseExited();
		int x=(int)((transform.position.x+1000)+1.0)-1000; //needs to be positive for rounding as an int
		int y=(int)((transform.position.y+1000)+1.0)-1000;
		try {
			if (attackType=="DDOS"&&!GameObject.Find("GameController").GetComponent <BoardController>().getPiece(x, y).GetComponent<GamePiece>().attackableByDDOS) return;
			if (attackType=="virus"&&!GameObject.Find("GameController").GetComponent <BoardController>().getPiece(x, y).GetComponent<GamePiece>().attackableByVirus) return;
			if (attackType=="malware"&&!GameObject.Find("GameController").GetComponent <BoardController>().getPiece(x, y).GetComponent<GamePiece>().attackableByMalware) return;
			if (GameObject.Find("GameController").GetComponent <BoardController>().getPiece(x, y).GetComponent<GamePiece>().color.Equals(owner.GetComponent<GamePiece>().color)) return;
		}catch (Exception e){
			if (e!=null)
				Debug.Log("Invalid Move");
			return;//invalid choice
		}
		owner.GetComponent<HasSelectionItems>().setAttack(attackType, x, y);
	}
	
}
