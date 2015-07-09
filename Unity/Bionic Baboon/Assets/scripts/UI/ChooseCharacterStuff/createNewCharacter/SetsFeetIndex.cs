using UnityEngine;
using System.Collections;

public class SetsFeetIndex : MonoBehaviour {

	private CreateCharacterController controller;
	private ComboBoxUsage comboBox;
	
	void Start () {
		controller=GameObject.Find("GameController").GetComponent<CreateCharacterController>();
		comboBox=GetComponent<ComboBoxUsage>();
	}
	
	void Update() {
		controller.feetIndex=comboBox.getSelectedIndex();
	}
}
