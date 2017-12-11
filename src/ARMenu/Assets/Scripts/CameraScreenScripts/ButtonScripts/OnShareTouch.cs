using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the script is attached to the Share button
public class OnShareTouch : MonoBehaviour, ButtonElement {

	void OnMouseUpAsButton () {
		Debug.Log("Share!");
	}
	
	public void Activate() {

	}

	public void Deactivate() {

	}
}
