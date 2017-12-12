using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached to the Order button
public class OnOrderTouch : MonoBehaviour, ButtonElement {

	private GameObject orderCanvas;
	private ButtonManager btnManager;

	void Start () {
		orderCanvas = GameObject.Find ("OrderCanvas");
		btnManager = transform.parent.GetComponent<ButtonManager>();
	}

	void OnMouseUpAsButton () {
		btnManager.OnButtonClick(this, false);
	}

	public void Activate() {
		orderCanvas.SetActive(true);
	}

	public void Deactivate() {
		orderCanvas.SetActive(false);
	}
}