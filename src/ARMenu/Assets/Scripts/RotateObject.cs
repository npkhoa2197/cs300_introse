using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script allows 3D objects with a box collider
//to rotate horizontally
public class RotateObject : MonoBehaviour {
	//code adapted from https://www.youtube.com/watch?v=S3pjBQObC90

	public float rotSpeed = 100;

	private Rigidbody rigidbody;

	void Start () {
		rigidbody = GetComponent<Rigidbody>();
	}

	void OnMouseDrag () {
		//Only rotate horizontally
		float rotX = Input.GetAxis("Horizontal") * rotSpeed;// * Mathf.Deg2Rad;

		rigidbody.AddTorque(transform.up * rotX);
	}
}
