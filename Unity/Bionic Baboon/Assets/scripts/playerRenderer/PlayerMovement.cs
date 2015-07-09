using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float moveForce, sprintingMoveForce;
	public Transform groundCheck;
	
	private Animator playerAnimator;
	private Rigidbody2D rBody2D;
	
	private bool grounded=false, sprinting=false;
	private bool moveRight=false, moveLeft=false, jump=false, leftMousePressed=false, rightMousePressed=false;
	private bool facingRight=true;
	private PlayerWeaponController weaponsController;
	
	void Start() {
		playerAnimator=GetComponentInParent<Animator>();
		weaponsController=GetComponent<PlayerWeaponController>();
		rBody2D=GetComponent<Rigidbody2D>();
	}
	
	void Update() {
		grounded=Physics2D.Linecast(transform.position, groundCheck.position, 1<<LayerMask.NameToLayer("Ground"));
		playerAnimator.SetBool("Grounded", grounded);
		
		moveRight=Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow);
		moveLeft=Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow);
		
		sprinting=Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
		playerAnimator.SetBool("Sprinting", sprinting);
		
		if (Input.GetMouseButtonDown(0)) leftMousePressed=true;
		if (Input.GetMouseButtonDown(1)) rightMousePressed=true;
		if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.UpArrow)) jump=true;
		
		faceRightWay();
	}
	
	void FixedUpdate() {
		updateMovement();
		
		if (jump&&grounded) {
			playerAnimator.SetTrigger("Jump");
			jump=false;
		}
		else tryToAttack();
		
		updateAnimatorValues();
	}
	

	private void updateMovement() {
		bool reallySprinting=false;
		if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("sprintingAnimation"))
			reallySprinting=true;
		
		
		if (!reallySprinting) {
			if (moveRight)rBody2D.AddForce(Vector3.right*moveForce);
			if (moveLeft)rBody2D.AddForce(Vector3.left*moveForce);
		}
		else {
			if (moveRight)rBody2D.AddForce(Vector3.right*sprintingMoveForce);
			if (moveLeft)rBody2D.AddForce(Vector3.left*sprintingMoveForce);
			if (Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)) weaponsController.putAwayWeapon();
		}
	}
	
	private void faceRightWay() {
		bool oldFacingRight=facingRight;
		if (sprinting&&Mathf.Abs(rBody2D.velocity.x)>1) {
			if (rBody2D.velocity.x>0) facingRight=true;
			else facingRight=false;
		}
		else {
			if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x>transform.position.x) facingRight=true;
			else facingRight=false;
		}
		if (facingRight^oldFacingRight) transform.localScale=new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}
	
	private void updateAnimatorValues() {
		float hSpeed=Mathf.Abs(rBody2D.velocity.x);
		playerAnimator.SetFloat("hSpeed", hSpeed);
		
		float vSpeed=rBody2D.velocity.y;
		playerAnimator.SetFloat("vSpeed", vSpeed);
		
		float hSignedSpeed=rBody2D.velocity.x;
		if (!facingRight)hSignedSpeed*=-1;
		playerAnimator.SetFloat("hSpeedSigned", hSignedSpeed);
	}
	
	private void tryToAttack() {
		bool holdingNothing=true;//fix this later
		if (attacking()||!holdingNothing||!grounded) {
			leftMousePressed=false;
			rightMousePressed=false;
			return;
		}

		int index=-1;
		
		if (leftMousePressed) {
			leftMousePressed=false;
			index=Random.Range(0, 2);
		}
		else if (rightMousePressed) {
			rightMousePressed=false;
			index=Random.Range(2, 4);
		}

		if (index!=-1) {
			playerAnimator.SetInteger("attackIndex", index);
			playerAnimator.SetTrigger("attack");
		}
	}
	
	private bool attacking() {
		AnimatorStateInfo stateInfo=playerAnimator.GetCurrentAnimatorStateInfo(0);
		if (stateInfo.IsName("kickAnimation")||stateInfo.IsName("jumpKick")||stateInfo.IsName("flipKickAnimation")||stateInfo.IsName("rightHandPunch")||stateInfo.IsName("leftHandPunch")) {
			return true;	
		}
		return false;
	}
}
