using UnityEngine;
using System.Collections;

public class SkipTurn : MonoBehaviour {

	void OnMouseDown() {
		Debug.Log ("Skipping turn...");
		GameObject.Find ("GameController").GetComponent <DrawsDaysLeftAndPlayerMoney> ().movesLeft = 0;
	}
}
