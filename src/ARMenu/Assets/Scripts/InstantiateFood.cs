using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateFood : MonoBehaviour {

	public GameObject defaultFoodModel;
	//public float scale;

	private GameObject clone;
	private Transform container;

	// Use this for initialization
	void Start () {
		//defaultFoodModel.transform.localScale = new Vector3(scale, scale, scale);
		CreateModel();
	}

	public void UpdateFoodModel (GameObject newFoodModel) {
		if (clone != null)
			Destroy(clone);
		defaultFoodModel = newFoodModel;
		CreateModel();
	}

	private void CreateModel () {
		if (container == null)
			container = gameObject.transform;

		float x = container.position.x;
		float y = container.position.y;
		float z = container.position.z;

		clone = Instantiate(defaultFoodModel, new Vector3(x, y, z), Quaternion.identity, container);
	}
}
