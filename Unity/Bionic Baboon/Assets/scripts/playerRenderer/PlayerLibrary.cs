using UnityEngine;
using System.Collections;

public class PlayerLibrary : MonoBehaviour {
	public Sprite[] bodySprites;
	public Sprite[] leftHandSprites;
	public Sprite[] rightHandSprites;
	public Sprite[] leftFootSprites;
	public Sprite[] rightFootSprites;
	
	
	public Item[] allHelmets, allChestplates, allBackplates, allBoots,
			allPrimaryWeapons, allSecondaryWeapons, allGrenades, allSpecials;
	
	public Item[][] allOwnedItems;
	
	private bool initialized=false;
	
	void Start() {
		init ();
	}
	
	public void init() {
		if (initialized)
			return;
		
		allOwnedItems=new Item[][]{allHelmets, allChestplates, allBackplates, allBoots,
			allPrimaryWeapons, allSecondaryWeapons, allGrenades, allSpecials};
			
		initialized=true;
	}
	
	public int getIndexOfItem(Item item, int row) {
		init();
		for (int i=0; i<allOwnedItems[row].Length; i++)
			if (item==allOwnedItems[row][i])
				return i;
		return -1;
	}
}
