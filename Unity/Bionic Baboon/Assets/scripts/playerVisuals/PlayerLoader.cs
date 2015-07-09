using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLoader : MonoBehaviour {

	public PlayerCostume player;
	public PlayerLibrary library;
	public bool unlockEverything=false; //SET THIS TO FALSE ON ACTUAL RELEASE!

	private string username;
	private int playerIndex;
	private int level, experience;
	
	public List<Item>[] ownedItems=new List<Item>[8];
	//public List<Item> ownedHelmets, ownedChestplates, ownedBackplates, ownedBoots, ownedPrimaryWeapons, ownedSecondaryWeapons, ownedGrenades, ownedSpecials;
	
	void Start() {
		//BOTH OF THESE NEED TO ACTUALLY LOAD THE ARMOR!
		loadPlayerSpritesAndItems();
		loadOwnedItems();
		loadLevelAndEXP();

		/*ownedItems[0]=ownedHelmets;
		ownedItems[1]=ownedChestplates;
		ownedItems[2]=ownedBackplates;
		ownedItems[3]=ownedBoots;
		ownedItems[4]=ownedPrimaryWeapons;
		ownedItems[5]=ownedSecondaryWeapons;
		ownedItems[6]=ownedGrenades;
		ownedItems[7]=ownedSpecials;*/
		
	}
	
	//TODO: make this actually load from a file
	private void loadPlayerSpritesAndItems() {
		username=PlayerPrefs.GetString("current username");
		playerIndex=PlayerPrefs.GetInt(username+" current playerIndex");
		int bodyIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" bodyIndex:");
		int handsIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" handsIndex:");
		int feetIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" feetIndex:");
	
		player.setAllSprites(library.bodySprites[bodyIndex], library.leftHandSprites[handsIndex], library.rightHandSprites[handsIndex],
		                     library.leftFootSprites[feetIndex], library.rightFootSprites[feetIndex]);
		
		library.init();
		for (int i=0; i<8; i++) {
			int itemIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" rowIndex:"+i+" itemIndex:");
			Item toSet=library.allOwnedItems[i][itemIndex];
			player.setItemPiece(i, toSet);
		}
	}
	
	//TODO: make this actually load armor
	private void loadOwnedItems() {
	
		for (int i=0; i<8; i++) {
			string ownedItemsAsString=PlayerPrefs.GetString(username+" ownedItemsAtRow:"+i, "1");//they should have only none and all zeros on a new account
			while(ownedItemsAsString.Length<library.allOwnedItems[i].Length)
				ownedItemsAsString+="0";
			PlayerPrefs.SetString(username+" ownedItemsAtRow:"+i, ownedItemsAsString);
			
			ownedItems[i]=new List<Item>();
			library.init();
			for (int j=0; j<ownedItemsAsString.Length; j++) {
				if (ownedItemsAsString[j]=='1'||unlockEverything)
					ownedItems[i].Add(library.allOwnedItems[i][j]);
			}
		}
	}
	
	private void loadLevelAndEXP() {
		level=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" level:");
		experience=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" exp:");
	}
	
	public Item getItem(int row, int index) {
		return ownedItems[row][index];
	}
	
	public List<Item> getItemList(int row) {
		return ownedItems[row];
	}

    public void setPlayerItemPiece(int row, int index) {///////////////////////////////THIS IS BROKEN!!!
		player.setItemPiece(row, ownedItems[row][index]);
		string username=PlayerPrefs.GetString("current username");
		int playerIndex=PlayerPrefs.GetInt(username+" current playerIndex");
		PlayerPrefs.SetInt(username+" playerIndex:"+playerIndex+" rowIndex:"+row+" itemIndex:", index);
    }

    public PlayerCostume getPlayerCostume() {
        return player;
    }

	public int getMaxExperience() {
		return 1000+100*(level-1);
	}
	
	public int getLevel() {
		return level;
	}
	
	public int getExp() {
		return experience;
	}

}
