using UnityEngine;
using System.Collections;

public class DrawsDaysLeftAndPlayerMoney : MonoBehaviour {

	private string preTextTitle="<color=ffffff><size="+Screen.width/15+"><b>", postTextTitle="</b></size></color>";
	private string preTextPlayerMoney = "<color=yellow><size="+Screen.width/25+"><b>", postTextPlayerMoney = "</b></size></color>";
	private string preTextPlayerTurnAndMovesLeft = "<color=blue><size="+Screen.width/35+"><b>", postTextPlayerTurnAndMovesLeft = "</b></size></color>";
	private string[] monthes={"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
	private int[] monthLengths={31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
	public string month="December";
	public int startingDay=20, player1Money=150, player2Money=150, player3Money=0, player4Money=0;

	public int playersTurn=2, movesLeft=3, players=2;
	public int player1CumulativeMoney=75, player2CumulativeMoney=75, player3CumulativeMoney=75, player4CumulativeMoney=75;

	private int simulationLength=120, extraSimulationLength=300;
	private int simulationCounter, nextPlayersTurn, payCount=0;

	void Start() {
		simulationCounter = simulationLength;
		nextPlayersTurn = (playersTurn)%players+1;
	}

	void Update() {
		if (gameObject.GetComponent<BoardController>().isDead(playersTurn)) movesLeft=0;
		if (movesLeft > 0)
			return;

		playersTurn = 0;
		if (simulationCounter>0) {
			simulationCounter--;
			return;
		}
		givePlayersMoney();
		
		simulationCounter=getSimulationLength();
		playersTurn=nextPlayersTurn;
		movesLeft=3;
		nextPlayersTurn=(playersTurn)%players+1;
		nextDay();
	}

	void OnGUI () {
		drawDay ();
		drawPlayersMoney(players==2?1:(nextPlayersTurn)/2);
		drawPlayerTurnAndMovesLeft ();
	}

	private void drawDay() {
		Rect textArea=new Rect (0, 0, Screen.width, Screen.height/7);
		GUIStyle style = GUI.skin.GetStyle ("Label");
		style.alignment = TextAnchor.LowerCenter;//centers text
		
		GUI.Label(textArea, preTextTitle+month+" "+startingDay+postTextTitle);//draw title
	}

	private void drawPlayersMoney(int playergroup) {
		Rect player1TextArea = new Rect (0,0, Screen.width/6, Screen.height*2/5);
		Rect player2TextArea = new Rect (Screen.width*5/6, 0, Screen.width/6, Screen.height*2/5);
		GUIStyle style = GUI.skin.GetStyle ("Label");
		style.alignment = TextAnchor.LowerCenter;

		int money1=playergroup==1?player1Money:player3Money;
		int money2=playergroup==1?player2Money:player4Money;
		GUI.Label (player1TextArea, preTextPlayerMoney+"$"+money1+postTextPlayerMoney);
		GUI.Label (player2TextArea, preTextPlayerMoney+"$"+money2+postTextPlayerMoney);
	}

	private void drawPlayerTurnAndMovesLeft() {
		Rect playerTurn = new Rect (Screen.width*1/15, 0, Screen.width*13/15, Screen.height/14);
		Rect movesLeft = new Rect (Screen.width*1/15, 0, Screen.width*13/15, Screen.height/14);
		Rect skipTurn = new Rect (Screen.width*1/15, 0, Screen.width*13/15, Screen.height*2/14);
		GUIStyle style = GUI.skin.GetStyle ("Label");

		style.alignment = TextAnchor.LowerLeft;
		GUI.Label (playerTurn, preTextPlayerTurnAndMovesLeft+getPlayerTurnAsString()+postTextPlayerTurnAndMovesLeft);

		style.alignment = TextAnchor.LowerRight;
		GUI.Label (movesLeft, preTextPlayerTurnAndMovesLeft+getMovesLeftAsString()+postTextPlayerTurnAndMovesLeft);
		GUI.Label (skipTurn, preTextPlayerTurnAndMovesLeft+"End Turn"+postTextPlayerTurnAndMovesLeft);
	}

	private string getMovesLeftAsString() {
		if (playersTurn == 0)
			return "Moves Left: 0";
		return "Moves Left: "+movesLeft;
	}
	
	private string getPlayerTurnAsString() {
		if (playersTurn == 1)
			return "Sony's Turn";
		if (playersTurn == 2)
			return "N. Korea's Turn";
		if (playersTurn == 3)
			return "Finest S.'s Turn";
		if (playersTurn == 4) 
			return "Lizard S.'s Turn";
		return "Simulating...";
	}

	private int getSimulationLength() {
		int pieces=0;
		int emptySpaces=0;
		BoardController b=gameObject.GetComponent<BoardController>();
		for (int x=0; x<b.getBoardWidth(); x++)
			for (int y=0; y<b.getBoardHeight(); y++)
				if (b.pieces[x+y*b.getBoardWidth()]==null)emptySpaces++;
				else pieces++;
		double percent=(0.0+pieces)/(pieces+emptySpaces);
		return simulationLength+(int)(extraSimulationLength*percent);
	}
	
	private void givePlayersMoney() {
		string gametype=gameObject.GetComponent<BoardController>().gameType;
		if (!gametype.Equals("FourPlayerStory")){
			if (gametype.Equals("TwoPlayerFair")) {
				if (month.Equals("December")&&startingDay==24) {
					player1Money+=275;
					player2Money+=275;
				}
				if (month.Equals("December")&&startingDay<15) {  
					player1Money+=75;
					player2Money+=75;
				}
				else {
					player1Money+=25;
					player2Money+=25;
				}
			}
			else {
				player1Money+=player1CumulativeMoney;
				player2Money+=player2CumulativeMoney;
				player3Money+=player3CumulativeMoney;
				player4Money+=player4CumulativeMoney;
			}
		}
		else {
			payCount++;
			if (payCount<4) return;
			Debug.Log("Paying");
			payCount=0;
			if (!month.Equals("December")) {
				player1Money+=100;
				player2Money+=150;
				player3Money+=50;
				player4Money+=50;
			}
			else {
				if (startingDay<1) {
					player1Money+=100;
					player2Money+=100;
					player3Money+=100;
					player4Money+=100;
				}
				else {
					player1Money+=50;
					player2Money+=75;
					player3Money+=150;
					player4Money+=200;
				}
			}
		}
	}
	
	private void nextDay() {
		int monthIndex=-1;
		foreach (string s in monthes) {
			monthIndex++;
			if (s.Equals(month)) break;
		}
		if (++startingDay>monthLengths[monthIndex]) {
			startingDay=1;
			if (++monthIndex>11) monthIndex=0;
			month=monthes[monthIndex];
		}
		
	}
}
