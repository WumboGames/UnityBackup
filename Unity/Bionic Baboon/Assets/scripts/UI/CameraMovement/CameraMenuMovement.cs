using UnityEngine;
using System.Collections;

public class CameraMenuMovement : MonoBehaviour {

    private float targetX, targetY;
	
    void Update () {
        float newX=Mathf.Lerp(transform.position.x, targetX, 0.1f); 
        float newY=Mathf.Lerp(transform.position.y, targetY, 0.1f); 
        transform.position=new Vector3(newX, newY, transform.position.z);
	}

    public void setTarget(float targetX, float targetY) {
        this.targetX=targetX;
        this.targetY=targetY;
    }
}
