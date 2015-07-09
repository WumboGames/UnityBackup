using UnityEngine;
using System.Collections;

public class swordEnemy : MonoBehaviour {
		
	private Animator animator;
	private Rigidbody2D rigidBody;
	private float jumpHeight=15, runSpeed=300, friction=0.8f;
	private bool facingRight=true, haveDoubleJump=true, grounded=false, facePalming=false, spaceReleased=false;
	
	public Transform groundCheck;
	private float groundCheckWidth=2.5f, groundCheckHeight=.5f;
	public Transform facePalmCheck;
	private float facePalmWidth=0.5f, facePalmHeight=2.5f;
	public LayerMask collideable;
	
	public GameObject throwingStar;
	public Transform starCreator;
	public ParticleSystem landingParticleSystem;
	
	public bool isAttacking=false, usesASword=true, wasAttacking=false;
	
	private bool pressingSpace=false, pressingLeft=false, pressingRight=false;
	
	private GameObject player;
	private GameController controller;
	public float health;
	private float maxHealth;
	
	public Transform healthBar;
	
	void Start() {
		maxHealth=health;
		animator=GetComponent<Animator>();
		rigidBody=GetComponent<Rigidbody2D>();
		player=GameObject.Find("Player");/////////////////////////////////////////////////Delete this!!!!!!!!!//////////////////////////
		StartCoroutine(pressButtons());
	}
	
	public void SetValues(GameObject player, GameController controller) {
		this.player=player;
		this.controller=controller;
	}
	
	void FixedUpdate () {
		healthBar.localScale=new Vector3(Mathf.Max(0.57692f*(health/maxHealth), 0), healthBar.localScale.y, healthBar.localScale.z);
	
		bool spacePressed=pressingSpace;
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
		if (stateInfo.IsName("SwingSword")||stateInfo.IsName("ThrowStar")) isAttacking=true;
		else {
			if (wasAttacking) {
				Instantiate(throwingStar, starCreator.position, Quaternion.identity);
			}
			isAttacking=false;
		}
		if (stateInfo.IsName("ThrowStar")) wasAttacking=true;
		else wasAttacking=false;
		
		if (pressingRight&&canMoveRight()) rigidBody.AddForce(Vector2.right*runSpeed);
		if (pressingLeft&&canMoveLeft()) rigidBody.AddForce(-Vector2.right*runSpeed);
		rigidBody.velocity=new Vector2(rigidBody.velocity.x*friction, rigidBody.velocity.y);
		
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
		bool wasGrounded=grounded;
		grounded=Physics2D.OverlapArea(point1, point2, collideable);
		if (grounded) haveDoubleJump=true;
		animator.SetBool("grounded", grounded);
		if (grounded&&!wasGrounded) {
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
	
	private IEnumerator pressButtons() {
		while(health>0) {
			while (isAttacking) {
				yield return new WaitForSeconds(0.1f);
			}
			double value=usesASword?0.5:0.25;
			if (Random.value<value) {
				if (usesASword) animator.Play("SwingSword");
				else animator.Play("ThrowStar");
				GameObject.Find("SwordMissSound").GetComponent<AudioSource>().Play();
			}
			
			if (usesASword) {
				float playerX=player.transform.position.x;
				if (playerX>transform.position.x) {
					pressingLeft=false;
					pressingRight=true;
				}
				else {
					pressingRight=false;
					pressingLeft=true;
				}
			}
			else {
				if (Random.value<0.5) {
					pressingLeft=false;
					pressingRight=true;
				}
				else {
					pressingLeft=true;
					pressingRight=false;
				}
			}
			
			if (Random.value<.5) {
				pressingSpace=true;
				yield return new WaitForSeconds(0.1f);
				pressingSpace=false;
			}
			
			yield return new WaitForSeconds(0.5f);//reevaluate after .5 seconds
		}
		controller.remove(gameObject);
		Destroy(gameObject);
	}
	
}