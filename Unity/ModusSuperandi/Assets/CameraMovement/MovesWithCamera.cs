using UnityEngine;
using System.Collections;

public class MovesWithCamera : MonoBehaviour {
	private GameObject mainCamera;
	void Start () {
		mainCamera=GameObject.Find("Camera");
	}
	
	void LateUpdate () {
		transform.position=new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0);
	}
}
