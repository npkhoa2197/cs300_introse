using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StillObject : MonoBehaviour {

	Quaternion fixedRotation;
	Vector3 fixedPosition;

	void Awake () {
		fixedRotation = transform.rotation;
		fixedPosition = transform.position;
	}
	
	void LateUpdate () {
		transform.rotation = fixedRotation;
		transform.position = fixedPosition;
	}
}
