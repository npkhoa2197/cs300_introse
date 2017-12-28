using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class HomeControl : MonoBehaviour {

	public void GoToCamera() {
		// enable camera screen
		XRSettings.enabled = true;
		SceneManager.LoadScene("CameraScreen");
	}

	public void GoToMenu() {
		SceneManager.LoadScene("MenuScreen");
	}
}
