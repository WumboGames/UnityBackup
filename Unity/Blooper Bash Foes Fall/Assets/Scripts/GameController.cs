using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	public int wave=1;
	private string preWaveText="<size="+Screen.width/15+"><b>", postWaveText="</b></size>";
	private string prePlayerInfoText="<size="+Screen.width/30+"><b>", postPlayerInfoText="</b></size>";
	private string playerInfoString="";
	
	private ArrayList enemies=new ArrayList();
	public GameObject swordEnemy, starEnemy, player;
	private int countDownTimer=20;
	
	void Start() {
		StartCoroutine("CountTimer");
		Application.targetFrameRate=60;
	}
	
	void Update() {
		if (wave==1) {
			playerInfoString="Good Luck";
			if (countDownTimer>15) playerInfoString="Move With WASD/Arrow Keys.";
			if (countDownTimer<=15&&countDownTimer>10) playerInfoString="Double jump by pressing up or space in the air.";
			if (countDownTimer<=10&&countDownTimer>3) playerInfoString="Click to attack.";
			if (countDownTimer==-1) playerInfoString="";
		}
		else playerInfoString="";
	}
	
	private void spawnEnimies() {
		switch(wave) {
		case 1: 
			enemies.Add(Instantiate(swordEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			break;
		case 2:
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			break;
		case 3:
			enemies.Add(Instantiate(swordEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			break;
		case 4: 
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			break;
		case 5:
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			break;
		case 6:
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			break;
		case 7:
		case 8:
		case 9:
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			break;
		default:
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(swordEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(8, 14, 0), Quaternion.identity));
			enemies.Add(Instantiate(starEnemy, new Vector3(-8, 14, 0), Quaternion.identity));
			break;
			
		}
		foreach(GameObject o in enemies) {
			o.GetComponent<swordEnemy>().SetValues(player, this);
		}
	}
	
	private IEnumerator CountTimer() {
		while (true) {
			if (countDownTimer!=-1) {
				countDownTimer-=1;
				if (countDownTimer==-1) spawnEnimies();
			}
			else {
				if (enemies.Count==0)  {
					countDownTimer=10;
					wave++;
				}
			}
			yield return new WaitForSeconds(1f);
		}
	}
	
	void OnGUI() {
		Rect currentWave = new Rect (Screen.width/15, Screen.height/14, Screen.width*14/15, Screen.height*13/14);
		GUIStyle style = GUI.skin.GetStyle ("Label");
		style.normal.textColor=Color.red;
		
		style.alignment=TextAnchor.UpperCenter;
		GUI.Label (currentWave,  prePlayerInfoText+playerInfoString+postPlayerInfoText);
		
		style.alignment = TextAnchor.LowerRight;
		string waveString="";
		if (countDownTimer!=-1) {
			waveString="Next wave in: "+countDownTimer;
		}
		else {
			waveString="Wave "+wave;
		}
		GUI.Label (currentWave,  preWaveText+waveString+postWaveText);
	}
	
	public void remove(GameObject o) {
		enemies.Remove(o);
	}
}
