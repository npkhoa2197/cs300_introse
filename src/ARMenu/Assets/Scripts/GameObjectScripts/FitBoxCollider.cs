using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitBoxCollider : MonoBehaviour {

	private BoxCollider boxCollider;

	// Use this for initialization
	public void GetFit (float scaleFactor) {

		boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
			boxCollider = gameObject.AddComponent<BoxCollider>();

		//create new bound to encapsulate children
		Bounds outBound = new Bounds(Vector3.zero, Vector3.zero);
		Bounds tmp = new Bounds(Vector3.zero, Vector3.zero);
		Vector3 childLocal;

		//check if the object itself has MeshFilter
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if (meshFilter != null) {
			tmp = meshFilter.mesh.bounds;
			tmp.size *= scaleFactor;
			outBound.Encapsulate(tmp);
		}

		foreach(Transform child in transform) {
			//get the local position of child to make it center of child bounds
			childLocal = child.transform.localPosition;

			//navigate each child and see if they have MeshFilter
			MeshFilter childMesh = child.gameObject.GetComponent<MeshFilter>();
			if (childMesh != null) {
				//get the bounds of the mesh
				tmp = childMesh.mesh.bounds;
				tmp.size *= scaleFactor;
				tmp.center = childLocal;
				outBound.Encapsulate(tmp);
			}

			//and check if they have child mesh (2nd-level deep)
			foreach (MeshFilter childChildMesh in child.gameObject.GetComponentsInChildren<MeshFilter>()) {
				//Debug.Log(childChildMesh.mesh.bounds);
				tmp = childChildMesh.mesh.bounds;
				tmp.size *= scaleFactor;
				tmp.center = childLocal;
				outBound.Encapsulate(tmp);
			}
		}

		//set the size of box collider to the size of the bounds
		//Debug.Log(outBound);
		boxCollider.center = outBound.center;
		boxCollider.size = outBound.size;
	}
}
