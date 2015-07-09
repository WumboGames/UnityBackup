using UnityEngine;
using System.Collections;

public class MovesOnToggle : MonoBehaviour {

	public bool showWhenArmorIsOpen=true;
	public Vector2 showPosition, hidePosition;
	
	private UISettings settings;
	private float moveSpeed=0.2f;
	
	void Start() {
		settings=GameObject.Find("GameController").GetComponent<UISettings>();
	}
	
	void Update() {
		Vector2 target=hidePosition;
		if (shown ()) target=showPosition;
		
		float targetX=Mathf.Lerp(transform.position.x, target.x, moveSpeed);
		float targetY=Mathf.Lerp(transform.position.y, target.y, moveSpeed);
		
		transform.position=new Vector3(targetX, targetY, transform.position.z);
	}
	
	private bool shown() {
		return !(settings.getArmorTabOpen()^showWhenArmorIsOpen);//returns true if they are the same, false if they are different
	}
}
