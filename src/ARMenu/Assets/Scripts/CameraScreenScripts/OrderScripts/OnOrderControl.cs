using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using AssemblyCSharp;
using System;

public class OnOrderControl : MonoBehaviour {

    //public GameObject optionprefab;
    private DishContent content;
    private Transform detail;

    //variables to process order
    private GameObject canvas;
    private InputField quantityInput; 
	private InputField requirementsInput; 
	private DatabaseReference rootRef;
	private FoodTargetManager foodManager;
	private Toast toast;
	private Button backBtn;
	private GlobalContentProvider provider;
	private ConfirmDialog confirmDialog;

    // Use this for initialization
    void Start () {
		provider = GlobalContentProvider.Instance;
		foodManager = provider.GetCurrentFoodManager();

		canvas = this.gameObject;
		detail = canvas.transform.Find("ScrollView_5/ScrollRect/Content");
		//quantityInput = GameObject.Find ("Amount").GetComponent<InputField> (); 
		quantityInput = detail.Find("Amount").GetComponent<InputField>();
		quantityInput.onEndEdit.AddListener(delegate {onQuantityChanged();});
		//requirementsInput = GameObject.Find ("RequirementsInput").GetComponent<InputField> ();
		requirementsInput = detail.Find("AdditionalInfo").GetComponent<InputField>();
		//get toast
		toast = detail.Find("Toast").GetComponent<Toast>();
		//get back btn
		backBtn = canvas.transform.Find("Title/BackBtn").GetComponent<Button>();
		backBtn.onClick.AddListener(OnBackClick);
		//get confirm dialog
		confirmDialog = transform.Find("ConfirmDialog").GetComponent<ConfirmDialog>();

		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");

		// Get the root reference location of the database.
		rootRef = FirebaseDatabase.DefaultInstance.RootReference;
	
		//set the layout content
		DishContent _content = new DishContent(
			foodManager.GetFoodName() + " (" + foodManager.GetSelectedVarName() + ")",
			null,
			0f,
			null,
			null,
			(float)foodManager.GetFoodPrice(),
			1,
			"",
			null
			);

        setContent(_content);
	}
	
	void OnBackClick() {
		SceneManager.UnloadSceneAsync("OrderScene");
	}

	// void OnEnable() {
	// 	if (content != null) {
	// 		content.dishname = foodManager.GetFoodName() + " (" + foodManager.GetSelectedVarName() + ")";
    //     	canvas.transform.Find("Title/Text").GetComponent<Text>().text = content.dishname;
	// 	}
	// }

	// void OnDisable() {
	// 	ResetInput();
	// }

	public void onOrderButtonClicked () {
		confirmDialog.Confirm(MakeOrder);
	}

	void MakeOrder() {
		//get inputs from the input fields
		string quantity = quantityInput.text;
		string requirements = requirementsInput.text;

		//create an Order object based on the information given by the users and the FoodManager
		Order order = new Order (
			"",
			requirements, 
			false, 
			foodManager.GetFoodName() + " (" + foodManager.GetSelectedVarName() + ")", 
			false, 
			foodManager.GetFoodPrice() * long.Parse(quantity), 
			long.Parse(quantity),
			provider.tableNumber);
		string jsonOrder = JsonUtility.ToJson(order);

		//write the new order as a new child node under Order entry
		DatabaseReference _ref = rootRef.Child("Order").Push();
		_ref.SetRawJsonValueAsync(jsonOrder);
		
		//add order to order history
		provider.AddOrderEntry(order, foodManager.GetFoodPrice());

		//show toast
		toast.ShowText("Your order has been placed!");

		//after finishing the ordering, the order box will disappear
		ResetInput();
	}

	public void onQuantityChanged() {
		if (quantityInput.text == null || quantityInput.text == "") {
			quantityInput.text = "1";
			detail.Find("Total").GetComponent<Text>().text = foodManager.GetFoodPrice().ToString() + "$";
		}
		else {
			double totalPrice = double.Parse(quantityInput.text)*foodManager.GetFoodPrice();
			detail.Find("Total").GetComponent<Text>().text = totalPrice.ToString() + "$";
		}
	}

    public void setContent(DishContent _content)
    {
        content = _content;
        
        //Set content
       	detail.localPosition = new Vector3(detail.localPosition.x, 0, detail.localPosition.z);
        canvas.transform.Find("Title/Text").GetComponent<Text>().text = content.dishname;
        detail.Find("Price").GetComponent<Text>().text = content.price.ToString() + "$";
        quantityInput.text = content.amount.ToString();
        detail.Find("Total").GetComponent<Text>().text = (content.price * content.amount).ToString() + "$";
        detail.Find("AdditionalInfo").GetComponent<InputField>().text = content.additionalinfo;
    }

	void ResetInput() {
		if (quantityInput != null) {
			quantityInput.text = "1";
			detail.Find("Total").GetComponent<Text>().text = foodManager.GetFoodPrice().ToString() + "$";
		}

		if (requirementsInput != null) {
			requirementsInput.text = "";
		}
	}
}
