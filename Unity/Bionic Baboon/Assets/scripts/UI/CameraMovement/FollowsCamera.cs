using UnityEngine;
using System.Collections;

public class FollowsCamera : MonoBehaviour {

    private Transform cameraToFollow;

    void Start() {
	    cameraToFollow=GameObject.Find("Main Camera").transform;
    }

    void Update() {
        transform.position=new Vector3(cameraToFollow.position.x, cameraToFollow.position.y, transform.position.z);
    }
}
