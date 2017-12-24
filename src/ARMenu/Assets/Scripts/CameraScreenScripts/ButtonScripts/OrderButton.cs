using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderButton : ButtonElement {

	private GameObject orderCanvas;

	override public void InternalStart() {
		orderCanvas = transform.parent.parent.parent.Find("OrderCanvas").gameObject;
	}
	
	override public void InternalActivate() {
		orderCanvas.SetActive(true);
	}

	override public void InternalDeactivate() {
		orderCanvas.SetActive(false);
	}
}
