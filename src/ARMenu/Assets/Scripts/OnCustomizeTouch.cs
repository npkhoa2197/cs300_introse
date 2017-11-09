using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached to the Customize button
public class OnCustomizeTouch : MonoBehaviour {

	private GetVariant getVariant = null;

	void Start () {
		//get parent imageTarget which contains the button
		GameObject parentTarget = gameObject.transform.parent.gameObject;

		if (parentTarget != null) {
			getVariant = parentTarget.GetComponent<GetVariant>();
		}
	}

	void OnMouseDown () {
		if (getVariant != null) {
			getVariant.NextVariant();
		}
	}
	
}
