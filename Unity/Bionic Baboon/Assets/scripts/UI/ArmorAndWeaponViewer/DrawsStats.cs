using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawsStats : MonoBehaviour {

	private ItemStatsController statsController;

	private bool initialized=false;
	private Item armorStats;
	private Vector3 normalScale, normalTitleScale, normalSquareScale;
	private int animationCounter=0, maxAnimationCounter=10;
	private string currentAnimation="open";

	public GameObject textMeshPrefab;
	private TextMesh title;
	private TextMesh subtitle;
	private TextMesh defense, weight, comfort;

	public GameObject squarePrefab;
	private List<StatsSquare>[] squares=new List<StatsSquare>[3];

	void Start() {
		normalScale=transform.localScale;
		transform.localScale=new Vector3(0, 0, 0);
	}

	void FixedUpdate() {
		switch (currentAnimation) {
			case "open":
				animationCounter++;
				if (animationCounter>=maxAnimationCounter) {
					currentAnimation="none"; 
				}
				break;
			case "close":
				animationCounter--;
				if (animationCounter<=0) {
					Destroy(gameObject);
				}
				break;
			case "none":
				break;
		}

		float scalar=(float)animationCounter/(float)maxAnimationCounter;
		updateScale(scalar);
		updateAlpha(scalar);
	}

	private void updateScale(float scalar) {
		transform.localScale=scalar*normalScale;
		title.transform.localScale=scalar*normalTitleScale*.33f;
		subtitle.transform.localScale=scalar*normalTitleScale*.33f;
		defense.transform.localScale=scalar*normalTitleScale*.33f;
		weight.transform.localScale=scalar*normalTitleScale*.33f;
		comfort.transform.localScale=scalar*normalTitleScale*.33f;

		for (int i=0; i<squares.Length; i++)
			for (int ii=0; ii<squares[i].Count; ii++)
				squares[i][ii].transform.localScale=scalar*normalSquareScale;
	}

	private void updateAlpha(float alpha) {
		Color old=GetComponent<SpriteRenderer>().color;
		alpha=Mathf.Sqrt(Mathf.Pow(alpha, 3));//1.5 power
		GetComponent<SpriteRenderer>().color=new Color(old.r, old.g, old.b, alpha);

		old=title.color;
		title.color=new Color(old.r, old.g, old.b, alpha);

		old=subtitle.color;
		subtitle.color=new Color(old.r, old.g, old.b, alpha);

		Color toSet=new Color(1, 1, 1, alpha);
		for (int i=0; i<squares.Length; i++) {
			for (int ii=0; ii<squares[i].Count; ii++) {
				squares[i][ii].GetComponent<SpriteRenderer>().color=toSet;
			}
		}
	}

	public void setStats(Item armorStats) {
		if (!initialized)
			init();
		if (armorStats==null||armorStats.rarity==null)
			return;
		this.armorStats=armorStats;
		
		setSprite();
		setTitle();
		setSubtitle();
		setStatLabels();
		setSquares();
	}

	private void setSquares() {
		for (int i=0; i<squares.Length; i++) {
			squares[i]=new List<StatsSquare>();
			for (int ii=0; ii<10; ii++) {
				Vector3 position=transform.position+Vector3.down*i*0.45f+Vector3.right*ii*0.3f+new Vector3(-0.5f, 0f, 0);
				squares[i].Add(((GameObject)Instantiate(squarePrefab, position, Quaternion.identity)).GetComponent<StatsSquare>());
				squares[i][ii].setColor("red");
			}
		}
		normalSquareScale=squares[0][0].transform.localScale;

		for (int i=0; i<armorStats.stat1; i++)
			squares[0][i].setColor("blue");

		for (int i=0; i<armorStats.stat2; i++)
			squares[1][i].setColor("blue");

		for (int i=0; i<armorStats.stat3; i++)
			squares[2][i].setColor("blue");
	}

	private void setSprite() {
		Sprite newSprite=null;
		switch (armorStats.rarity) {
			case "grey":
				newSprite=statsController.greyBackground;
				break;
			case "green":
				newSprite=statsController.greenBackground;
				break;
			case "blue":
				newSprite=statsController.blueBackground;
				break;
			case "red":
				newSprite=statsController.redBackground;
				break;
			default:
				Debug.Log("Invalid rarity in DrawStats.setStats!");
				break;
		}
		gameObject.GetComponent<SpriteRenderer>().sprite=newSprite;
	}

	private void setTitle() {
		Vector3 position=transform.position+new Vector3(-2.3f, 1.15f, 0);
		title=((GameObject)(GameObject.Instantiate(textMeshPrefab, position, Quaternion.identity))).GetComponent<TextMesh>();
		title.text=armorStats.name;
		title.fontSize=72;
		title.fontStyle=FontStyle.Bold;
		title.color=new Color(103/(float)255, 154/(float)255, 249/(float)255);
		normalTitleScale=title.transform.localScale;
		title.transform.localScale=Vector3.zero;
	}

	private void setSubtitle() {
		Vector3 position=transform.position+new Vector3(-2f, .55f, 0);
		subtitle=((GameObject)(GameObject.Instantiate(textMeshPrefab, position, Quaternion.identity))).GetComponent<TextMesh>();
		subtitle.text=armorStats.description;
		subtitle.fontSize=36;
		subtitle.fontStyle=FontStyle.Normal;
		subtitle.color=new Color(175/(float)255, 152/(float)255, 243/(float)255);
		subtitle.transform.localScale=Vector3.zero;
	}

	private void setStatLabels() {
		Vector3 position=transform.position+new Vector3(-2.2f, 0.2f, 0);
		defense=((GameObject)(GameObject.Instantiate(textMeshPrefab, position, Quaternion.identity))).GetComponent<TextMesh>();
		defense.text=armorStats.stat1Name;
		defense.fontSize=48;
		defense.fontStyle=FontStyle.Bold;
		defense.color=new Color(103/(float)255, 154/(float)255, 249/(float)255);
		defense.transform.localScale=Vector3.zero;

		position+=Vector3.down*0.45f;

		weight=((GameObject)(GameObject.Instantiate(textMeshPrefab, position, Quaternion.identity))).GetComponent<TextMesh>();
		weight.text=armorStats.stat2Name;
		weight.fontSize=48;
		weight.fontStyle=FontStyle.Bold;
		weight.color=new Color(103/(float)255, 154/(float)255, 249/(float)255);
		weight.transform.localScale=Vector3.zero;

		position+=Vector3.down*0.45f;

		comfort=((GameObject)(GameObject.Instantiate(textMeshPrefab, position, Quaternion.identity))).GetComponent<TextMesh>();
		comfort.text=armorStats.stat3Name;
		comfort.fontSize=48;
		comfort.fontStyle=FontStyle.Bold;
		comfort.color=new Color(103/(float)255, 154/(float)255, 249/(float)255);
		comfort.transform.localScale=Vector3.zero;
	}

	public void close() {
		currentAnimation="close";
	}

	private void init() {
		statsController=GameObject.Find("GameController").GetComponent<ItemStatsController>();
		initialized=true;
	}

	void OnDestroy() {
		deleteText();
		deleteSquares();
	}

	private void deleteText() {
		if (title!=null)
			Destroy(title.gameObject);
		if (subtitle!=null)
			Destroy(subtitle.gameObject);
		if (defense!=null)
			Destroy(defense.gameObject);
		if (weight!=null)
			Destroy(weight.gameObject);
		if (comfort!=null)
			Destroy(comfort.gameObject);
	}

	private void deleteSquares() {
		for (int i=0; i<squares.Length; i++)
			for (int ii=0; ii<squares[i].Count; ii++)
				Destroy(squares[i][ii].gameObject);
	}

}
