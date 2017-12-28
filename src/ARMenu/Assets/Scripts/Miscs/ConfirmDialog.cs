using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ConfirmDialog : MonoBehaviour {

	private Button backgroundBtn;
	private Button yesBtn;
	private Button noBtn;
	private Action confirmAction;
	private Animator dialogAnim;

	void Start() {
		backgroundBtn = transform.Find("Background").GetComponent<Button>();
		yesBtn = transform.Find("Image/Yes").GetComponent<Button>();
		noBtn = transform.Find("Image/No").GetComponent<Button>();
		dialogAnim = GetComponent<Animator>();

		yesBtn.onClick.AddListener(OnYesClick);
		noBtn.onClick.AddListener(OnNoClick);
		backgroundBtn.onClick.AddListener(OnBackgroundClick);

		gameObject.SetActive(false);
	}

	public void Confirm(Action confirmAction) {
		this.confirmAction = confirmAction;
		gameObject.SetActive(true);
		dialogAnim.Play("ConfirmDialogAppears");
	}

	void OnYesClick() {
		if (confirmAction != null) {
			confirmAction();
			confirmAction = null;
		}
		gameObject.SetActive(false);
	}
	
	void OnNoClick() {
		confirmAction = null;
		gameObject.SetActive(false);
	}

	void OnBackgroundClick() {
		confirmAction = null;
		gameObject.SetActive(false);
	}
}
