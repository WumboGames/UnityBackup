using UnityEngine;
using System.Collections;

public class RowController : MonoBehaviour {

	public ArmorRow[] rows;
	private bool initialized=false;

	void Update() {
		if (!initialized)
			init();
	}

	private void init() {
		foreach (ArmorRow row in rows) {
			row.init();
		}
		initialized=true;
	}
}
