using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachToGUI : MonoBehaviour {

	public Canvas canvas;
	
	// Update is called once per frame
	void Update () {
		Vector3 canvasPos = Camera.main.WorldToScreenPoint(this.transform.position);
		canvas.transform.position = canvasPos;
	}
}