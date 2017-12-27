using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderButton : ButtonElement {

	private GameObject orderCanvas;
	private FoodTargetManager foodManager;

	override public void InternalStart() {
		foodManager = transform.parent.parent.parent.GetComponent<FoodTargetManager>();
		//orderCanvas = transform.parent.parent.parent.Find("OrderCanvas").gameObject;
	}
	
	override public void InternalActivate() {
		GlobalContentProvider.Instance.currentFoodManager = foodManager;
		SceneManager.LoadScene("OrderScene", LoadSceneMode.Additive);
		//orderCanvas.SetActive(true);
	}

	override public void InternalDeactivate() {
		//orderCanvas.SetActive(false);
	}
}
