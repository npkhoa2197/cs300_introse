using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitBoxCollider : MonoBehaviour {

	private BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
			boxCollider = gameObject.AddComponent<BoxCollider>();

		Bounds outBound = GetComponent<Renderer>().bounds;
		foreach(Renderer r in GetComponentsInChildren<Renderer>()) {
			Debug.Log(r.bounds);
			outBound.Encapsulate(r.bounds);
		}

		Debug.Log(outBound);
	}
}
