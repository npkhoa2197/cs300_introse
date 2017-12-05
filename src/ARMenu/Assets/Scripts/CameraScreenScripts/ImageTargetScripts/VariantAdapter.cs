using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariantAdapter : MonoBehaviour {

	public int foodObjectIndex;
	public int selected = 0;
	
	private FoodObject foodObject;
	private Variant[] variants;

	void Awake() {
		GlobalContentProvider contentProvider = GlobalContentProvider.Instance;
		foodObject = contentProvider.foods[foodObjectIndex];
		variants = foodObject.variants;
	}
	
	public FoodObject GetFoodObject() {
		return foodObject;
	}

	public void SelectVariant (int position) {
		if (position < 0 || position >= variants.Length)
			return;

		if (selected == position)
			return;

		selected = position;

		//notify all classes implement VariantChangeListener
		NotifyChange();
	}

	public string GetSelectedVarName () {
		return variants[selected].variantName;
	}

	public GameObject GetSelectedVarModel () {
		return variants[selected].variantModel;
	}

	public GameObject[] GetVarModels() {
		GameObject[] models = new GameObject[variants.Length];
		for (int i = 0; i < models.Length; ++i) {
			models[i] = variants[i].variantModel;
		}

		return models;
	}

	void NotifyChange () {
		foreach (VariantChangeListener listener in GetComponentsInChildren<VariantChangeListener>()) {
			listener.OnModelChange(GetSelectedVarModel());
		}
	}
}
