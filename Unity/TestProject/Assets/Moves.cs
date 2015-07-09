using UnityEngine;
using System.Collections;

public class Moves : MonoBehaviour {

	public int jumpHeight=20;
	public float speed=5;

	public void Update () {//called automatically
		if (Input.GetKeyDown("space")) {
			jump();
		}
		
		if (Input.GetKey("d")) {
			move(speed);
		}
		
		if (Input.GetKey("a")) {
			move(-speed);
		}
	}
	
	private void jump() {
		rigidbody2D.velocity=new Vector2(0, jumpHeight);
		Debug.Log("jumping");
	}
	
	private void move(float moveSpeed) {
		rigidbody2D.velocity=new Vector2(moveSpeed, rigidbody2D.velocity.y);
	}
	
}