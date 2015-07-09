using UnityEngine;
using System.Collections;

public class GamePiece : MonoBehaviour {

	private bool affectedByVirus=false;
	public GameObject virusSymbol, redVersion, blueVersion, goldVersion, greenVersion, DDOSRequest, storedRequest;
	private GameObject myVirusSymbol;

	public string color;
	public bool attackableByMalware=true, attackableByVirus=false, attackableByDDOS=true;
	public bool canHoldRequests=false, transportsRequestsNormally, transportsRequestsDiagonally, attractsRequests;
	public GameObject virusMessenger, malwareMessenger;

	private double malwareChance=0.05, virusChance=0.5;
	private ArrayList requests=new ArrayList();
	public string targetAttack="none";
	public int targetX=0, targetY=0;

	private int VIRUS_LENGTH=180;

	private int attackCounter=90, virusCounter=0, DDOSCounter=0, requestProcessCounter=60, requestCooldown=40, requestProcessTime=60;

	virtual public void Update () {
		if ((affectedByVirus||DDOSCounter>0)&&myVirusSymbol==null) myVirusSymbol=(GameObject)Instantiate(virusSymbol, transform.position, Quaternion.identity);
		if (!(affectedByVirus||DDOSCounter>0)) Destroy(myVirusSymbol);

		float distance=0.15f;
		int maxRequests=10;
		for (int y=0; y<5; y++) {
			for (int x=0; x<5; x++) {
				if (requests.Count>x+y*5)
					(requests[x+y*5] as GameObject).transform.position=new Vector3(transform.position.x-distance*(x-2.5f), transform.position.y-distance*(y-2.5f), -0.01f);
			}
		}
		if (requests.Count>maxRequests) {
			DDOSCounter+=requestProcessTime*(requests.Count-maxRequests);
			for (int i=maxRequests; i<requests.Count; i++) {
				GameObject o=requests[i] as GameObject;
				requests.RemoveAt(i);
				GameObject.Destroy(o);
			}
		}

	}
	
	public void simulationUpdate() {
		if (!(haveVirus()||DDOSCounter>0))
			tryToAttack();

		if (DDOSCounter==0) {
			if (--requestProcessCounter<=0){
				if (requests.Count>0) {
					GameObject toDestroy=(requests[0] as GameObject);
					requests.RemoveAt(0);
					Destroy(toDestroy);
				}
				requestProcessCounter=requestProcessTime;
			}
		}
		else {
			DDOSCounter--;
		}
		if (gameObject.GetComponent<CapturableBySurroundings>()!=null)gameObject.GetComponent<CapturableBySurroundings>().simulationUpdate();
	}

	public void postSimulationUpdate() {
		if (gameObject.GetComponent<CapturableBySurroundings>()!=null) gameObject.GetComponent<CapturableBySurroundings>().postSimulationUpdate();
	}

	private bool haveVirus() {
		if (affectedByVirus) {
			if (--virusCounter<0) {
				affectedByVirus=false;
				return false;
			}

			if (virusCounter==VIRUS_LENGTH/2||virusCounter==0) {
				BoardController controller=GameObject.Find("GameController").GetComponent<BoardController>();
				GameObject piece=controller.getPiece((int)(transform.position.x+0.5+1), (int)(transform.position.y+0.5));
				if (piece!=null&&piece.GetComponent<GamePiece>().color.Equals(color)) attackOther("virus", (int)(transform.position.x+0.5+1), (int)(transform.position.y+0.5));

				piece=controller.getPiece((int)(transform.position.x+0.5-1), (int)(transform.position.y+0.5));
				if (piece!=null&&piece.GetComponent<GamePiece>().color.Equals(color)) attackOther("virus", (int)(transform.position.x+0.5-1), (int)(transform.position.y+0.5));

				piece=controller.getPiece((int)(transform.position.x+0.5), (int)(transform.position.y+0.5+1));
				if (piece!=null&&piece.GetComponent<GamePiece>().color.Equals(color)) attackOther("virus", (int)(transform.position.x+0.5), (int)(transform.position.y+0.5+1));

				piece=controller.getPiece((int)(transform.position.x+0.5), (int)(transform.position.y+0.5-1));
				if (piece!=null&&piece.GetComponent<GamePiece>().color.Equals(color)) attackOther("virus", (int)(transform.position.x+0.5), (int)(transform.position.y+0.5-1));
			}
			return true;
		}
		return false;
	}

