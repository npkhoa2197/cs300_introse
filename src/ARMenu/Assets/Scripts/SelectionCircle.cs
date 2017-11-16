using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCircle : MonoBehaviour {

	public GameObject testModel;
	public int objectCount = 5;
	public float radius = 10f;
	public float itemScale = 0.3f;

	// Use this for initialization
	void Start () {
		Transform container = gameObject.transform;
		float x = container.position.x;
		float y = container.position.y;
		float z = container.position.z;
		Vector3 scaleVector = new Vector3(itemScale, itemScale, itemScale);

		for (int i = 0; i < objectCount; i++) {
	        float angle = i * Mathf.PI * 2 / objectCount;
	        Vector3 pos = new Vector3(x, y, z);
	        pos += new Vector3(Mathf.Cos(angle), y, Mathf.Sin(angle)) * radius;
	        GameObject clone = Instantiate(testModel, pos, Quaternion.identity, container);
	        clone.transform.localScale = scaleVector;
    	}
	}
}
