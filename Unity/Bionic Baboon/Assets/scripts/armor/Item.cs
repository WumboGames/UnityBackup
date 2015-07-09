using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
	public Sprite squareIcon;
	public GameObject renderedArmor;//can be just a spriterenderer, or something with particle effects
	public GameObject renderedRightBoot;//only if this is a pair of boots, otherwise this should be null

	public new string name="";
	public string description="";
	public int unlockLevel;
	public string rarity="grey";//grey, blue, green, gold, red
	public float stat1;//how much damage is blocked
	public string stat1Name="Defense";
	public float stat2;//jump multiplier
	public string stat2Name="Weight";
	public float stat3;//speed multiplier	
	public string stat3Name="Comfort";
}
