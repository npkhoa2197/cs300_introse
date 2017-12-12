using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTargetManager : MonoBehaviour {

	//index of FoodObject in Content Provider
	public int foodObjectIndex;
	//selected food variant
	public int selected = 0;
	
	private FoodObject foodObject;
	private Variant[] variants;

	void Awake() {
		GlobalContentProvider contentProvider = GlobalContentProvider.Instance;
		foodObject = contentProvider.foods[foodObjectIndex];
		variants = foodObject.variants;
	}

	public string GetFoodName() {
		return foodObject.foodName;
	}

	public double GetFoodPrice() {
		return foodObject.price;
	}

	public string GetFoodDescription() {
		return foodObject.description;
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