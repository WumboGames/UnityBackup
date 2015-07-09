using UnityEngine;
using System.Collections;

public class IntroCameraMovement : MonoBehaviour {
	
	private int section=-5;
	private float SPEED=0.01f, RESIZE_SPEED=0.002f;
	private int counter=0;

	private int updatesCounted=0, maxUpdatesCounted=2;

	void Start() {
		Application.targetFrameRate=60;
	}

	void Update () {
		
		if (counter>100) {
			counter=-1;
			section++;
		}
		counter++;

		switch(section) {
		case 0: 
			transform.Translate(Vector3.down*SPEED+Vector3.left*SPEED);
			Camera c=gameObject.GetComponent<Camera>();
			if (c!=null) c.orthographicSize=c.orthographicSize+RESIZE_SPEED;
			break;
		case 1:
			c=gameObject.GetComponent<Camera>();
			if (c!=null) c.orthographicSize=c.orthographicSize+RESIZE_SPEED;
			transform.Translate(Vector3.down*SPEED);
			break;
		case 2:
			transform.Translate(Vector3.right*SPEED);
			break;
		case 3:
			transform.Translate(Vector3.right*SPEED);
			break;
		case 4:
			transform.Translate(Vector3.right*SPEED+Vector3.up*SPEED);c=gameObject.GetComponent<Camera>();
			if (c!=null) c.orthographicSize=c.orthographicSize-RESIZE_SPEED;
			break;
		case 5:
			transform.Translate(Vector3.up*SPEED+Vector3.left*SPEED);
			c=gameObject.GetComponent<Camera>();
			if (c!=null) c.orthographicSize=c.orthographicSize-RESIZE_SPEED;
			break;
		case 7:
			transform.Translate(Vector3.right*SPEED);
			if (counter>93) counter=101;
			break;
		case 8:
			transform.position=new Vector3(1.50f, transform.position.y, transform.position.z);
			break;
		case 12:
			counter=0;
			c=gameObject.GetComponent<Camera>();
			if (c==null) return;
			if (c.orthographicSize<2.6) c.orthographicSize+=RESIZE_SPEED*3;
			if (transform.position.x>0) transform.Translate(Vector3.left*SPEED);
			if (transform.position.y>-0.5) transform.Translate(Vector3.down*SPEED);

			break;
		}
		if (++updatesCounted<maxUpdatesCounted) {
			Update ();
		}
		else{
			updatesCounted=0;
		}
	}
}
