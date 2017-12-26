using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFood : MonoBehaviour, VariantChangeListener {

	//public float scale;
	private FoodTargetManager foodManager;
	private GameObject baseFoodModel;
	private GameObject clone;
	private Transform container;

	// Use this for initialization
	void Start () {
		//get foodManager from parent
		foodManager = transform.parent.gameObject.GetComponent<FoodTargetManager>();
		//get selected variant model
		baseFoodModel = foodManager.GetSelectedVarModel();
		CreateModel();
	}

	public void UpdateFoodModel (GameObject newFoodModel) {
		if (clone != null)
			Destroy(clone);
		baseFoodModel = newFoodModel;
		CreateModel();
	}

	private void CreateModel () {
		if (container == null)
			container = gameObject.transform;

		float x = container.position.x;
		float y = container.position.y;
		float z = container.position.z;

		clone = Instantiate(baseFoodModel, new Vector3(x, y, z), Quaternion.identity, container);

		//set clone scale to 1
		clone.transform.localScale = new Vector3(1, 1, 1);
		
		Transform baseFoodTransform = baseFoodModel.GetComponent<Transform>();
		if (baseFoodTransform != null) {
			clone.transform.rotation = baseFoodTransform.rotation;
		}

		//fit the box collider to the new model
		FitBoxCollider fit = GetComponent<FitBoxCollider>();
		fit.GetFit(1);
	}

	public void OnModelChange (GameObject gameObject) {
		UpdateFoodModel(gameObject);
	}
}