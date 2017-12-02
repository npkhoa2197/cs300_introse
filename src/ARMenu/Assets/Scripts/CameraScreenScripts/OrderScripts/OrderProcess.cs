using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using AssemblyCSharp;

public class OrderProcess : MonoBehaviour {

	private GameObject canvas; 
	private InputField quantityInput; 
	private InputField requirementsInput; 
	private DatabaseReference reference;
	private FoodInfo foodInfo;
	private VariantAdapter foodVariant;

	// Use this for initialization
	void Start () {
		// Referencing the order canvas and the current model
		canvas = GameObject.Find ("OrderCanvas");
		foodInfo = transform.parent.GetComponent<FoodInfo> ();
		foodVariant = transform.parent.GetComponent<VariantAdapter> ();

		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");

		// Get the root reference location of the database.
		reference = FirebaseDatabase.DefaultInstance.RootReference;
	}

	// Update is called once per frame
	void Update () {

	}

	public void onCloseButtonClicked () {
		canvas.SetActive (false);
	}

	public void onOrderButtonClicked () {
		//get inputs from the input fields
		quantityInput = GameObject.Find ("QuantityInput").GetComponent<InputField> (); 
		requirementsInput = GameObject.Find ("RequirementsInput").GetComponent<InputField> ();

		string quantity = quantityInput.text;
		string requirements = requirementsInput.text;

		//create an Order object based on the information given by the users and the foodInfo
		Order order = new Order (requirements, false, foodInfo.getFoodName(), false, 
			foodInfo.getPrice(), long.Parse(quantity), 0);
		
		//write the new order as a new child node under Order entry
		DatabaseReference _ref  = reference.Child ("Order").Push();
		_ref.Child ("Additional Requirements").SetValueAsync (requirements);
		_ref.Child ("Finished").SetValueAsync (false);
		_ref.Child ("Meal").SetValueAsync (foodInfo.getFoodName ());
		_ref.Child ("Paid").SetValueAsync (false);
		_ref.Child ("Price").SetValueAsync (long.Parse (quantity));
		_ref.Child ("Quantity").SetValueAsync (quantity);
		_ref.Child ("Table").SetValueAsync (0);
		
		//after finishing the ordering, the order box will disappear
		canvas.SetActive (false);
	}
}
