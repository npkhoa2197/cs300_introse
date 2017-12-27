using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using AssemblyCSharp;
using System;

public class MenuDetailControl : MonoBehaviour {

	private List<GameObject> optionlist;
    private List<GameObject> commentlist;
    public GameObject optionprefab;
    public GameObject commentprefab;
    private DishContent content;
    private GameObject menuinfo;
    private Transform menuinfoTransform;

    private GameObject reviewCanvas;

    //variables to process order
    private InputField quantityInput; 
    private InputField requirementsInput; 
    private DatabaseReference rootRef;
    private float totalPrice;
    private GlobalContentProvider global;

    // Use this for initialization
    void Start () {
        reviewCanvas = GameObject.Find("ReviewCanvas");
		// Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");

        // Get the root reference location of the database.
        rootRef = FirebaseDatabase.DefaultInstance.RootReference;

        //referencing variables for order process
        menuinfo = GameObject.Find("MenuDetail");
        menuinfoTransform = menuinfo.transform.Find("ScrollView_5/ScrollRect/Content");
        quantityInput = menuinfoTransform.Find("Amount").GetComponent<InputField>();
        requirementsInput = menuinfoTransform.Find("AdditionalInfo").GetComponent<InputField>();

        //add listener for quantity input field on end edit
        quantityInput.onEndEdit.AddListener(delegate {onQuantityChanged(content);});

        //init GlobalContentProvider object
        global = GlobalContentProvider.Instance;
	}

