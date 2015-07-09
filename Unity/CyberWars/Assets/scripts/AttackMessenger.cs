using UnityEngine;
using System.Collections;

public class AttackMessenger : MonoBehaviour {

	public string attackType;
	private string attackerColor;
	private int targetX, targetY;
	private float speed=0.05f;

	void Start() {
		GameObject.Find("GameController").GetComponent<BoardController>().attackingPieces.Add(gameObject);
	}

	public void setValues(int targetX, int targetY, string attackerColor) {
		this.targetX=targetX;
		this.targetY=targetY;
		this.attackerColor=attackerColor;
		if (attackType.Equals("virus")) {
			speed=0.02f;
		}
	}

	public void simulationUpdate() {
		if (targetX-0.5f>transform.position.x) transform.position+= new Vector3(speed, 0, 0);
		if (targetX-0.5f<transform.position.x) transform.position-=new Vector3(speed, 0, 0);
		if (targetY-0.5f>transform.position.y) transform.position+=new Vector3(0, speed, 0);
		if (targetY-0.5f<transform.position.y) transform.position-=new Vector3(0, speed, 0);

		if (attackType.Equals("virus")) {
			if (Mathf.Abs(transform.position.x-targetX+0.5f)<=speed&&Mathf.Abs(transform.position.y-targetY+0.5f)<=speed) {
				if (GameObject.Find("GameController").GetComponent<BoardController>().getPiece(targetX, targetY)!=null) {
					GameObject.Find("GameController").GetComponent<BoardController>().getPiece(targetX, targetY).GetComponent<GamePiece>().attackWithVirus();
				}
				GameObject.Find("GameController").GetComponent<BoardController>().attackingPieces.Remove(gameObject);
				Destroy(gameObject);
			}
		}
		else {//attackType=="malware"
			if (Mathf.Abs(transform.position.x-targetX+0.5f)<=speed&&Mathf.Abs(transform.position.y-targetY+0.5f)<=speed) {
				if (GameObject.Find("GameController").GetComponent<BoardController>().getPiece(targetX, targetY)!=null) {
					GameObject.Find("GameController").GetComponent<BoardController>().getPiece(targetX, targetY).GetComponent<GamePiece>().attackWithMalware(attackerColor);
				}
				GameObject.Find("GameController").GetComponent<BoardController>().attackingPieces.Remove(gameObject);
				Destroy(gameObject);
			}
		}
	}

}
