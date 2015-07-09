using UnityEngine;
using System.Collections;

public class MovesCamera : MonoBehaviour {

    public float targetX, targetY;

    void OnMouseDown() {
        GameObject.Find("Main Camera").GetComponent<CameraMenuMovement>().setTarget(targetX, targetY);
    }
}
