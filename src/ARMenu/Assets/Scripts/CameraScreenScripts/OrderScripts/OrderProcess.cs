﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using AssemblyCSharp;
using System;

public class OrderProcess : MonoBehaviour {

	private GameObject canvas; 
	private InputField quantityInput; 
	private InputField requirementsInput; 
	private DatabaseReference rootRef;
	private FoodObject foodObject;
	private VariantAdapter foodVariant;

	// Use this for initialization
	void Start () {
		// Referencing the order canvas and the current model and the input fields
		canvas = GameObject.Find ("OrderCanvas");
		foodVariant = transform.parent.GetComponent<VariantAdapter> ();
		foodObject = foodVariant.GetFoodObject();
		quantityInput = GameObject.Find ("QuantityInput").GetComponent<InputField> (); 
		requirementsInput = GameObject.Find ("RequirementsInput").GetComponent<InputField> ();

		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");

		// Get the root reference location of the database.
		rootRef = FirebaseDatabase.DefaultInstance.RootReference;

		canvas.SetActive (false);
	}

	public void onCloseButtonClicked () {
		canvas.SetActive (false);
		quantityInput.text = "";
		requirementsInput.text = "";
	}

	public void onOrderButtonClicked () {
		//get inputs from the input fields
		string quantity = quantityInput.text;
		string requirements = requirementsInput.text;

		//create an Order object based on the information given by the users and the FoodObject
		Order order = new Order (
			requirements, 
			false, 
			foodObject.foodName + "(" + foodVariant.GetSelectedVarName() + ")", 
			false, 
			foodObject.price, 
			long.Parse(quantity), 
			0);
		string jsonOrder = JsonUtility.ToJson(order);

		//write the new order as a new child node under Order entry
		DatabaseReference _ref = rootRef.Child("Order").Push();
		_ref.SetRawJsonValueAsync(jsonOrder);
		
		//after finishing the ordering, the order box will disappear
		quantityInput.text = "";
		requirementsInput.text = "";
		canvas.SetActive (false);
	}
}
