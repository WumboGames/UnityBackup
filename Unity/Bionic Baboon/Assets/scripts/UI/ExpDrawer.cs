using UnityEngine;
using System.Collections;

public class ExpDrawer : MonoBehaviour {

	private Vector3 normalScale;
	private PlayerLoader playerLoader;
	private bool initialized=false;
	private int level=0, experience=0, maxExperience=0;

	public TextMesh levelNumber;
	
	void Start () {
		normalScale=transform.localScale;
	}

	void Update () {
		if (!initialized)
			init();
	}

	private void init() {
		initialized=true;
		playerLoader=GameObject.Find("GameController").GetComponent<PlayerLoader>();
		level=playerLoader.getLevel();
		experience=playerLoader.getExp();
		maxExperience=playerLoader.getMaxExperience();

		float scalar=experience/(float)maxExperience;
		transform.localScale=new Vector3(normalScale.x*scalar, normalScale.y, normalScale.z);

		levelNumber.text=""+level;
	}
}
