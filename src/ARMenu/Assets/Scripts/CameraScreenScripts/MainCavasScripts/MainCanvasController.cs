using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasController : MonoBehaviour {

	private Button backBtn;
	private Button historyBtn;
	public GameObject historyCanvas;

	// Use this for initialization
	void Start () {
		backBtn = transform.Find("BackToMenu").gameObject.GetComponent<Button>();
		historyBtn = transform.Find("OrderHistory").gameObject.GetComponent<Button>();
		backBtn.onClick.AddListener(OnBackClick);
		historyBtn.onClick.AddListener(OnHistoryClick);
	}
	
	void OnBackClick() {
		SceneManager.LoadScene("HomeScreen");
	}

	void OnHistoryClick() {
		historyCanvas.SetActive(true);
	}
}
