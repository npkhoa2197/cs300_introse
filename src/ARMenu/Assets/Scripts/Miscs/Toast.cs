using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour {

	private Animator toastAnim;
	private Text toastText;

	// Use this for initialization
	void Start () {
		toastAnim = GetComponent<Animator>();
		toastText = transform.Find("Text").GetComponent<Text>();
	}

	public void ShowText(string text) {
		toastText.text = text;
		toastAnim.Play("ToastAppears");
	}
}
