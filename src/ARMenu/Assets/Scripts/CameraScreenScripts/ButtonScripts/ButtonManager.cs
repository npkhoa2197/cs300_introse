using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour {

	//this class is used to manage the state of current activated button
	private ButtonElement currentBtn;

	void Start() {
		currentBtn = null;
	}

	//true: btnManager save the activate state of the button
	//false: btnManager doesn't save the activate state of the button
	public void OnButtonClick(ButtonElement btn, bool saveState) {
		//if there are no current activated button -> activate the given button and save it
		if (currentBtn == null) {
			btn.Activate();
			if (saveState) {
				currentBtn = btn;
			}
		}
		//if the current activate btn is the same as the given one
		//-> deactivate it and remove the saved btn
		else if (currentBtn == btn) {
			currentBtn.Deactivate();
			currentBtn = null;
		}
		//if the current activate btn is not the same
		//-> deactivate the current, save the given and activate it
		else {
			currentBtn.Deactivate();
			btn.Activate();
			if (saveState) {
				currentBtn = btn;
			}
		}
	}
}