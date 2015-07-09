using UnityEngine;
using System.Collections;

public class SecondaryWeapon : MonoBehaviour {
	void Start () {
		transform.rotation=Quaternion.Euler(0, 0, -75);
	}
}
