using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderProcess : MonoBehaviour {

	private GameObject canvas; 
	private InputField quantityInput; 
	private InputField requirementsInput; 

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("OrderCanvas");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onCloseButtonClicked () {
		canvas.SetActive (false);
	}

	public void onOrderButtonClicked () {
		quantityInput = GameObject.Find ("QuantityInput").GetComponent<InputField> (); 
		requirementsInput = GameObject.Find ("RequirementsInput").GetComponent<InputField> ();


		string quantity = quantityInput.text;
		string requirements = requirementsInput.text;

		Debug.Log (quantity + requirements);
	}
}
