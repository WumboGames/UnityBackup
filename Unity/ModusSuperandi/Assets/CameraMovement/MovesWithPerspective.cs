using UnityEngine;
using System.Collections;

public class MovesWithPerspective : MonoBehaviour {
	private GameObject mainCamera;
	public float perspectiveScaleX=0.9f,  perspectiveScaleY=1f;
	public float normalXOffset=0, normalYOffset=0;
	public float loopDistance=33;
	
	void Start () {
		mainCamera=GameObject.Find("Camera");
	}
	
	void LateUpdate () {
		float xOffset=mainCamera.transform.position.x-mainCamera.transform.position.x*perspectiveScaleX+normalXOffset;
		float yOffset=mainCamera.transform.position.y-mainCamera.transform.position.y*perspectiveScaleY+normalYOffset;
		xOffset=(xOffset%loopDistance+loopDistance)%loopDistance-loopDistance/2;
		float newX=mainCamera.transform.position.x-xOffset;
		float newY=mainCamera.transform.position.y-yOffset;
		
		
		
		transform.position=new Vector3(newX, newY, 0);
	}
}
