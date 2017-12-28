using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonPanel : MonoBehaviour, FoodManagerListener {

	private Button detailsBtn;
	private Button customBtn;
	private Button orderBtn;
	private Button reviewBtn;
	private Animator panelAnim;
	private GlobalContentProvider provider;
	private bool isCircleActive;

	// Use this for initialization
	void Start () {
		detailsBtn = transform.Find("DetailsBtn").GetComponent<Button>();
		customBtn =  transform.Find("CustomizeBtn").GetComponent<Button>();
		orderBtn = transform.Find("OrderBtn").GetComponent<Button>();
		reviewBtn = transform.Find("ReviewBtn").GetComponent<Button>();
		panelAnim = GetComponent<Animator>();
		provider = GlobalContentProvider.Instance;
		provider.AddManagerListener(this);

		//set onClick
		detailsBtn.onClick.AddListener(OnDetailsClick);
		customBtn.onClick.AddListener(OnCustomClick);
		orderBtn.onClick.AddListener(OnOrderClick);
		reviewBtn.onClick.AddListener(OnReviewClick);

		isCircleActive = false;
	}

	void OnDetailsClick() {
		if (isCircleActive) {
			provider.selectionCircle.SetActive(false);
			isCircleActive = false;
		}
		SceneManager.LoadScene("DetailsScene", LoadSceneMode.Additive);
	}

	void OnCustomClick() {
		if (provider.selectionCircle != null) {
			if (!isCircleActive) {
				provider.selectionCircle.SetActive(true);
				isCircleActive = true;
			}
			else {
				provider.selectionCircle.SetActive(false);
				isCircleActive = false;
			}
		}
	}

	void OnOrderClick() {
		if (isCircleActive) {
			provider.selectionCircle.SetActive(false);
			isCircleActive = false;
		}
		SceneManager.LoadScene("OrderScene", LoadSceneMode.Additive);
	}

	void OnReviewClick() {
		if (isCircleActive) {
			provider.selectionCircle.SetActive(false);
			isCircleActive = false;
		}
		SceneManager.LoadScene("ReviewScene", LoadSceneMode.Additive);
	}

	void OnDestroy() {
		provider.RemoveManagerListener(this);
	}

	public void OnNewFoodManager() {
		panelAnim.Play("PanelAppears");
		detailsBtn.interactable = true;
		customBtn.interactable = true;
		orderBtn.interactable = true;
		reviewBtn.interactable = true;
	}

	public void OnFoodManagerLost() {
		detailsBtn.interactable = false;
		customBtn.interactable = false;
		orderBtn.interactable = false;
		reviewBtn.interactable = false;
		panelAnim.Play("PanelDisappears");
		isCircleActive = false;
	}
}
