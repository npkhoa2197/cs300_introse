using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeButton : ButtonElement {

	private GameObject selectCircle;

	override public void InternalStart() {
		selectCircle = transform.parent.parent.parent.Find("SelectCircle").gameObject;
	}
	
	override public void InternalActivate() {
		selectCircle.SetActive(true);
	}

	override public void InternalDeactivate() {
		selectCircle.SetActive(false);
	}
}
