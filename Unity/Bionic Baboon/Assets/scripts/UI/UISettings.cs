using UnityEngine;
using System.Collections;

public class UISettings : MonoBehaviour {

	bool armorOpen=true;
	
	public bool getArmorTabOpen() {
		return armorOpen;
	}
	
	public bool getWeaponsTabOpen() {
		return !armorOpen;
	}
	
	public void toggleWeaponsArmorTab() {
		armorOpen=!armorOpen;
	}
}
