using UnityEngine;
using System.Collections;

public class ItemButton : MonoBehaviour {
	
	private  ArmorRow row;
	private int index;
	private Vector3 normalScale;
	private new string animation="none";
	private int animationCounter, maxAnimationCounter=10;
	private TextMesh armorLevel;
	
	public SpriteRenderer squareIcon;
	private Item armorStats;
	private ItemStatsController statsController;
	private DrawsStats statsDrawer;

	private int playerLevel=0;

	private bool initialized=false;

	public virtual void Start() {
        squareIcon=((GameObject)Instantiate(squareIcon.gameObject, transform.position, Quaternion.identity)).GetComponent<SpriteRenderer>();//makes a copy
        squareIcon.transform.parent=transform;
		normalScale=squareIcon.transform.localScale;
		animationCounter=maxAnimationCounter;
    }

	private void Update() {
		if (!initialized)
			init();
	}

	private void FixedUpdate() {
		switch(animation) {
			case "none":
				break;
			case "opening":
				updateOpeningAnimation();
				break;
			case "closing":
				updateClosingAnimation();
				break;
			default:
				Debug.Log("invalid string in fixedUpdate for ArmorButton!");
				break;
		}

		float scalar=(float)animationCounter/(float)maxAnimationCounter;
		squareIcon.transform.localScale=scalar*normalScale;
	}

	private void updateOpeningAnimation() {
		animationCounter++;
		if (animationCounter>=maxAnimationCounter) {
			animationCounter=maxAnimationCounter;
			animation="none";
		}
	}

	private void updateClosingAnimation() {
		animationCounter--;
		if (animationCounter<=0) {
			animation="opening";
			row.switchArmor(index);
		}
	}

	public void setRowAndIndex(ArmorRow row, int index) {
		this.row=row;
		this.index=index;
	}
	
	public void setSprite(Item armorStats) {
		if (!initialized)
			init();
		if (armorStats==null) {
			squareIcon.sprite=null;
			armorLevel.text="";
			return;
		}
		this.armorStats=armorStats;
		squareIcon.sprite=armorStats.squareIcon;
		if (!canBeClicked())
			armorLevel.text=""+armorStats.unlockLevel;
		else
			armorLevel.text="";
	}

	////////////////////////////////////////////////////////ADD CHECK TO MAKE SURE ANOTHER PIECE IS NOT SWITCHING!!!
    public void OnMouseDown() {
		if (!canBeClicked())
			return;
		startSwitchingAnimation();
		row.startSwitchingAnimation();
		closeStats();
    }

	public void startSwitchingAnimation() {
		if (animation.Equals("none")) {
			animation="closing";
			animationCounter=maxAnimationCounter;
		}
	}

	public void OnMouseEnter() {
		if (armorStats==null)
			return;
		Vector3 position=transform.position+Vector3.up*1.9f+Vector3.left*1;
		statsDrawer=((GameObject)(GameObject.Instantiate(statsController.drawStatsPrefab.gameObject, position, Quaternion.identity))).GetComponent<DrawsStats>();
		statsDrawer.setStats(armorStats);
	}

	public void OnMouseExit() {
		closeStats();
	}

	private void closeStats() {
		if (statsDrawer!=null)
			statsDrawer.close();
	}

	private void init() {
		statsController=GameObject.Find("GameController").GetComponent<ItemStatsController>();
		initialized=true;
		setArmorLevel();
		playerLevel=GameObject.Find("GameController").GetComponent<PlayerLoader>().getLevel();
	}

	private void setArmorLevel() {
		TextMesh textMeshPrefab=GameObject.Find("GameController").GetComponent<ItemStatsController>().emptyTextMeshPrefab;

		Vector3 position=transform.position+new Vector3(0.3f, -0.4f, 0);
		armorLevel=((GameObject)GameObject.Instantiate(textMeshPrefab.gameObject, position, Quaternion.identity)).GetComponent<TextMesh>();
		armorLevel.transform.localScale=new Vector3(0.1f, 0.1f, 1f);
		armorLevel.text="";
		armorLevel.fontSize=48;
		armorLevel.fontStyle=FontStyle.Bold;
		armorLevel.color=new Color(141/(float)255, 0f, 40/(float)255);
		armorLevel.GetComponent<MeshRenderer>().sortingLayerName="RequiredLevelForArmor";
		armorLevel.GetComponent<MeshRenderer>().sortingOrder=0;
		armorLevel.transform.parent=transform;
	}

	public bool canBeClicked() {
		return armorStats!=null&&armorStats.unlockLevel<=playerLevel;
	}
}
