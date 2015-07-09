using UnityEngine;
using System.Collections;

public class Throwable : MonoBehaviour {	
	private Vector3 target;
	public LayerMask enemies;
	public Vector2 size, center;
	public ParticleSystem explosion;
	private Rigidbody2D rigidBody;
	private float throwStrength=12f;
	
	void Start () {
		target=Input.mousePosition;
		rigidBody=GetComponent<Rigidbody2D>();
		Vector2 velocity=new Vector2(target.x-transform.position.x, target.y-transform.position.y+2f);
		velocity/=Mathf.Sqrt(velocity.x*velocity.x+velocity.y*velocity.y);
		rigidBody.velocity=velocity*throwStrength;
	}
	
	void FixedUpdate() {
		float width=size.x, height=size.y, x=center.x, y=center.y;
		Vector2 point1=new Vector2(transform.position.x-width/2, transform.position.y-height/2);
		Vector2 point2=new Vector2(transform.position.x+width/2, transform.position.y+height/2);
		Collider2D other=Physics2D.OverlapArea(point1, point2, enemies);
		if (other==null) return;
		
		Vector2 enemyDirection=Vector2.up;
		if (transform.position.x>other.gameObject.transform.position.x)enemyDirection=new Vector2(-20, 10);
		else enemyDirection=new Vector2(20, 10);
		other.gameObject.GetComponent<Rigidbody2D>().velocity=enemyDirection;
		swordEnemy toAttack=other.gameObject.GetComponent<swordEnemy>();
		toAttack.health-=0.25f;
		
		Vector2 explosionPoint=(transform.position+other.transform.position)*.5f;
		Instantiate(explosion, explosionPoint, Quaternion.identity);
	}
}
