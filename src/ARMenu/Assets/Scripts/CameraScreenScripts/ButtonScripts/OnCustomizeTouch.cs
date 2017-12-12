using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached to the Customize button
public class OnCustomizeTouch : MonoBehaviour {

	private GameObject selectCircle;
	private bool isCustomizing = false;

	void Start () {
		selectCircle = transform.parent.Find("SelectCircle").gameObject;
	}

	void OnMouseUpAsButton () {
		isCustomizing = !isCustomizing;
		selectCircle.SetActive(isCustomizing);
	}
}