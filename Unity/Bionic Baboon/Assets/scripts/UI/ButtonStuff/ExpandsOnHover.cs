using UnityEngine;
using System.Collections;

public class ExpandsOnHover : MonoBehaviour {

	public bool shrinkOnClick=true;

    private float scaleIncrease = .3f, regularXScale, regularYScale;
    private int scaleCounter = 0, MAX_SCALE_COUNTER = 5;
    private bool mouseOn = false; 

    void Start() {
        regularXScale = transform.localScale.x;
        regularYScale = transform.localScale.y;
    }

    void LateUpdate () {
        if (mouseOn) {
            scaleCounter++;
            if (scaleCounter>MAX_SCALE_COUNTER)
                scaleCounter=MAX_SCALE_COUNTER;
        }
        else {
            scaleCounter--;
            if (scaleCounter<0)
                scaleCounter=0;
        }
        float totalScaleIncrease=1+scaleIncrease*scaleCounter/MAX_SCALE_COUNTER;
        transform.localScale=new Vector3(regularXScale*totalScaleIncrease, regularYScale*totalScaleIncrease, transform.localScale.z);
	}

    void OnMouseEnter() {
        mouseOn = true;
    }

    void OnMouseExit() {
        mouseOn = false;
    }
    
    void OnMouseDown() {
    	if (shrinkOnClick) scaleCounter=0;
    }
}
