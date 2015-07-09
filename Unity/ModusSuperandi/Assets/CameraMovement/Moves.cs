using UnityEngine;
using System.Collections;

public class Moves : MonoBehaviour {
	void Update () {
		if (Input.GetKey("a")) {
			transform.position+=new Vector3(-0.1f, 0, 0);
		}
		if (Input.GetKey("s")) {
			transform.position+=new Vector3(0, -0.1f, 0);
		}
		if (Input.GetKey("d")) {
			transform.position+=new Vector3(0.1f, 0, 0);
		}
		if (Input.GetKey("w")) {
			transform.position+=new Vector3(0, 0.1f, 0);
		}
	}
}