	//listener on end editing of quantity input field
    public void onQuantityChanged(DishContent content) {
    	if (quantityInput.text == null || quantityInput.text == "") {
    		menuinfoTransform.Find("Total").GetComponent<Text>().text = "$" + content.price.ToString();
    	}
    	else {
        	menuinfoTransform.Find("Total").GetComponent<Text>().text = "$" + (float.Parse(quantityInput.text)*content.price).ToString();
    	}
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void onButtonOrderClicked() {
        //get inputs from the input fields
        string quantity = "1";
        if (quantityInput.text != null && quantityInput.text != "")
            quantity = quantityInput.text;
        string requirements = requirementsInput.text;

        //get dish name of the selected option
        GameObject _menuDetail = GameObject.Find("MenuDetail");
        GameObject _menuDetailContent = _menuDetail.transform.Find("ScrollView_5/ScrollRect/Content").gameObject;
        GameObject _optionList = _menuDetailContent.transform.Find("OptionList/ScrollRect/Content").gameObject;
        
        string variant = "";
        for (int i = 0; i < _optionList.transform.childCount; ++i) {
            if (_optionList.transform.GetChild(i).gameObject.GetComponent<Toggle>().isOn == true) {
                variant = " (" + _optionList.transform.GetChild(i).gameObject.GetComponentInChildren<Text>().text + ")";
                break;
            }
        }

        //create an Order object for database storage 
        Order order = new Order (
            "",
            requirements, 
            false, 
            content.dishname + variant, 
            false, 
            long.Parse(quantity)*content.price, 
            long.Parse(quantity), 
            global.tableNumber);

        string jsonOrder = JsonUtility.ToJson(order);

        //write the new order as a new child node under Order entry
        DatabaseReference _ref = rootRef.Child("Order").Push();
        _ref.SetRawJsonValueAsync(jsonOrder);

        //add new order into global conent provider
        GlobalContentProvider.Instance.AddOrderEntry(order, (double) content.price);
        
        //after finishing the ordering, navigating back to the menulist
        quantityInput.text = "1";
        requirementsInput.text = "";
        GameObject temp = GameObject.Find("Menulist");
        temp.GetComponent<MenuListControl>().PostInsideOrderButtonClicked();
    }

    //set options
    public void setOptions(GameObject optionprefab, GameObject DishContent)
    {
    	GameObject optionsContent = DishContent.transform.Find("OptionList/ScrollRect/Content").gameObject;
        for (int i = 0; i < optionsContent.transform.childCount; i++)
        {
        	Destroy(optionsContent.transform.GetChild(i).gameObject);
		}
        optionlist = new List<GameObject>();
        optionsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);

        //referencing the toggleGroup in order to group all the toggles (options) into one Toggle Group
        Transform _optionList = menuinfoTransform.gameObject.transform.Find("OptionList/ScrollRect/");
        ToggleGroup toggleGroup = _optionList.Find("Content").GetComponent<ToggleGroup>();

        for (int i = 0; content.options != null && i < content.options.Count; i++)
        {
        	GameObject option = GameObject.Instantiate(optionprefab);
            option.transform.SetParent(optionsContent.transform);
            option.transform.localScale = new Vector3(1, 1, 1);
            option.transform.localPosition = new Vector3(i*360 + 60, -50, 0);
            option.transform.Find("Text").GetComponent<Text>().text = content.options[i];
            option.GetComponent<Toggle>().isOn = false;
            
            //set the toggleGroup
            option.GetComponent<Toggle>().group = toggleGroup; 
            
            optionsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)optionsContent.transform).rect.width + 360);
            optionlist.Add(option);
        }
	}

	//set comments
	public void setComments()
	{
        GameObject commentsContent = menuinfoTransform.gameObject.transform.Find("CommentList/ScrollRect/Content").gameObject;

		for (int i = 0; i < commentsContent.transform.childCount; i++)
		{
        	Destroy(commentsContent.transform.GetChild(i).gameObject);
		}
		commentlist = new List<GameObject>();
        commentsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        for (int i = 0; content.comments != null && i < content.comments.Count; i++)
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

	//set content of the detail screen
    public void setContent(DishContent _content)
    {
        content = _content;
        //Set content
        GameObject menuinfo = GameObject.Find("MenuDetail");
        Transform Content = menuinfo.transform.Find("ScrollView_5/ScrollRect/Content");
        Content.localPosition = new Vector3(Content.localPosition.x, 0, Content.localPosition.z);
        menuinfo.transform.Find("Title/Text").GetComponent<Text>().text = content.dishname;
        if (content.image == null) Content.Find("Image").GetComponent<Image>().color = Color.black;
        else Content.Find("Image").GetComponent<Image>().sprite = content.image;
        Content.Find("Rating").GetComponent<Rating>().setValue(content.score);
        Content.Find("Description").GetComponent<Text>().text = content.description;
        //Options content
        setOptions(optionprefab, Content.gameObject);
        Content.Find("Price").GetComponent<Text>().text = "$" + content.price.ToString();
        Content.Find("Amount").Find("Placeholder").GetComponent<Text>().text = content.amount.ToString();
        Content.Find("Total").GetComponent<Text>().text = "$" + (content.price * content.amount).ToString();
        Content.Find("AdditionalInfo").GetComponent<InputField>().text = content.additionalinfo;
        
        //call database to get comments
        InvokeDatabase();
        //setComments(commentprefab, Content.gameObject);
    }

    void InvokeDatabase() {
        FirebaseDatabase.DefaultInstance
        .GetReference("Meal/" + GlobalContentProvider.GetMealKey(content.dishname) + "/Comments")
        .GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                //handle error
            }
            else if (task.IsCompleted) {
                DataSnapshot commentsSnap = task.Result;
                foreach (DataSnapshot comment in commentsSnap.Children) {
                    if ((string) comment.Child("username").Value != "" && (string) comment.Child("content").Value != "")
                    content.comments.Add(new Tuple<string, string>(
                        (string) comment.Child("username").Value, 
                        (string) comment.Child("content").Value));
                }

                setComments();
            }
        });
    }

    public void onShareButtonClick() {
        reviewCanvas.SetActive(true);
        reviewCanvas.GetComponent<ReviewControlMenuList>().init(content);
    }
}
