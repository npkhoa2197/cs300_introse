using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached to the Customize button
public class OnCustomizeTouch : MonoBehaviour {
	void OnMouseDown () {
		GetVariant getVariant = GetComponent<GetVariant>();

		getVariant.NextVariant();

		Debug.Log("Customize!");
	}
	
}
