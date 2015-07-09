using UnityEngine;
using System.Collections;

public class FadesOut : MonoBehaviour {

	public int counter=-30;
	public int end=200;
	private int countSpeed=1;
	
	void Update () {
		int newCounter=(counter<0?0:counter);
		float alpha=1-((0.0f+newCounter)/end);
		gameObject.GetComponent <SpriteRenderer>().color = new Color(1f,1f,1f, alpha);
		
		if (counter<end) counter+=countSpeed;
		else Application.LoadLevel("BlooperBashFoesTitleScreen");
		if (counter==-20) GameObject.Find("WumboGamesIntroSound").GetComponent<AudioSource>().Play();
	}
}
