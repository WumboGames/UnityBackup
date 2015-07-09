using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {

	private GameObject player;
	public LayerMask enemies;
	public Vector2 size, center;
	public ParticleSystem explosion;
	
	void Start () {
		player=transform.parent.parent.gameObject;
	}
	
	void FixedUpdate() {
		if (!player.GetComponent<PlayerMovement>().isAttacking) return;
		
		float width=size.x, height=size.y, x=center.x, y=center.y;
		Vector2 point1=new Vector2(transform.position.x-width/2, transform.position.y-height/2);
		Vector2 point2=new Vector2(transform.position.x+width/2, transform.position.y+height/2);
		Collider2D other=Physics2D.OverlapArea(point1, point2, enemies);
		if (other==null) return;
		
		Vector2 enemyDirection=Vector2.up;
		if (transform.position.x>other.gameObject.transform.position.x)enemyDirection=new Vector2(-100, 10);
		else enemyDirection=new Vector2(100, 10);
		other.gameObject.GetComponent<Rigidbody2D>().velocity=enemyDirection;
		swordEnemy enemy=other.gameObject.GetComponent<swordEnemy>();
		enemy.health-=1;
		
		Vector2 explosionPoint=(transform.position+other.transform.position)*.5f;
		Instantiate(explosion, explosionPoint, Quaternion.identity);
	}
}
