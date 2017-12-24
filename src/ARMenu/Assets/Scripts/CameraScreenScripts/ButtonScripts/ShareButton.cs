using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareButton : ButtonElement {

	private GameObject reviewCanvas;

	override public void InternalStart() {
		reviewCanvas = transform.parent.parent.parent.Find("ReviewCanvas").gameObject;
	}
	
	override public void InternalActivate() {
		reviewCanvas.SetActive(true);
	}

	override public void InternalDeactivate() {
		reviewCanvas.SetActive(false);
	}
}
