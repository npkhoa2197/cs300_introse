using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitBoxCollider : MonoBehaviour {

	private BoxCollider boxCollider;

	// Use this for initialization
	public void GetFit () {
		boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
			boxCollider = gameObject.AddComponent<BoxCollider>();

		//create new bound to encapsulate children
		Bounds outBound = new Bounds(Vector3.zero, Vector3.zero);
		foreach(Transform child in transform) {
			//navigate each child and see if they have MeshFilter
			MeshFilter childMesh = child.gameObject.GetComponent<MeshFilter>();
			if (childMesh != null) {
				//get the bounds of the mesh
				outBound.Encapsulate(childMesh.mesh.bounds);
			}

			//and check if they have child mesh (2nd-level deep)
			foreach (MeshFilter childChildMesh in child.gameObject.GetComponentsInChildren<MeshFilter>()) {
				outBound.Encapsulate(childChildMesh.mesh.bounds);
			}
		}
		
		//set the size of box collider to the size of the bounds
		boxCollider.center = outBound.center;
		boxCollider.size = outBound.size;
	}
}
