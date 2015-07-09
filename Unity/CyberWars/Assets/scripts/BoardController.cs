using UnityEngine;
using System.Collections;

public class BoardController : MonoBehaviour {
	
	public int minXSquare, minYSquare, maxXSquare, maxYSquare;
	public ArrayList attackingPieces=new ArrayList(), requests=new ArrayList();
	public GameObject[] pieces;
	public GameObject selected=null;
	public string gameType="TwoPlayerFair";

	public string loser="";
	public Sprite winner1=null, winner2=null;
	private int winnerCounter=0;
	
	private bool redDead=false, blueDead=false, goldDead=false, greenDead=false, soundPlayed=false;

	void Start () {
		pieces=new GameObject[getBoardWidth()*getBoardHeight()];
	}

	void Update() {
		findWinner();
	
		if (gameType.Equals("TwoPlayerStory")&&gameObject.GetComponent<DrawsDaysLeftAndPlayerMoney>().startingDay>=25&&loser.Equals("")) {
			if (gameObject.GetComponent<DrawsDaysLeftAndPlayerMoney>().month.Equals("December")) {
				loser="blue";
				blueDead=true;
				GameObject.Find("CheeringSound").GetComponent<AudioSource>().Play();
			}
		}
		
		if (gameType.Equals("FourPlayerStory")&&gameObject.GetComponent<DrawsDaysLeftAndPlayerMoney>().startingDay>=25&&loser.Equals("")) {
			if (gameObject.GetComponent<DrawsDaysLeftAndPlayerMoney>().month.Equals("December")) {
				loser="Time's up!";
				blueDead=true;
				greenDead=true;
				GameObject.Find("CheeringSound").GetComponent<AudioSource>().Play();
			}
		}
	}
	
	private void findWinner() {
	
		if (loser.Equals("")) {
			if (gameObject.GetComponent<DrawsDaysLeftAndPlayerMoney>().playersTurn==0) {
				foreach (GameObject o in pieces) {
					if (o!=null) {
						o.GetComponent<GamePiece>().simulationUpdate();
						o.GetComponent<GamePiece>().postSimulationUpdate();
					}
				}
				for (int i=0; i<attackingPieces.Count; i++)
					if (attackingPieces[i]!=null) (attackingPieces[i] as GameObject).GetComponent<AttackMessenger>().simulationUpdate();
				for (int i=0; i<requests.Count; i++)
					if (requests[i]!=null) (requests[i] as GameObject).GetComponent<request>().simulationUpdate();
			}
		}
	
		int totalAlive=0;
		if (!redDead) totalAlive++;
		if (!blueDead) totalAlive++;
		if (!goldDead) totalAlive++;
		if (!greenDead) totalAlive++;
		if (!(totalAlive==5-gameObject.GetComponent<DrawsDaysLeftAndPlayerMoney>().players||
				(gameType.Equals("FourPlayerStory")&&redDead))) return;
		
		if (!goldDead){
			if (gameType.Equals("FourPlayerFair")) transform.position=new Vector3(-2.5f, -2.5f, 0);
			if (gameType.Equals("FourPlayerStory")) transform.position=new Vector3(-2.5f, -2.5f, 0);
		}
		if (!greenDead){
			if (gameType.Equals("FourPlayerFair")) transform.position=new Vector3(2.5f, -2.5f, 0);
			if (gameType.Equals("FourPlayerStory")) transform.position=new Vector3(2.5f, -2.5f, 0);
		}
		if (!redDead) {
			if (gameType.Equals("TwoPlayerFair")) transform.position=new Vector3(-2.5f, -0.5f, 0);
			if (gameType.Equals("TwoPlayerStory")) transform.position=new Vector3(-2.5f, -2.5f, 0);
			if (gameType.Equals("FourPlayerFair")) transform.position=new Vector3(-2.5f, 1.5f, 0);
			if (gameType.Equals("FourPlayerStory")) transform.position=new Vector3(-2.5f, 1.5f, 0);
		}
		if (!blueDead){
			if (gameType.Equals("TwoPlayerFair")) transform.position=new Vector3(2.5f, -0.5f, 0);
			if (gameType.Equals("TwoPlayerStory")) transform.position=new Vector3(2.5f, 1.5f, 0);
			if (gameType.Equals("FourPlayerFair")) transform.position=new Vector3(2.5f, 1.5f, 0);
			if (gameType.Equals("FourPlayerStory")) transform.position=new Vector3(2.5f, 1.5f, 0);
		}
		
		
		loser="I did it already...";
		if (--winnerCounter<0) {
			winnerCounter=30;
			if (gameObject.GetComponent<SpriteRenderer>().sprite==winner1)gameObject.GetComponent<SpriteRenderer>().sprite=winner2;
			else gameObject.GetComponent<SpriteRenderer>().sprite=winner1;
		}
		
		if (!soundPlayed) {
			soundPlayed=true;
			GameObject.Find("CheeringSound").GetComponent<AudioSource>().Play();
		}
	}

	public int getBoardWidth() {
		return maxXSquare - minXSquare;
	}

	public int getBoardHeight() {
		return maxYSquare - minYSquare;
	}

	public void createPiece(int x, int y, GameObject piece) {
		x -= minXSquare+1;
		y -= minYSquare+1;
		if (x >= getBoardWidth () || y >= getBoardHeight ()||x<0||y<0) {
			//Debug.Log("-----Error: invalid coordinates. Coordinates were calculated to be: x="+x+"y="+y);
			return;
		}
		pieces[x+y*getBoardWidth()] = piece;
	}

	public GameObject getPiece(int x, int y) {
		x -= minXSquare+1;
		y -= minYSquare+1;
		if (x >= getBoardWidth () || y >= getBoardHeight ()||x<0||y<0) {
			//Debug.Log("-----Error: invalid coordinates. Coordinates were calculated to be: x="+x+"y="+y);
			return null;
		}
		return pieces[x+y*getBoardWidth()];
	}

	public int getPlayerTurn() {
		return gameObject.GetComponent <DrawsDaysLeftAndPlayerMoney> ().playersTurn;
	}

	public int getMovesLeft() {
		return gameObject.GetComponent <DrawsDaysLeftAndPlayerMoney> ().movesLeft;
	}

	public void countMove() {
		if (getMovesLeft()>0)
			gameObject.GetComponent <DrawsDaysLeftAndPlayerMoney> ().movesLeft--;
	}
	
	public void notifyDeath(string color) {
		switch (color) {
		case "red":
			redDead=true;
			break;
		case "blue":
			blueDead=true;
			break;
		case "gold":
			goldDead=true;
			break;
		case "green":
			greenDead=true;
			break; 
		}
	}
	
	public bool isDead(int player) {
		switch(player) {
		case 1:
			return redDead;
		case 2:
			return blueDead;
		case 3:
			return goldDead;
		case 4:
			return greenDead;
		}
		return false;
	}
}
