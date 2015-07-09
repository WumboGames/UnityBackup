using UnityEngine;
using System.Collections;

public class Dies : MonoBehaviour {
	public float timeBeforeDying=2f;
	void Start() {
		StartCoroutine("die");
	}
	
	IEnumerator die() {
		Debug.Log("starting");
		yield return new WaitForSeconds(timeBeforeDying);
		Destroy(gameObject);
		yield break;
	}
}
