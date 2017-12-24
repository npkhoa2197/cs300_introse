using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using AssemblyCSharp;
using System;

public class DetailsControl : MonoBehaviour {

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

		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");

		// Get the root reference location of the database.
		rootRef = FirebaseDatabase.DefaultInstance.RootReference;

		// set default to inactive
		canvas.SetActive (false);
	
		//set the layout content
		DishContent _content = new DishContent(
			foodManager.GetFoodName(),
			null,
			0.7f,
			foodManager.GetFoodDescription(),
			null,
			(float)foodManager.GetFoodPrice(),
			1,
			"",
			comments
			);

        setContent(_content); 
	}

	//initialize comments in the detail screen
	public void setComments()
	{
		GameObject commentsContent = detail.transform.Find("CommentList/ScrollRect/Content").gameObject;
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
		
        detail.Find("Price").GetComponent<Text>().text = content.price.ToString() + "$";
        
        //Comments content
        setComments();
    }
}