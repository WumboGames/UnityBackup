using UnityEngine;
using System.Collections;

public class GivesJumpBoost : MonoBehaviour {
	public float jumpForce;
	private Rigidbody2D rBody2D;
	
	void Start() {
		rBody2D=GetComponent<Rigidbody2D>();
	}
	
	public void giveJumpBoost() {
		rBody2D.velocity=new Vector2(rBody2D.velocity.x, jumpForce);
	}
}
