using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariantAdapter : MonoBehaviour {

	public GameObject[] variantModels;
	public string[] variantNames;
	public int selected = 0;
	
	public void SelectVariant (int position) {
		if (position < 0 || position >= variantNames.Length)
			return;

		if (selected == position)
			return;

		selected = position;

		//notify all classes implement VariantChangeListener
		NotifyChange();
	}

	public string GetSelectedVarName () {
		return variantNames[selected];
	}

	public GameObject GetSelectedVarModel () {
		return variantModels[selected];
	}

	void NotifyChange () {
		foreach (VariantChangeListener listener in GetComponentsInChildren<VariantChangeListener>()) {
			listener.OnModelChange(GetSelectedVarModel());
		}
	}
}
