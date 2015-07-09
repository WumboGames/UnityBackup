using UnityEngine;
using System.Collections;

public class request : MonoBehaviour {
	private float speed=0.03f;
	private int targetX, targetY, finalX, finalY;
	private ArrayList targets=new ArrayList(), pointsChecked=new ArrayList();

	public void setUpValues(int targetX, int targetY) {
		finalX=targetX;
		finalY=targetY;
		this.targetX=(int)(transform.position.x+0.5f);
		this.targetY=(int)(transform.position.y+0.5f);
		if (!searchForPath((int)(transform.position.x+0.5f), (int)(transform.position.y+0.5f), "")) {
			Debug.Log("Dying");
			Destroy(gameObject);
			return;
		}
		GameObject.Find("GameController").GetComponent<BoardController>().requests.Add(gameObject);
	}

	public void simulationUpdate() {
		if (targetX-0.5f>transform.position.x) transform.position+= new Vector3(speed, 0, 0);
		if (targetX-0.5f<transform.position.x) transform.position-=new Vector3(speed, 0, 0);
		if (targetY-0.5f>transform.position.y) transform.position+=new Vector3(0, speed, 0);
		if (targetY-0.5f<transform.position.y) transform.position-=new Vector3(0, speed, 0);

		if (Mathf.Abs(transform.position.x-targetX+0.5f)<=speed&&Mathf.Abs(transform.position.y-targetY+0.5f)<=speed) {
			if (targets.Count==0) {
				if (GameObject.Find("GameController").GetComponent<BoardController>().getPiece(targetX, targetY)!=null)
					GameObject.Find("GameController").GetComponent<BoardController>().getPiece(targetX, targetY).GetComponent<GamePiece>().attackWithDDOS();
				GameObject.Find("GameController").GetComponent<BoardController>().requests.Remove(gameObject);
				Destroy(gameObject);
				return;
			}
			Vector2 newTarget=(Vector2)(targets[0]);
			targets.RemoveAt(0);
			targetX=(int)newTarget.x;
			targetY=(int)newTarget.y;
		}
	}

	private bool research(int testX, int testY, BoardController controller, string color) {
		GameObject piece=controller.getPiece(testX+1, testY);
		if (piece!=null&&piece.GetComponent<GamePiece>().canHoldRequests) 
			if (check (testX+1, testY, color)) return true;
		piece=controller.getPiece(testX-1, testY);
		if (piece!=null&&piece.GetComponent<GamePiece>().canHoldRequests) 
			if (check (testX-1, testY, color)) return true;
		piece=controller.getPiece(testX, testY+1);
		if (piece!=null&&piece.GetComponent<GamePiece>().canHoldRequests) 
			if (check (testX, testY+1, color)) return true;
		piece=controller.getPiece(testX, testY-1);
		if (piece!=null&&piece.GetComponent<GamePiece>().canHoldRequests) 
			if (check (testX, testY-1, color)) return true;
		
		piece=controller.getPiece(testX+1, testY+1);
		if (piece!=null&&(piece.GetComponent<GamePiece>().transportsRequestsDiagonallym()||controller.getPiece(testX, testY).GetComponent<GamePiece>().transportsRequestsDiagonallym()))
			if (check (testX+1, testY+1, color)) return true;
		piece=controller.getPiece(testX+1, testY-1);
		if (piece!=null&&(piece.GetComponent<GamePiece>().transportsRequestsDiagonallym()||controller.getPiece(testX, testY).GetComponent<GamePiece>().transportsRequestsDiagonallym()))
			if (check (testX+1, testY-1, color)) return true;
		piece=controller.getPiece(testX-1, testY+1);
		if (piece!=null&&(piece.GetComponent<GamePiece>().transportsRequestsDiagonallym()||controller.getPiece(testX, testY).GetComponent<GamePiece>().transportsRequestsDiagonallym()))
			if (check (testX-1, testY+1, color)) return true;
		piece=controller.getPiece(testX-1, testY-1);
		if (piece!=null&&(piece.GetComponent<GamePiece>().transportsRequestsDiagonallym()||controller.getPiece(testX, testY).GetComponent<GamePiece>().transportsRequestsDiagonallym()))
			if (check (testX-1, testY-1, color)) return true;

		for (int i=0; i<pointsChecked.Count; i++) {
			if (((Vector2)pointsChecked[i]).x==testX&&((Vector2)pointsChecked[i]).y==testY) {
				pointsChecked.RemoveAt(i);
				i--;
			}
		}
		
		return false;
	}
	private bool searchForPath(int testX, int testY, string color) {
		foreach (Vector2 testPoint in pointsChecked)
			if (testPoint.x==testX&&testPoint.y==testY) return false;//already tried here

		BoardController controller=GameObject.Find("GameController").GetComponent<BoardController>();
		GameObject piece=controller.getPiece(testX, testY);
		string color2=piece.GetComponent<GamePiece>().color;
		if (color==null||color.Equals("")) color=color2;
		if (!(color.Equals("both")||color2.Equals("both")||color.Equals(color2))) return false;

		if (testX==finalX&&testY==finalY) {
			targets.Add(new Vector2(testX, testY));
			searchForMacs(testX, testY, color2);
			return true;
		}
		if (!(piece!=null&&piece.GetComponent<GamePiece>().transportsRequestsNormallym())) 
			if (!(testX==transform.position.x+0.5f&&testY==transform.position.y+0.5f))return false;//I was a computer space...

		pointsChecked.Add(new Vector2(testX, testY));
		return research(testX, testY, controller, color2);
	}

	private void searchForMacs(int testX, int testY, string color) {
		BoardController controller=GameObject.Find("GameController").GetComponent<BoardController>();
		GameObject piece=controller.getPiece(testX+1, testY);
		if (piece!=null&&piece.GetComponent<GamePiece>().color.Equals(color)&&piece.GetComponent<GamePiece>().attractsRequests&&piece.GetComponent<GamePiece>().canTakeRequest()) {
			targets.Add(new Vector2(testX+1, testY));
			return;
		}
		else 
		piece=controller.getPiece(testX-1, testY);
		if (piece!=null&&piece.GetComponent<GamePiece>().color.Equals(color)&&piece.GetComponent<GamePiece>().attractsRequests&&piece.GetComponent<GamePiece>().canTakeRequest()) {
			targets.Add(new Vector2(testX-1, testY));
			return;
		}
		piece=controller.getPiece(testX, testY+1);
		if (piece!=null&&piece.GetComponent<GamePiece>().color.Equals(color)&&piece.GetComponent<GamePiece>().attractsRequests&&piece.GetComponent<GamePiece>().canTakeRequest()) {
			targets.Add(new Vector2(testX, testY+1));
			return;
		}
		piece=controller.getPiece(testX, testY-1);
		if (piece!=null&&piece.GetComponent<GamePiece>().color.Equals(color)&&piece.GetComponent<GamePiece>().attractsRequests&&piece.GetComponent<GamePiece>().canTakeRequest()) {
			targets.Add(new Vector2(testX, testY-1));
			return;
		}
	}

	private bool check(int testX, int testY, string color) {
		if (searchForPath(testX, testY, color)) {
			targets.Insert(0, new Vector2(testX, testY));
			return true;
		}
		return false;
	}

}
