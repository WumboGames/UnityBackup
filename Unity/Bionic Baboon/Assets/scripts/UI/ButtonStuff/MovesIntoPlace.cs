 using UnityEngine;
using System.Collections;

public class MovesIntoPlace : MonoBehaviour {

	public Vector2 startLocation, endLocation;
	private Transform mainCamera;
	
	void Start() {
		mainCamera=GameObject.Find("Main Camera").transform;
	}
	
	void Update () {
		float percentThere=mainCamera.position.x/10;
		float x=Mathf.Lerp(startLocation.x, endLocation.x, percentThere);
		float y=Mathf.Lerp(startLocation.y, endLocation.y, percentThere);
		transform.position=new Vector3(x, y, transform.position.z);
	}
}