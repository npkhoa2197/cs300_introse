using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetailsButton : ButtonElement {

	private GameObject detailsCanvas;
	private FoodTargetManager foodManager;

	override public void InternalStart() {
		foodManager = transform.parent.parent.parent.GetComponent<FoodTargetManager>();
		//detailsCanvas = transform.parent.parent.parent.Find("DetailsCanvas").gameObject;
	}
	
	override public void InternalActivate() {
		//detailsCanvas.SetActive(true);
		GlobalContentProvider.Instance.currentFoodManager = foodManager;
		SceneManager.LoadScene("DetailsScene", LoadSceneMode.Additive);
	}

	override public void InternalDeactivate() {
		//detailsCanvas.SetActive(false);
	}
}
