using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {
	//code adapted from https://www.youtube.com/watch?v=S3pjBQObC90

	float rotSpeed = 100;

	void OnMouseDrag () {
		//Only rotate horizontally
		float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;

		transform.Rotate(Vector3.up * -rotX);
	}

}
