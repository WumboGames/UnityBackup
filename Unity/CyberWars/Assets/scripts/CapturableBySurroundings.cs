using UnityEngine;
using System.Collections;

public class CapturableBySurroundings : MonoBehaviour {

	public string realColor="";
	public Sprite redVersion=null;
	public Sprite blueVersion=null;
	public Sprite goldVersion=null;
	public Sprite greenVersion=null;

	private string colorToChangeTo="";
	private int redCounter=0, blueCounter=0, greenCounter=0, goldCounter=0, changeColorCounter=-1;

	public void simulationUpdate() {
		if (--changeColorCounter==0) {
			Debug.Log("Changing color from "+realColor+" to "+colorToChangeTo);
			realColor=colorToChangeTo;
			return;
		}
		if (changeColorCounter>0) return;

		if (redCounter>blueCounter&&redCounter>goldCounter&&redCounter>greenCounter) {
			gameObject.GetComponent<SpriteRenderer>().sprite=redVersion;
			changeToColor("red");
		}
		if (blueCounter>redCounter&&blueCounter>greenCounter&&blueCounter>goldCounter) {
			gameObject.GetComponent<SpriteRenderer>().sprite=blueVersion;
			changeToColor("blue");
		}
		if (greenCounter>redCounter&&greenCounter>blueCounter&&greenCounter>goldCounter) {
			gameObject.GetComponent<SpriteRenderer>().sprite=greenVersion;
			changeToColor("green");
		}
		if (goldCounter>redCounter&&goldCounter>greenCounter&&goldCounter>blueCounter) {
			gameObject.GetComponent<SpriteRenderer>().sprite=goldVersion;
			changeToColor("gold");
		}
	}

	public void postSimulationUpdate() {
		
		redCounter=0;
		blueCounter=0;
		greenCounter=0;
		goldCounter=0;

		BoardController b=GameObject.Find("GameController").GetComponent<BoardController>();
		evaluate(b.getPiece((int)(transform.position.x+0.5-1), (int)(transform.position.y+0.5)));
		evaluate(b.getPiece((int)(transform.position.x+0.5), (int)(transform.position.y+0.5-1)));
		evaluate(b.getPiece((int)(transform.position.x+0.5+1), (int)(transform.position.y+0.5)));
		evaluate(b.getPiece((int)(transform.position.x+0.5), (int)(transform.position.y+0.5+1)));
		
		evaluate(b.getPiece((int)(transform.position.x+0.5-1), (int)(transform.position.y+0.5+1)));
		evaluate(b.getPiece((int)(transform.position.x+0.5-1), (int)(transform.position.y+0.5-1)));
		evaluate(b.getPiece((int)(transform.position.x+0.5+1), (int)(transform.position.y+0.5+1)));
		evaluate(b.getPiece((int)(transform.position.x+0.5+1), (int)(transform.position.y+0.5-1)));
	}

	private void evaluate(GameObject o) {
		if (o==null) return;
		evaluateColor(o.GetComponent<GamePiece>().color, o);
	}

	private void evaluateColor(string color, GameObject o) {
		if (color.Equals("red"))redCounter++;
		if (color.Equals("blue"))blueCounter++;
		if (color.Equals("green"))greenCounter++;
		if (color.Equals("gold"))goldCounter++;
		if (color.Equals("both"))evaluateColor(o.GetComponent<CapturableBySurroundings>().realColor, o);
	}

	private void changeToColor(string color) {
		if (colorToChangeTo.Equals(color)) return;
		colorToChangeTo=color;
		changeColorCounter=50;
	}
}
