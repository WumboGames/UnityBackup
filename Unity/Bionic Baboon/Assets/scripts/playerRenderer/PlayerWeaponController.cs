using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour {
	private int weapon=0;//0==none, 1==primary, 2==secondary;
	private Animator animator;
	private bool switchedWeapons=false;

	void Start() {
		animator=GetComponent<Animator>();
	}

	void Update() {
		switchWeaponsIfKeyIsPressed();
	}

	void LateUpdate() {
		pointWeaponAtCursor();
		switchedWeapons=false;
	}

	public void putAwayWeapon() {
		switchToWeapon(0);
	}

	private void switchWeaponsIfKeyIsPressed() {
		if (Input.GetKeyDown(KeyCode.Alpha1)&&weapon!=1)
			switchToWeapon(1);
		if (Input.GetKeyDown(KeyCode.Alpha2)&&weapon!=2)
			switchToWeapon(2);
		if (Input.GetKeyDown(KeyCode.Alpha3)&&weapon!=0)
			switchToWeapon(0);
	}

	private void switchToWeapon(int weapon) {
		if (weapon==this.weapon)
			return;

		animator.SetInteger("currentWeaponIndex", weapon);
		switchedWeapons=true;
		
		this.weapon=weapon;
	}


	private void pointWeaponAtCursor() {
		if (switchedWeapons||!holdingWeapon())//holding weapon check is needed to let the animator catch up
			return;//I don't need to point a weapon if I am not holding one
		Vector3 cameraLoation=Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 playerLocation=transform.position+Vector3.up*0.5f;
		Vector3 difference=cameraLoation-playerLocation;
		float theta=Mathf.Rad2Deg*Mathf.Atan(difference.y/difference.x);
		if (difference.x<0)
			theta*=-1;
		float time=(theta+90)/180+0.01f;
		animator.Play(animator.GetCurrentAnimatorStateInfo(1).fullPathHash, 1, time);
	}

	private bool holdingWeapon() {
		AnimatorStateInfo stateInfo=animator.GetCurrentAnimatorStateInfo(1);//1 is weapons layer
		return stateInfo.IsName("holdingSecondary")||stateInfo.IsName("holdingPrimary");
	}

}
