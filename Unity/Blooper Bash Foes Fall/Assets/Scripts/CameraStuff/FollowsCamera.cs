using UnityEngine;
using System.Collections;

public class FollowsCamera : MonoBehaviour {

	public float paralax=1.0f;
	public float xContinualOffset=.00f, xMaxOffset=10f;
	public float xOffset=0f;
	private GameObject camera1;
	
	void LateUpdate() {
		xOffset+=xContinualOffset;
		if (xOffset>xMaxOffset) xOffset-=xMaxOffset*2;
		
		if (camera1==null) camera1=GameObject.Find("camera");
		transform.position=new Vector3(camera1.transform.position.x*paralax+xOffset, camera1.transform.position.y*paralax, transform.position.z);
	}
}
