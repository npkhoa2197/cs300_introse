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

		canvas = GameObject.Find ("OrderCanvas");
		detail = canvas.transform.Find("ScrollView_5/ScrollRect/Content");
		//quantityInput = GameObject.Find ("Amount").GetComponent<InputField> (); 
		quantityInput = detail.Find("Amount").GetComponent<InputField>();
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
	
	// Update is called once per frame
	void Update () {
		
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
		quantityInput.text = "";
		requirementsInput.text = "";
		canvas.SetActive (false);
	}

 //    public void setOptions(GameObject optionprefab, GameObject DishContent)
 //    {
 //    	GameObject optionsContent = DishContent.transform.Find("OptionList/ScrollRect/Content").gameObject;
 //        for (int i = 0; i < optionsContent.transform.childCount; i++)
 //        {
 //        	Destroy(optionsContent.transform.GetChild(i).gameObject);
	// 	}
 //        optionlist = new List<GameObject>();
 //        optionsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
 //        for (int i = 0; i < content.options.Count; i++)
 //        {
 //        	GameObject option = GameObject.Instantiate(optionprefab);
 //            option.transform.SetParent(optionsContent.transform);
 //            option.transform.localScale = new Vector3(1, 1, 1);
 //            option.transform.localPosition = new Vector3(i*360 + 60, -50, 0);
 //            option.transform.Find("Text").GetComponent<Text>().text = content.options[i];
 //            option.GetComponent<Toggle>().isOn = false;
 //            optionsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)optionsContent.transform).rect.width + 360);
 //            optionlist.Add(option);
 //        }
	// }

	//initialize comments in the detail screen
	public void setComments(GameObject commentprefab, GameObject DishContent)
	{
		GameObject commentsContent = DishContent.transform.Find("CommentList/ScrollRect/Content").gameObject;
		for (int i = 0; i < commentsContent.transform.childCount; i++)
		{
        	Destroy(commentsContent.transform.GetChild(i).gameObject);
		}
		commentlist = new List<GameObject>();
        commentsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        for (int i = 0; i < content.comments.Count; i++)
        {
        	GameObject comment = GameObject.Instantiate(commentprefab);
            comment.transform.SetParent(commentsContent.transform);
            comment.transform.localScale = new Vector3(1, 1, 1);
            comment.transform.localPosition = new Vector3(658.7f * i + 311, -174.4f, 0);
            comment.transform.Find("Writer").GetComponent<Text>().text = content.comments[i].Item1;
            comment.transform.Find("Text").GetComponent<Text>().text = content.comments[i].Item2;
            commentsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)commentsContent.transform).rect.width + 658.7f);
            commentlist.Add(comment);
		}
	}

    public void setContent(DishContent _content)
    {
        content = _content;
        
        //Set content
       	detail.localPosition = new Vector3(detail.localPosition.x, 0, detail.localPosition.z);
        canvas.transform.Find("Title/Text").GetComponent<Text>().text = content.dishname;
        
        if (content.image == null) detail.Find("Image").GetComponent<Image>().color = Color.black;
        else detail.Find("Image").GetComponent<Image>().sprite = content.image;
        
        detail.Find("Rating").GetComponent<Rating>().setValue(content.score);
        detail.Find("Description").GetComponent<Text>().text = content.description;
        
        //Options content
        //setOptions(optionprefab, Content.gameObject);
        
        detail.Find("Price").GetComponent<Text>().text = content.price.ToString() + "$";
        detail.Find("Amount").Find("Placeholder").GetComponent<Text>().text = content.amount.ToString();
        detail.Find("Total").GetComponent<Text>().text = (content.price * content.amount).ToString() + "$";
        detail.Find("AdditionalInfo").GetComponent<InputField>().text = content.additionalinfo;
        
        //Comments content
        setComments(commentprefab, detail.gameObject);
    }
}
