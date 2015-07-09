using UnityEngine;
using System.Collections;

public class Draggable : MonoBehaviour {
	private Vector3 screenPoint, offset, originalPosition;
	private int cost;
	private bool initiatedPoints=false;
	private int minXSquare, minYSquare, maxXSquare, maxYSquare;
	private DrawsDaysLeftAndPlayerMoney drawer;

	public GameObject gamePieceToCreate;
	public int player;

	void Start() {
		originalPosition = transform.position;
		cost = gameObject.GetComponent <DrawsText>().cost;
	}

	void Update() {
		if (!initiatedPoints) {
			initiatedPoints=true;
			BoardController controller = GameObject.Find ("GameController").GetComponent<BoardController>();
			drawer=GameObject.Find("GameController").GetComponent<DrawsDaysLeftAndPlayerMoney>();
			minXSquare = controller.minXSquare;
			minYSquare = controller.minYSquare;
			maxXSquare = controller.maxXSquare;
			maxYSquare = controller.maxYSquare;
		}
	}

	void OnMouseDown() {
		if (!isMyTurn()) return;
		screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
		offset = gameObject.transform.position - 
			Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag() {
		if (!isMyTurn()) return;
		Vector3 currentScreenPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		transform.position = Camera.main.ScreenToWorldPoint (currentScreenPosition)+offset;
	}

	void OnMouseUp() {//Make the only valid snap-spaces the ones in the square
	
		if (!isMyTurn()) return;
		Vector3 newPosition = new Vector3 ((int)(transform.position.x+0.01)+Mathf.Sign(transform.position.x)*0.5f,
		                                   (int)(transform.position.y+0.01)+Mathf.Sign(transform.position.y)*0.5f, 
		                                   transform.position.z);
		transform.position=originalPosition;

		int pieceX = (int)(newPosition.x + 0.5f);
		int pieceY = (int)(newPosition.y + 0.5f);

		if (!(Mathf.Min (Mathf.Max (newPosition.x, minXSquare), maxXSquare) == newPosition.x &&
		      Mathf.Min (Mathf.Max (newPosition.y, minYSquare), maxYSquare) == newPosition.y)) {
			GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
			return;
		}

		BoardController controller=GameObject.Find ("GameController").GetComponent <BoardController> (); 
		if (gamePieceToCreate!=null) {
			if (controller.getPiece (pieceX, pieceY) != null){//there is a piece on my space
				GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
				return;
			}
			if (!isLegalSpot(pieceX, pieceY)) {
				GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
				return;
			}
		}
		else {
			if (controller.getPiece(pieceX, pieceY)==null){
				GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
				return;
			}//can't dig something that isn't there
			if (!isLegalName(controller.getPiece(pieceX, pieceY).GetComponent<GamePiece>().color, controller.getPiece(pieceX, pieceY))){
				GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
				return;
			}
			if (controller.getPiece (pieceX, pieceY).GetComponent<CapitolGamePiece>()!=null){
				GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
				return;
			}//you can't destroy your capitol
		}

		if (!canSubtractCost ()) {
			GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
			return;
		}
		if (!isMyTurn ()) {
			GameObject.Find("WrongSound").GetComponent<AudioSource>().Play();
			return;
		}

		GameObject.Find("PlaceObjectSound").GetComponent<AudioSource>().Play();
		countMove();
		transform.position = originalPosition;//place the piece
		if (gamePieceToCreate!=null) 
			controller.createPiece (pieceX, pieceY,(GameObject)Instantiate (gamePieceToCreate, newPosition, Quaternion.identity));
		else {
			Destroy(controller.getPiece(pieceX, pieceY));
			controller.createPiece (pieceX, pieceY, null);
		}
	}

	private bool canSubtractCost() {
		if (player == 1 && drawer.player1Money < cost)
			return false;
		if (player == 2 && drawer.player2Money < cost)
			return false;
		if (player == 3 && drawer.player3Money < cost)
			return false;
		if (player == 4 && drawer.player4Money < cost)
			return false;

		return true;
	}

	private bool isMyTurn() {
		return (drawer.playersTurn == player && drawer.movesLeft > 0);
	}

	private void countMove() {
		if (player == 1)
			drawer.player1Money -= cost;
		if (player == 2) 
			drawer.player2Money -= cost;
		if (player == 3) 
			drawer.player3Money -= cost;
		if (player == 4) 
			drawer.player4Money -= cost;
			
		drawer.movesLeft--;
	}

	private bool isLegalSpot(int x, int y) {
		BoardController controller=GameObject.Find ("GameController").GetComponent <BoardController> (); 
		GameObject piece=controller.getPiece(x+1, y);
		if (piece!=null&&isLegalName(piece.GetComponent<GamePiece>().color, piece)) return true;

		piece=controller.getPiece(x-1, y);
		if (piece!=null&&isLegalName(piece.GetComponent<GamePiece>().color, piece)) return true;

		piece=controller.getPiece(x, y+1);
		if (piece!=null&&isLegalName(piece.GetComponent<GamePiece>().color, piece)) return true;

		piece=controller.getPiece(x, y-1);
		if (piece!=null&&isLegalName(piece.GetComponent<GamePiece>().color, piece)) return true;

		piece=controller.getPiece(x-1, y-1);
		if (piece!=null&&isLegalName(piece.GetComponent<GamePiece>().color, piece)) return true;

		piece=controller.getPiece(x+1, y-1);
		if (piece!=null&&isLegalName(piece.GetComponent<GamePiece>().color, piece)) return true;

		piece=controller.getPiece(x-1, y+1);
		if (piece!=null&&isLegalName(piece.GetComponent<GamePiece>().color, piece)) return true;

		piece=controller.getPiece(x+1, y+1);
		if (piece!=null&&isLegalName(piece.GetComponent<GamePiece>().color, piece)) return true;

		return false;
	}

	private bool isLegalName(string name, GameObject o) {
		if (player==1) {
			return name.Equals("red")||(name.Equals("both")&&o.GetComponent<CapturableBySurroundings>().realColor.Equals("red"));
		}
		else if (player==2) {
			return name.Equals("blue")||(name.Equals("both")&&o.GetComponent<CapturableBySurroundings>().realColor.Equals("blue"));
		}
		else if (player==3) {
			return name.Equals("gold")||(name.Equals("both")&&o.GetComponent<CapturableBySurroundings>().realColor.Equals("gold"));
		}
		else if (player==4) {
			return name.Equals("green")||(name.Equals("both")&&o.GetComponent<CapturableBySurroundings>().realColor.Equals("green"));
		}

		return false;
	}
}
