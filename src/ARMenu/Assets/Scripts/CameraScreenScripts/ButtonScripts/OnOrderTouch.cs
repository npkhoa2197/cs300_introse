using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached to the Order button
public class OnOrderTouch : MonoBehaviour {

	private GameObject orderCanvas;
	private bool isOrdering = false;

	void Start () {
		orderCanvas = GameObject.Find ("OrderCanvas");
	}

	void OnMouseUpAsButton () {
		orderCanvas.SetActive(true);
	}
}
