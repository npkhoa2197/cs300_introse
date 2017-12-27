using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShareButton : ButtonElement {

	private GameObject reviewCanvas;
	private FoodTargetManager foodManager;

	override public void InternalStart() {
		foodManager = transform.parent.parent.parent.GetComponent<FoodTargetManager>();
		//reviewCanvas = transform.parent.parent.parent.Find("ReviewCanvas").gameObject;
	}
	
	override public void InternalActivate() {
		GlobalContentProvider.Instance.currentFoodManager = foodManager;
		SceneManager.LoadScene("ReviewScene", LoadSceneMode.Additive);
		//reviewCanvas.SetActive(true);
	}

	override public void InternalDeactivate() {
		//reviewCanvas.SetActive(false);
	}
}
