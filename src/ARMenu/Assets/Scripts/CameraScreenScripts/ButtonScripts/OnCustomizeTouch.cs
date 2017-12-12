using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached to the Customize button
public class OnCustomizeTouch : MonoBehaviour, ButtonElement {

	private GameObject selectCircle;
	private ButtonManager btnManager;

	void Start () {
		selectCircle = transform.parent.parent.Find("SelectCircle").gameObject;
		btnManager = transform.parent.GetComponent<ButtonManager>();
	}

	void OnMouseUpAsButton () {
		btnManager.OnButtonClick(this, true);
	}

	public void Activate() {
		selectCircle.SetActive(true);
	}

	public void Deactivate() {
		selectCircle.SetActive(false);
	}
}