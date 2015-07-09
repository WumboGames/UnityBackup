using UnityEngine;
using System.Collections;

public class HidesOnTurn : MonoBehaviour {

	public bool hideFirst=false;
	private float originalX=0;
	
	void Start () {
		originalX=transform.position.x;
	}

	void Update () {
		int hideStatus=getHideStatus();
		if (hideStatus==1) hide();
		if (hideStatus==0) show ();
	}
	
	private void hide() {
		transform.position=new Vector3(10000000f, transform.position.y, transform.position.z);
	}
	
	private void show() {
		transform.position=new Vector3(originalX, transform.position.y, transform.position.z);
	}
	
	private int getHideStatus() {
		DrawsDaysLeftAndPlayerMoney controller=GameObject.Find("GameController").GetComponent<DrawsDaysLeftAndPlayerMoney>();
		int hide=-1;
		switch(controller.playersTurn) {
		case 0:
			hide=-1;
			break;
		case 1:
		case 2:
			if (hideFirst) hide=1;
			else hide=0;
			break;
		case 3:
		case 4:
			if (hideFirst) hide=0;
			else hide=1;
			break;
		default:
			Debug.Log("Error: unknown player turn");
			break;
		}
		return hide;
	}
}
