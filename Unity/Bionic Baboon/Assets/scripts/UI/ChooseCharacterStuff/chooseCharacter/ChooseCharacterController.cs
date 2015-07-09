using UnityEngine;
using System.Collections;

public class ChooseCharacterController : MonoBehaviour {

	public GameObject characterNameButtonPrefab;
	public PlayerCostume playerCostume;
	
	private PlayerLibrary library;
	private GameObject[] characterNameButtons;
	private string username;
	private Vector3 playerCostumePosition;
	
	private bool deleteButtonPressed=false;
	
	void Start() {
		playerCostumePosition=playerCostume.transform.position;
		library=GameObject.Find("GameLoader").GetComponent<PlayerLibrary>();
		loadButtons ();
		showCharacter(0);
	}
	
	public void showCharacter(int playerIndex) {
		if (playerIndex>=PlayerPrefs.GetInt(username+" players:")) {
			playerCostume.transform.position=new Vector3(9999999999, 0, 0);
			return;
		}
		playerCostume.transform.position=playerCostumePosition;
		int bodyIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" bodyIndex:");
		int handsIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" handsIndex:");
		int feetIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" feetIndex:");
		playerCostume.setAllSprites(library.bodySprites[bodyIndex], library.leftHandSprites[handsIndex], library.rightHandSprites[handsIndex], 
				library.leftFootSprites[feetIndex], library.rightFootSprites[feetIndex]);
	}
	
	public void characterClicked(int playerIndex) {
		PlayerPrefs.SetInt(username+" current playerIndex", playerIndex);
		Application.LoadLevel("ChangeArmorAndWeaponsScreen");
	}
	
	private void loadButtons() {
		username=PlayerPrefs.GetString("current username");
		int numberOfPlayers=PlayerPrefs.GetInt(username+" players:");
		characterNameButtons=new GameObject[numberOfPlayers];
		
		for (int i=0; i<numberOfPlayers; i++) {
			Vector3 position=new Vector3(4, .8f, 0)+i*Vector3.down*1.5f;
			GameObject current=(GameObject)GameObject.Instantiate(characterNameButtonPrefab.gameObject, position, Quaternion.identity);
			
			current.GetComponent<ShowsCharacterOnMouseOver>().setPlayerIndex(i);
			current.GetComponentInChildren<TextMesh>().text=PlayerPrefs.GetString(username+" playerIndex:"+i+" playerName:");
			current.GetComponent<ShowsCharacterOnMouseOver>().hideDeleteButton();
			characterNameButtons[i]=current;
		}
	}
	
	public void deleteCharacter(int playerIndex) {
		if (!deleteButtonPressed) 
			return;
		
		string username=PlayerPrefs.GetString("current username");
		int players=PlayerPrefs.GetInt(username+" players:");
		
		//delete this character's data
		deleteInfoForPlayer(username, playerIndex);
		
		//copies the data for all following players to the player one before
		for (playerIndex++; playerIndex<players; playerIndex++) {
			int bodyIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" bodyIndex:");
			int handsIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" handsIndex:");
			int feetIndex=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" feetIndex:");
			int level=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" level:");
			int exp=PlayerPrefs.GetInt(username+" playerIndex:"+playerIndex+" exp:");
			PlayerPrefs.SetInt(username+" playerIndex:"+(playerIndex-1)+" bodyIndex:", bodyIndex);
			PlayerPrefs.SetInt(username+" playerIndex:"+(playerIndex-1)+" handsIndex:", handsIndex);
			PlayerPrefs.SetInt(username+" playerIndex:"+(playerIndex-1)+" feetIndex:", feetIndex);
			PlayerPrefs.SetInt(username+" playerIndex:"+(playerIndex-1)+" level:", level);
			PlayerPrefs.SetInt(username+" playerIndex:"+(playerIndex-1)+" exp:", exp);
			
			for (int i=0; i<8; i++) {
				PlayerPrefs.SetInt(username+" playerIndex:"+(playerIndex-1)+" rowIndex:", i);
			}
			
			deleteInfoForPlayer(username, playerIndex);
		}
		
		PlayerPrefs.SetInt(username+" players:", players-1);
		Application.LoadLevel("ChooseCharacterScreen");
	}
	
	private void deleteInfoForPlayer(string username, int playerIndex) {
		PlayerPrefs.DeleteKey(username+" playerIndex:"+playerIndex+" bodyIndex:");
		PlayerPrefs.DeleteKey(username+" playerIndex:"+playerIndex+" handsIndex:");
		PlayerPrefs.DeleteKey(username+" playerIndex:"+playerIndex+" feetIndex:");
		PlayerPrefs.DeleteKey(username+" playerIndex:"+playerIndex+" level:");
		PlayerPrefs.DeleteKey(username+" playerIndex:"+playerIndex+" exp:");
		for (int i=0; i<8; i++)
			PlayerPrefs.DeleteKey(username+" playerIndex:"+playerIndex+" rowIndex:"+i);
	}
	
	public void deleteButtonClicked() {
		deleteButtonPressed=!deleteButtonPressed;
		Debug.Log("deleteButtonClicked: "+deleteButtonPressed);
		foreach (GameObject o in characterNameButtons)
			if (deleteButtonPressed)
				o.GetComponent<ShowsCharacterOnMouseOver>().showDeleteButton();
			else
				o.GetComponent<ShowsCharacterOnMouseOver>().hideDeleteButton();
	}
}
