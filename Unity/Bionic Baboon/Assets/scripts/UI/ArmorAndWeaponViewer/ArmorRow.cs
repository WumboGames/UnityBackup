using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmorRow : MonoBehaviour {
	public int row;
	public List<ItemButton> buttons;
	public ItemButton selectedButton;
	private PlayerLoader loader;
	private int armorOffset=0;

	public void init() {
		loader=GameObject.Find("GameController").GetComponent<PlayerLoader>();
        setButtonSprites();
	}

	
	public void leftArrowPressed() {
		armorOffset--;
		if (armorOffset<0) armorOffset=0;
        Debug.Log("left arrow pressed on row "+row+". armorOffset= "+armorOffset);
        setButtonSprites();
	}
	
	public void rightArrowPressed() {
		armorOffset++;
		int armorLength=loader.getItemList(row).Count;
        if (armorOffset+buttons.Count>armorLength-1)//one of them is selected
            armorOffset=Mathf.Max(armorLength-buttons.Count-1, 0);
        Debug.Log("right arrow pressed on row "+row+". armorOffset= "+armorOffset);
        setButtonSprites();
	}

    private void setButtonSprites() {
        int index=0;//don't show the selected item
		Item selected=loader.getPlayerCostume().getArmor(row);
		int selectedIndex=getSelectedIndex();
        
        /*for (int i=0; index<buttons.Count; i++) {
        	int itemIndex=armorOffset+1;
			if (loader.getItemList(row).Count>itemIndex&&itemIndex>=0) {//if there is another sprite to be shown
				if (itemIndex==selectedIndex) {
					continue;//do not increment index
				}
				buttons[index].setSprite(loader.getItem(row, itemIndex));
				buttons[index].setRowAndIndex(this, i);
            }
            else {
                buttons[index].setSprite(null);
				buttons[index].setRowAndIndex(this, i);
			}
            index++;
        }*/
        
        for (int i=0; i<buttons.Count; i++) {
        	buttons[i].setSprite(null);
        	buttons[i].setRowAndIndex(this, i);
        }
        
        index=-armorOffset;
		for (int itemIndex=0; itemIndex<loader.getItemList(row).Count; itemIndex++) {
        	if (itemIndex==selectedIndex) {
        		continue; // do not increment index
        	}
			if (index>=0&&index<buttons.Count) buttons[index].setSprite(loader.getItem(row, itemIndex));
        	index++;
        }
        
		selectedButton.setRowAndIndex(this, -1);
		selectedButton.setSprite(selected);
    }
    
	private int getSelectedIndex() {
		Item selected=loader.getPlayerCostume().getArmor(row);
		int listLength=loader.getItemList(row).Count;
		for (int i=0; i<listLength; i++) {
			if (loader.getItem(row, i)==selected) 
				return i;
		}
		return -1;
    }

    public void switchArmor(int indexInRow) {
		if (indexInRow==-1) //if the selected box is telling me to switch to it
			return;
		int itemIndex=indexInRow+armorOffset;
		if (getSelectedIndex()<=itemIndex) itemIndex++;
        loader.setPlayerItemPiece(row, itemIndex);
		setButtonSprites();
    }

	public void startSwitchingAnimation() {
		selectedButton.startSwitchingAnimation();
	}
	
}
