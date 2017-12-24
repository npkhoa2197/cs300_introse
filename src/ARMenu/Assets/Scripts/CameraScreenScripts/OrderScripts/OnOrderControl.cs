using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using AssemblyCSharp;
using System;

public class OnOrderControl : MonoBehaviour {

	//variables of order layout
	private List<GameObject> optionlist;
    private List<GameObject> commentlist;
    //public GameObject optionprefab;
    public GameObject commentprefab;
    private DishContent content;
    private Transform detail;
    public List<Tuple<string,string> > comments = new List<Tuple<string, string>> { new Tuple<string, string>("User1", "Delicious!"), new Tuple<string, string>("User2", "Brilliant!"), new Tuple<string, string>("User3", "Creative!!") };
    
    //variables to process order
    private GameObject canvas;
    private InputField quantityInput; 
	private InputField requirementsInput; 
	private DatabaseReference rootRef;
	private FoodTargetManager foodManager;

    // Use this for initialization
    void Start () {
		foodManager = transform.parent.GetComponent<FoodTargetManager> ();

		canvas = this.gameObject;
		detail = canvas.transform.Find("ScrollView_5/ScrollRect/Content");
		//quantityInput = GameObject.Find ("Amount").GetComponent<InputField> (); 
		quantityInput = detail.Find("Amount").GetComponent<InputField>();
		quantityInput.onValueChange.AddListener(delegate {onQuantityChanged();});
		//requirementsInput = GameObject.Find ("RequirementsInput").GetComponent<InputField> ();
		requirementsInput = detail.Find("AdditionalInfo").GetComponent<InputField>();

		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");

		// Get the root reference location of the database.
		rootRef = FirebaseDatabase.DefaultInstance.RootReference;

		// set default to inactive
		canvas.SetActive (false);
	
		//set the layout content
		DishContent _content = new DishContent(
			foodManager.GetFoodName() + " (" + foodManager.GetSelectedVarName() + ")",
			null,
			0.7f,
			foodManager.GetFoodDescription(),
			null,
			(float)foodManager.GetFoodPrice(),
			1,
			"",
			comments
			);

        canvas.GetComponent<OnOrderControl>().setContent(_content);
        
	}
	
	void OnEnable() {
		if (content != null) {
			content.dishname = foodManager.GetFoodName() + " (" + foodManager.GetSelectedVarName() + ")";
        	canvas.transform.Find("Title/Text").GetComponent<Text>().text = content.dishname;
		}
	}

	void OnDisable() {
		if (quantityInput != null) {
			quantityInput.text = "";
		}
		if (requirementsInput != null) {
			requirementsInput.text = "";
		}
	}

	// public void onCloseButtonClicked () {
	// 	canvas.SetActive (false);
	// 	quantityInput.text = "";
	// 	requirementsInput.text = "";
	// }

	public void onOrderButtonClicked () {
		//get inputs from the input fields
		string quantity = quantityInput.text;
		Debug.Log(quantity);
		string requirements = requirementsInput.text;

		//create an Order object based on the information given by the users and the FoodManager
		Order order = new Order (
			"",
			requirements, 
			false, 
			foodManager.GetFoodName() + "(" + foodManager.GetSelectedVarName() + ")", 
			false, 
			foodManager.GetFoodPrice(), 
			long.Parse(quantity), 
			0);
		string jsonOrder = JsonUtility.ToJson(order);

		//write the new order as a new child node under Order entry
		DatabaseReference _ref = rootRef.Child("Order").Push();
		_ref.SetRawJsonValueAsync(jsonOrder);
		
		//after finishing the ordering, the order box will disappear
		quantityInput.text = "1";
		requirementsInput.text = "";
		//canvas.SetActive (false);
	}

	public void onQuantityChanged() {
		double totalPrice = double.Parse(quantityInput.text)*foodManager.GetFoodPrice();
		detail.Find("Total").GetComponent<Text>().text = totalPrice.ToString() + "$";
	}

    public void setContent(DishContent _content)
    {
        content = _content;
        
        //Set content
       	detail.localPosition = new Vector3(detail.localPosition.x, 0, detail.localPosition.z);
        canvas.transform.Find("Title/Text").GetComponent<Text>().text = content.dishname;
        detail.Find("Price").GetComponent<Text>().text = content.price.ToString() + "$";
        detail.Find("Amount").Find("Placeholder").GetComponent<Text>().text = content.amount.ToString();
        detail.Find("Total").GetComponent<Text>().text = (content.price * content.amount).ToString() + "$";
        detail.Find("AdditionalInfo").GetComponent<InputField>().text = content.additionalinfo;
    }
}
