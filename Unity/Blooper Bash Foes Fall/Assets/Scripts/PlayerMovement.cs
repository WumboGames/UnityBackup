using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Animator animator;
	private Rigidbody2D rigidBody;
	private float jumpHeight=15, runSpeed=500, friction=0.8f;
	private bool facingRight=true, haveDoubleJump=true, grounded=false, facePalming=false, spaceReleased=false;
	
	public Transform groundCheck;
	private float groundCheckWidth=2.5f, groundCheckHeight=.5f;
	public Transform facePalmCheck;
	private float facePalmWidth=0.5f, facePalmHeight=2.5f;
	public LayerMask collideable;
	
	public bool isAttacking=false;
	public Transform healthBar;
	public float health;
	private float maxHealth;
	
	public ParticleSystem landingParticleSystem;
	private int particleCounter=0;
	
	void Start() {
		animator=GetComponent<Animator>();
		rigidBody=GetComponent<Rigidbody2D>();
		maxHealth=health;
	}
	
	void FixedUpdate () {

        Debug.Log("Horray! Visual Studio is working!");

		if (health<=0) Application.LoadLevel("BlooperBashFoesTitleScreen");
		healthBar.localScale=new Vector3(Mathf.Max(0.57692f*(health/maxHealth), 0), healthBar.localScale.y, healthBar.localScale.z);
	
		bool spacePressed=Input.GetKeyDown("space")||Input.GetKeyDown("w")||Input.GetKeyDown("up"); 
		bool leftPressed=Input.GetKey ("a")||Input.GetKey("left");
        bool rightPressed=Input.GetKey("d")||Input.GetKey("right");

		if (!spacePressed) spaceReleased=true;
		if (!spaceReleased) spacePressed=false;
		
		AnimatorStateInfo stateInfo=setAnimatorValues();
		if ((stateInfo.IsName("Idle")||stateInfo.IsName("RunningAnimation"))&&spacePressed) {
			jump ();
			animator.Play("JumpingAnimation");
			spaceReleased=false;
			spacePressed=false;
			GameObject.Find("JumpSound").GetComponent<AudioSource>().Play(); 
		}
		if (stateInfo.IsName("Idle")&&Mathf.Abs(rigidBody.velocity.x)>0.1) {
			animator.Play("RunningAnimation");
		}
		if (spacePressed&&haveDoubleJump&&(stateInfo.IsName("JumpingAnimation")||stateInfo.IsName("FallingAnimation"))) {
			animator.Play("FlipAnimation");
			haveDoubleJump=false;
			jump();
			spaceReleased=false;
			spacePressed=false;
			GameObject.Find("JumpSound").GetComponent<AudioSource>().Play();
		}
		
		if (stateInfo.IsName("SwingSword")) isAttacking=true;
		else isAttacking=false;
		
		if (rightPressed&&canMoveRight()) {
			rigidBody.AddForce(Vector2.right*runSpeed);
			if (grounded&&++particleCounter>10) {
				Instantiate (landingParticleSystem, groundCheck.position, Quaternion.identity);
				particleCounter=0;
			}
		}
		if (leftPressed&&canMoveLeft()) {
			rigidBody.AddForce(-Vector2.right*runSpeed);
			if (grounded&&++particleCounter>10) {
				Instantiate (landingParticleSystem, groundCheck.position, Quaternion.identity);
				particleCounter=0;
			}
		}
		rigidBody.velocity=new Vector2(rigidBody.velocity.x*friction, rigidBody.velocity.y);
		
		if (Input.GetMouseButtonDown(0)) {
			animator.Play("SwingSword");
			GameObject.Find("SwordMissSound").GetComponent<AudioSource>().Play();
		}
		
		if (rigidBody.velocity.x>0.7&&!facingRight) {
			facingRight=true;
			transform.localScale=new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		if (rigidBody.velocity.x<-0.7&&facingRight) {
			facingRight=false;
			transform.localScale=new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
	}
	
	private AnimatorStateInfo setAnimatorValues() {
		animator.SetFloat("vSpeed", rigidBody.velocity.y);
		animator.SetFloat("hSpeed", Mathf.Abs(rigidBody.velocity.x));
		checkGrounded();
		checkFacePalm();
		return animator.GetCurrentAnimatorStateInfo(0);
	}
	
	private void jump() {
		rigidBody.velocity=new Vector2(rigidBody.velocity.x, jumpHeight);
	}
	
	private bool canMoveRight() {
		return !(facingRight&&facePalming);
	}
	
	private bool canMoveLeft() {
		return !((!facingRight)&&facePalming);
	}
	
	private void checkGrounded() {
		Vector2 point1=new Vector2(groundCheck.position.x-groundCheckWidth/2, groundCheck.position.y-groundCheckHeight/2);
		Vector2 point2=new Vector2(groundCheck.position.x+groundCheckWidth/2, groundCheck.position.y+groundCheckHeight/2);
		bool oldGrounded=grounded;
		grounded=Physics2D.OverlapArea(point1, point2, collideable);
		if (grounded) haveDoubleJump=true;
		animator.SetBool("grounded", grounded);
		if (grounded&&!oldGrounded)  {
			Instantiate (landingParticleSystem, groundCheck.position, Quaternion.identity);
			GameObject.Find("LandSound").GetComponent<AudioSource>().Play();
		}
	}
	
	private bool checkFacePalm(){
		Vector2 point1=new Vector2(facePalmCheck.position.x-facePalmWidth/2, facePalmCheck.position.y-facePalmHeight/2);
		Vector2 point2=new Vector2(facePalmCheck.position.x+facePalmWidth/2, facePalmCheck.position.y+facePalmHeight/2);
		facePalming=Physics2D.OverlapArea(point1, point2, collideable);
		return facePalming;
	}
}
