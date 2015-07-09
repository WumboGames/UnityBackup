using UnityEngine;
using System.Collections;

public class FadesOut : MonoBehaviour {
	
	public int counter=-30;
	public int end=100;
	private int countSpeed=2;
	void Update () {
		int newCounter=(counter<0?0:counter);
		float alpha=1-((0.0f+newCounter)/end);
		gameObject.GetComponent <SpriteRenderer>().color = new Color(1f,1f,1f, alpha);

		if (counter<end) counter+=countSpeed;
		if (counter==-20) GameObject.Find("CyberWarsSound").GetComponent<AudioSource>().Play();
	}
}
