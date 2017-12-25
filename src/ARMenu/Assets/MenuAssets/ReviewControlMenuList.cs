using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ReviewControlMenuList : MonoBehaviour {

    private Transform detail;
	private Toast toast;

    //variables to process order
    private GameObject canvas;
	private InputField commentInput;
	private InputField usernameInput;
	private Slider rating;
	private DatabaseReference commentsRef;
	private DatabaseReference ratingRef;
	private DatabaseReference currentRatingRef;
	private string foodKey;
	private DishContent content;
	
	// key of rating of this meal on the db in this session
	private string ratingKey = "";

	public void init(DishContent _content) {
		content = _content;
		foodKey = GlobalContentProvider.GetMealKey(content.dishname);

		commentsRef = FirebaseDatabase.DefaultInstance
			.GetReference("Meal/" + foodKey + "/Comments");
		// Get the reference for rating
		ratingRef = FirebaseDatabase.DefaultInstance
			.GetReference("Meal/" + foodKey + "/Rating");
	}

    // Use this for initialization
    void Start () {
		canvas = GameObject.Find ("ReviewCanvas");
		detail = canvas.transform.Find("ScrollView_5/ScrollRect/Content");

		//get ref to comment and username input
		commentInput = detail.Find("CommentField").GetComponent<InputField>();
		usernameInput = detail.Find("Username").GetComponent<InputField>();

		//add listener for button
		detail.Find("Submit").GetComponent<Button>().onClick.AddListener(OnSubmitClick);
		detail.Find("Share").GetComponent<Button>().onClick.AddListener(OnShareClick);

		//get ref to rating
		rating = detail.Find("Rating").Find("Score").GetComponent<Slider>();

		//get toast
		toast = detail.Find("Toast").GetComponent<Toast>();

		// Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");

		// // Try to get the rating key
		// GlobalContentProvider.Instance.ratings.TryGetValue(foodKey, out ratingKey);
		// // if there is no rating key associated with this meal in this session -> push new one
		// if (ratingKey == "" || ratingKey == null) {
		// 	currentRatingRef = ratingRef.Push();
		// 	ratingKey = currentRatingRef.Key;
		// 	GlobalContentProvider.Instance.ratings.Add(foodKey, ratingKey);
		// } else {
		// 	currentRatingRef = ratingRef.Child(ratingKey);
		// }

		// set default to inactive
		canvas.SetActive (false);

		setContent();
	}

	void ResetInput() {
		rating.value = 0f;
		commentInput.text = "";
		usernameInput.text = "";
	}

	void OnSubmitClick() {
		float score = rating.value;
		string commentName = usernameInput.text;
		string commentContent = commentInput.text;

		DatabaseReference newComment = commentsRef.Push();
		newComment.Child("username").SetValueAsync(commentName);
		newComment.Child("content").SetValueAsync(commentContent);

		//At the mean time just push new key to rate
		ratingRef.Push().SetValueAsync(score);
		//currentRatingRef.SetValueAsync(score);

		//Show toast
		toast.ShowText("Your review has been submitted!");

		ResetInput();
	}

	void OnShareClick() {

	}

    public void setContent()
    {
        //Set content
       	detail.localPosition = new Vector3(detail.localPosition.x, 0, detail.localPosition.z);
        rating.value = 0f;
	}
}
