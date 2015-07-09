using UnityEngine;
using System.Collections;

public class PlayerCostume : MonoBehaviour {

    //TODO: make this more organized so that everything is a general piece of armor, instead of having the same
    //      code like 5 times in a row

    //TODO: make one method that does scale changes to reduce duplications

	public SpriteRenderer bodyRenderer, leftHandRenderer, rightHandRenderer, leftFootRenderer, rightFootRenderer;
	public Transform[] transformParents;
	
	private Item[] itemStats;//instantiate these--------------------------------------------
	private GameObject[] renderedItems;
	private GameObject[] renderedRightItems;
	
	private bool initialized=false;
	
	private void init() {
		itemStats=new Item[8];
		renderedItems=new GameObject[8];
		renderedRightItems=new GameObject[8];
		initialized=true;
	}

	public void setItemPiece(int level, Item stats) {
		if (!initialized)
			init ();
		setItem(stats, level);
	}

	private void setItem(Item item, int row) {
		if (renderedItems[row]!=null)
			GameObject.Destroy(renderedItems[row]);
		itemStats[row]=item;
		if (item==null)
			return;

		Transform parent=transformParents[row];
		Vector3 position=parent.position+item.renderedArmor.transform.position;

		renderedItems[row]=(GameObject)Instantiate(item.renderedArmor, position, Quaternion.identity);

		Vector3 scaleChange=parent.localScale;
		Vector3 defaultScale=renderedItems[row].transform.localScale;
		renderedItems[row].transform.localScale=new Vector3(scaleChange.x*defaultScale.x, scaleChange.y*defaultScale.y, scaleChange.z*defaultScale.z);

		renderedItems[row].transform.parent=parent;


		//right, usually only for boots
		if (renderedRightItems[row]!=null)
			GameObject.Destroy(renderedRightItems[row]);
		if (item.renderedRightBoot==null) return;//this does not have a right half
		if (row!=3)
			Debug.Log("ERROR in playerCostume.setItem()-no support for right items other than boots!   row= "+row);

		position=rightFootRenderer.transform.position+item.renderedRightBoot.transform.position;
		renderedRightItems[row]=(GameObject)Instantiate(item.renderedRightBoot, position, Quaternion.identity);

		scaleChange=rightFootRenderer.transform.localScale;
		defaultScale=renderedRightItems[row].transform.localScale;
		renderedRightItems[row].transform.localScale=new Vector3(scaleChange.x*defaultScale.x, scaleChange.y*defaultScale.y, scaleChange.z*defaultScale.z);

		renderedRightItems[row].transform.parent=rightFootRenderer.transform;
	}

	
	public void setAllSprites(Sprite body, Sprite leftHand, Sprite rightHand, Sprite leftFoot, Sprite rightFoot) {
		setBodySprite(body);
		setLeftHandSprite(leftHand);
		setRightHandSprite(rightHand);
		setLeftFootSprite(leftFoot);
		setRightFootSprite(rightFoot);
	}
	
	public void setBodySprite(Sprite body) {
		bodyRenderer.sprite=body;
	}
	
	public void setLeftHandSprite(Sprite leftHand) {
		leftHandRenderer.sprite=leftHand;
	}
	
	public void setRightHandSprite(Sprite rightHand) {
		rightHandRenderer.sprite=rightHand;
	}
	
	public void setLeftFootSprite(Sprite leftFoot) {
		leftFootRenderer.sprite=leftFoot;
	}
	
	public void setRightFootSprite(Sprite rightFoot) {
		rightFootRenderer.sprite=rightFoot;
	}


    public Item getArmor(int row) {
		if (!initialized)
			init ();
		return itemStats[row];
    }

}
