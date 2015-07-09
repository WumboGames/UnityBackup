using UnityEngine;
using System.Collections;

public class ComboBoxUsage : MonoBehaviour {
	GUIContent[] comboBoxList;
	private ComboBox comboBoxControl;// = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();
	public string[] choices;
	public float x, y, width=1f/4, height=1f/20;
	
	private void Start(){
		comboBoxList=new GUIContent[choices.Length];
		for (int i=0; i<choices.Length; i++) {
			comboBoxList[i]=new GUIContent(choices[i]);
		}
		
		listStyle.normal.textColor = Color.white; 
		listStyle.onHover.background =
			listStyle.hover.background = new Texture2D(2, 2);
		listStyle.padding.left =
			listStyle.padding.right =
				listStyle.padding.top =
				listStyle.padding.bottom = 4;
		
		comboBoxControl = new ComboBox(new Rect(Screen.width*x, Screen.height*y, Screen.width*width, Screen.height*height), comboBoxList[0],
					comboBoxList, "button", "box", listStyle);
	}
	
	private void OnGUI () {
		comboBoxControl.Show();
	}
	
	public int getSelectedIndex() {
		return comboBoxControl.selectedIndex;
	}
}
