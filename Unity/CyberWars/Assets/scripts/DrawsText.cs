using UnityEngine;
using System.Collections;

public class DrawsText : MonoBehaviour {

	private int lowerBuffer=Screen.width/40;
	private string preText = "<color=yellow><size="+Screen.width/45+">", postText="</size></color>";
	public int cost;

	void OnGUI() {
		Vector3 screenLocation=Camera.main.WorldToScreenPoint (new Vector3(transform.position.x, transform.position.y, 0));
		Rect toDrawTextIn = new Rect (screenLocation.x-Screen.width/2, -1*screenLocation.y+Screen.height+lowerBuffer, Screen.width, Screen.height);

		GUIStyle style=GUI.skin.GetStyle ("Label");
		style.alignment = TextAnchor.UpperCenter;

		GUI.Label (toDrawTextIn, preText+"$"+cost+postText);
	}
}
