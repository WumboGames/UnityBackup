using UnityEngine;
using System.Collections;

public class CreateCharacterController : MonoBehaviour {

	public string playerName;
	public int bodyIndex, handsIndex, feetIndex;

	public void createCharacterButtonPressed() {
		string username=PlayerPrefs.GetString("current username");
		int players=PlayerPrefs.GetInt(username+" players:", -9999999);
		if (players>=4) {
			Debug.Log("Player not created: this login alread has four players.");
			return;
		}
		Debug.Log(username+" "+players+" "+playerName+" "+bodyIndex+" "+feetIndex+" "+handsIndex);
		
		PlayerPrefs.SetInt(username+" players:", players+1);//we are adding a player
		PlayerPrefs.SetString(username+" playerIndex:"+players+" playerName:", playerName);
		PlayerPrefs.SetInt(username+" playerIndex:"+players+" bodyIndex:", bodyIndex);
		PlayerPrefs.SetInt(username+" playerIndex:"+players+" handsIndex:", handsIndex);
		PlayerPrefs.SetInt(username+" playerIndex:"+players+" feetIndex:", feetIndex);
		
		for (int i=0; i<8; i++)
			PlayerPrefs.SetInt(username+" playerIndex:"+players+" rowIndex: "+i+" itemIndex:", 0);//set all armor to none
		
		PlayerPrefs.SetInt(username+" playerIndex:"+players+" level:", 1);
		PlayerPrefs.SetInt(username+" playerIndex:"+players+" exp:", 0);
		
		PlayerPrefs.Save();
		
		Application.LoadLevel("ChooseCharacterScreen");
	}
}
