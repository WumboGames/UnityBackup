using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
    public ArmorRow row;
    public bool leftArrow=true;

    void OnMouseDown() {
        if (leftArrow)
            row.leftArrowPressed();
        else
            row.rightArrowPressed();
    }
}
