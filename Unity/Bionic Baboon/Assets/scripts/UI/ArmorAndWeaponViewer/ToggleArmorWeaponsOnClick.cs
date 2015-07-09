using UnityEngine;
using System.Collections;

public class ToggleArmorWeaponsOnClick : MonoBehaviour {
	public Transform buttonToFlip;
	private UISettings settings;
	
	void Start() {
		settings=GameObject.Find("GameController").GetComponent<UISettings>();
	}
	
	void OnMouseDown() {
		buttonToFlip.localScale=new Vector3(buttonToFlip.localScale.x*-1, buttonToFlip.localScale.y, buttonToFlip.localScale.z);
		settings.toggleWeaponsArmorTab();
	}
}
