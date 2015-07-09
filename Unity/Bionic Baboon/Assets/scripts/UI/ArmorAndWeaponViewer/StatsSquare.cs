using UnityEngine;
using System.Collections;

public class StatsSquare : MonoBehaviour {

	public Sprite redSquare, blueSquare;

	public void setColor(string color) {
		if (color.Equals("red")) {
			GetComponent<SpriteRenderer>().sprite=redSquare;
		}
		else if (color.Equals("blue")) {
			GetComponent<SpriteRenderer>().sprite=blueSquare;
		}
		else {
			Debug.Log("Invalid Color in StatsSquare.setColor()!");
		}
	}
}
