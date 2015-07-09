using UnityEngine;
using System.Collections;

public class CloudMovement : MonoBehaviour {
	private GameObject mainCamera=null;
	public float xOffset=0;
	public float speed=0;
	
	void Start () {
		mainCamera=GameObject.Find("Camera");
	}

	void LateUpdate () {
		xOffset+=speed;
		if (Mathf.Abs(xOffset)>33)xOffset*=-1;
		transform.position=new Vector3(mainCamera.transform.position.x+xOffset, mainCamera.transform.position.y+1.3f, 0);
	}
}
