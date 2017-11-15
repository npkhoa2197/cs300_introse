using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script allows 3D objects with a box collider
//to rotate horizontally
public class RotateObject : MonoBehaviour {

	public float amount = 20f;

	void OnMouseDrag () {
		//Only rotate horizontally
		float h = Input.GetAxis("Mouse X") * amount * Mathf.Deg2Rad;
        transform.Rotate(transform.up * -h);
	}
}
