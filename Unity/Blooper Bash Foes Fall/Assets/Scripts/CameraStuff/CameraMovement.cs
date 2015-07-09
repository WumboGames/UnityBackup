using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private Transform player;

	void Start() {
		player=GameObject.Find("Player").transform;
	}

	void Update () {
		transform.position=Vector3.Lerp(transform.position, player.position, 0.2f);
		transform.position=new Vector3(Mathf.Max(Mathf.Min(transform.position.x, 9), -10), Mathf.Max(transform.position.y, -1), -10);
	}
}