	private void tryToAttack() {
		if (targetAttack.Equals("none")) attackCounter=90;
		if (--attackCounter<0) {
			attackCounter=90;
			if (targetAttack.Equals("malware")) {
				if (isLegalDiagnol(targetX, targetY)){
					attackOther(targetAttack, targetX, targetY);
					Debug.Log("Attacking...");
				}
				targetAttack="none";
			}
			if (targetAttack.Equals("virus")) {
				if (isLegalDiagnol(targetX, targetY)){
					attackOther(targetAttack, targetX, targetY);
					Debug.Log("Attacking...");
				}
				targetAttack="none";
			}
			if (targetAttack.Equals("DDOS")) {
				if (GameObject.Find("GameController").GetComponent<BoardController>().getPiece(targetX, targetY).GetComponent<GamePiece>().color.Equals(color)) return;
				GameObject request=(GameObject)Instantiate(DDOSRequest, new Vector3(transform.position.x, transform.position.y, transform.position.z-0.1f), Quaternion.identity);
				request.GetComponent<request>().setUpValues(targetX, targetY);
				attackCounter=requestCooldown;
			}
		}
	}

	public void attackWithMalware(string color) {
		if (!attackableByMalware) return;
		if (Random.Range(0.0f, (affectedByVirus||DDOSCounter>0)?0.0f:1.0f)<=malwareChance) {
			GameObject oppisite=(GameObject)Instantiate(getAlternateColorOfMe(color), transform.position, Quaternion.identity);
			GameObject.Find("GameController").GetComponent<BoardController>().createPiece((int)(transform.position.x+0.5), (int)(transform.position.y+0.5), oppisite);
			Destroy(gameObject);
		}
	}
	
	public GameObject getAlternateColorOfMe(string color) {
		switch(color) {
		case "red":
			return redVersion;
		case "blue":
			return blueVersion;
		case "gold":
			return goldVersion;
		case "green":
			return greenVersion;
		}
		return null;
	}

	public void attackWithVirus() {
		if (!attackableByVirus) return;
		if (Random.Range(0.0f, 1.0f)<=virusChance) {
			virusCounter=VIRUS_LENGTH;
			affectedByVirus=true;
		}
	}

	public virtual void attackWithDDOS() {
		if (DDOSCounter==0)requests.Add(Instantiate(storedRequest, transform.position, Quaternion.identity));
		else DDOSCounter+=requestProcessTime;
	}

	public bool canTakeRequest() {
		if (affectedByVirus||DDOSCounter>0)
			return false;
		return true;
	}

	public void setTarget(string targetAttack, int targetX, int targetY) {
		this.targetAttack=targetAttack;
		this.targetX=targetX;
		this.targetY=targetY;
	}

	public GameObject getVirusSymbol() {
		return myVirusSymbol;
	}

	private bool isLegalDiagnol(float targetX, float targetY) {
		return Mathf.Abs(targetX-transform.position.x-0.5f)<=1&&Mathf.Abs(targetY-transform.position.y-0.5f)<=1;
	}

	private void attackOther(string attackType, int x, int y) {
		if (attackType.Equals("virus")){
			GameObject messenger=(GameObject)Instantiate(virusMessenger, new Vector3(transform.position.x, transform.position.y, transform.position.z-0.1f), Quaternion.identity);
			messenger.GetComponent<AttackMessenger>().setValues(x, y, color);
		}
		if (attackType.Equals("malware")) {
			GameObject messenger=(GameObject)Instantiate(malwareMessenger, new Vector3(transform.position.x, transform.position.y, transform.position.z-0.1f), Quaternion.identity);
			messenger.GetComponent<AttackMessenger>().setValues(x, y, color);
		}
	}

	public bool transportsRequestsNormallym() {
		if (affectedByVirus||DDOSCounter>0) {
			return false;
		}
		return transportsRequestsNormally;
	}

	public bool transportsRequestsDiagonallym() {
		if (affectedByVirus||DDOSCounter>0) {
			return false;
		}
		return transportsRequestsDiagonally;
	}

	void OnDestroy() {
		foreach (GameObject o in requests)
			Destroy(o);
		if (myVirusSymbol!=null) Destroy(myVirusSymbol);
	}

	public int getDDOSCounter() {
		return DDOSCounter;
	}
}
