using UnityEngine;
using System.Collections;

public class CapitolGamePiece : GamePiece {
	public int startX=0, startY=0;
	private bool haveSetUpValues=false;

	override public void Update() {
		if (!haveSetUpValues) setUpValues();
		base.Update ();
	}

	override public void attackWithDDOS() {
		base.attackWithDDOS();
		if (base.getDDOSCounter()!=0) {
			GameObject.Find("GameController").GetComponent<BoardController>().notifyDeath(base.color);
		}
	}

	private void setUpValues() {
		haveSetUpValues=true;
		GameObject.Find("GameController").GetComponent<BoardController>().createPiece(startX, startY, gameObject);
	}
}
