using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeControl : MonoBehaviour {

	public void GoToCamera() {
		SceneManager.LoadScene("CameraScreen");
	}

	public void GoToMenu() {
		SceneManager.LoadScene("MenuScreen");
	}
}
