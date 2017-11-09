using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is placed under each ImageTarget
//and acts as an array of 3D models
//this script allows changing to different models
//and get info of the selected model
public class GetVariant : MonoBehaviour {

	private int selected = 0;
	public GameObject[] foodModels;
	
	//when script gets initialized
	//hide all models which are not selected
	void Start () {
		for (int i = 0; i < foodModels.Length; ++i) {
			if (i == selected)
				continue;

			foodModels[i].SetActive(false);
		}
	}

	public void SwitchToVariant (int position) {
		if (position >= foodModels.Length) {
			position = 0;
		}
		else if (position < 0) {
			position = foodModels.Length - 1;
		}

		foodModels[selected].SetActive(false);
		foodModels[selected = position].SetActive(true);
	}

	public void NextVariant () {
		SwitchToVariant(selected + 1);
	}

	public void PreviousVariant () {
		SwitchToVariant(selected - 1);
	}

	public void PrintInfo () {	
		GetFoodInfo getFoodInfo = foodModels[selected].GetComponent(typeof(GetFoodInfo)) as GetFoodInfo;

		if (getFoodInfo != null) {
			Debug.Log(getFoodInfo.getFoodName() + " " + getFoodInfo.getVariant());
		}
	}

	public int Count() {
		return foodModels.Length;
	}
}
