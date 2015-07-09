using UnityEngine;
using System.Collections;

public class ParalaxMovement : MonoBehaviour {
	public float horizontalMovementScalar=1.0f, verticalMovementScalar=1.0f;
	public float xOffset=0, yOffset=0;
	private new Transform camera;
	private Vector3 startPosition, startCameraPosition;
	
	void Start() {
		camera=GameObject.Find("mainCamera").transform;
		startCameraPosition=camera.position;
		startPosition=transform.position;
	}
	
	void LateUpdate() {
		float x=startPosition.x+(camera.position.x-startCameraPosition.x)*horizontalMovementScalar+xOffset;
		float y=startPosition.y+(camera.position.y-startCameraPosition.y)*verticalMovementScalar+yOffset;
		float z=startPosition.z;
		transform.position=new Vector3(x, y, z);
	}
}
