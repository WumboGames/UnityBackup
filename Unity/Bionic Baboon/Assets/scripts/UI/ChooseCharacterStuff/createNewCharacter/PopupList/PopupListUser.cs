using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopupListUser : MonoBehaviour {

	private GUIContent[] contentList;
	private GUIStyle listStyle;
	//private bool picked=false;
	
	void Start() {
		contentList=new GUIContent[5];
		contentList[0]=new GUIContent("asdf 1");
		contentList[1]=new GUIContent("asdf 2");
		contentList[2]=new GUIContent("asdf 3");
		contentList[3]=new GUIContent("asdf 4");
		contentList[4]=new GUIContent("asdf 5");
		
		listStyle=new GUIStyle();
	}
	
	void OnGUI() {
		//if (Popup.)
	}
}
