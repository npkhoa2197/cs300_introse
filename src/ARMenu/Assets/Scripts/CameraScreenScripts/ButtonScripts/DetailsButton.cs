using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsButton : ButtonElement {

	private GameObject detailsCanvas;

	override public void InternalStart() {
		detailsCanvas = transform.parent.parent.parent.Find("DetailsCanvas").gameObject;
	}
	
	override public void InternalActivate() {
		detailsCanvas.SetActive(true);
	}

	override public void InternalDeactivate() {
		detailsCanvas.SetActive(false);
	}
}
