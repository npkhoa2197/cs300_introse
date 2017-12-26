using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;

public class MenuListControl : MonoBehaviour {

	private List<GameObject> MenuItems;
    public GameObject menuPrefab;
    private GameObject Content;
    private GameObject dishDetail;
    private float offset;
    private float menuHeight;
    private GameObject menuinfo;
    private GameObject reviewCanvas;

    //private GameObject commentinfo;
    private float posx, nextx;
    private bool viewinfo;
    private bool viewlist;
    private float v;
    public float vx;

    private InputField quantityInput; 
    private InputField requirementsInput;

    //GlobalContentProvider provides a list of supported dishes
    GlobalContentProvider provider;

    //Debug
    void wl(string s)
    {
        Debug.Log(s, gameObject);
    }

	// Use this for initialization
	void Start () {
        viewinfo = false;
        menuinfo = null;
        MenuItems = new List<GameObject>();
        
        Content = GameObject.Find("/Menulist/Background/ScrollView_1/ScrollRect/Content");
        dishDetail = GameObject.Find("/Menulist/MenuDetail/ScrollView_5/ScrollRect/Content");
        reviewCanvas = GameObject.Find("ReviewCanvas");
        
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            GameObject Menuitem = Content.transform.GetChild(i).gameObject;
            MenuItems.Add(Menuitem);
        }

        offset = ((RectTransform)menuPrefab.transform).rect.height * 0.03f;
        menuHeight = ((RectTransform)menuPrefab.transform).rect.height * 0.65f + offset;
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, menuHeight * MenuItems.Count + 10);
        //wl(MenuItems[0].transform.localScale.x.ToString());

        // Set up the Editor before calling into the realtime database.
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");

        //add dishes from GlobalContentProvider to the MenuItem list
        provider = GlobalContentProvider.Instance;
        for (int i = 0; i < provider.foods.Length; ++i) {
            
            //init options (variants) for each dish
            List<string> options = new List<string>();
            for (int j = 0; j < provider.foods[i].variants.Length; ++j) {
                options.Add(provider.foods[i].variants[j].variantName);
            }

            //init comments for each dish, init defaul value for demo
            List<Tuple<string,string>> comments = new List<Tuple<string, string>>();
            
            //invoke database to get the rating score and add menuitem once the async task is finished
            // DishContent temp = new DishContent(
            //     provider.foods[i].foodName,
            //     provider.foods[i].foodImage,
            //     0,
            //     provider.foods[i].description,
            //     options,
            //     (float)provider.foods[i].price,
            //     1,
            //     "",
            //     comments);
            // InvokeDatabase(temp);
            addMenuItem(new DishContent(
                provider.foods[i].foodName,
                provider.foods[i].foodImage,
                0,
                provider.foods[i].description,
                options,
                (float)provider.foods[i].price,
                1,
                "",
                comments));
        }
    }


    void InvokeDatabase(Rating _rating, string key, DishContent content) {
    	double temp = 0;

        FirebaseDatabase.DefaultInstance
        .GetReference("Meal/" + key + "/AveRating/Rate")
        .GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
                //handle error
            }
            else if (task.IsCompleted) {
                DataSnapshot rating = task.Result;             
                temp = (double) rating.Value;
                _rating.scorevalue = (float) temp;
                _rating.setValue(_rating.scorevalue);
                content.score = _rating.scorevalue;
            }
        });
    }

    private void addMenuItem(DishContent _content)
    {
        //Intantiate object
        GameObject menuitem = (GameObject)GameObject.Instantiate(menuPrefab);

        //set position
        menuitem.transform.SetParent(Content.transform);
        menuitem.transform.localScale = new Vector3(0.65f, 0.65f, 1);
        if (MenuItems.Count != 0)
        {
            wl("set local position");
            menuitem.transform.localPosition = new Vector3(MenuItems[MenuItems.Count - 1].transform.localPosition.x, MenuItems[MenuItems.Count - 1].transform.localPosition.y - menuHeight, 0);
        }
        else
        {
            menuitem.transform.localPosition = new Vector3(401.015f, -menuHeight / 2, 0);
        }

        //set content
        if (_content.image != null) {
            menuitem.transform.Find("Image").GetComponent<Image>().color = Color.white;
            menuitem.transform.Find("Image").GetComponent<Image>().sprite = _content.image;
        }
        menuitem.transform.Find("Dishname").GetComponent<Text>().text = _content.dishname;
        menuitem.transform.Find("Info").GetComponent<Text>().text = _content.description + "\nPrice: $" + _content.price;
        menuitem.transform.Find("Rating").gameObject.transform.Find("Score").GetComponent<Slider>().maxValue = 5;
        //menuitem.transform.Find("Rating").GetComponent<Rating>().scorevalue = _content.value;
        InvokeDatabase(menuitem.transform.Find("Rating").GetComponent<Rating>(),
            GlobalContentProvider.GetMealKey(_content.dishname), _content);
        menuitem.transform.Find("Order").GetComponent<Button>().onClick.AddListener(() => ViewMenuItem(_content));
        menuitem.transform.Find("Share").GetComponent<Button>().onClick.AddListener(() => onClickShare(_content, reviewCanvas));
        menuitem.transform.Find("ItemClick").GetComponent<Button>().onClick.AddListener(() 
            => ViewMenuItem(_content));

        //add to MenuItems list
        MenuItems.Add(menuitem);
    }


    // Update is called once per frame
    void Update () {
        if (viewinfo || viewlist)
        {
            posx = menuinfo.transform.position.x;
            if ((viewinfo && (posx < nextx)) || (viewlist && (posx > nextx)))
            {
                menuinfo.transform.Translate(nextx - posx, 0, 0);
                viewinfo = false;
                viewlist = false;
            }
            else
            {
                menuinfo.transform.Translate(v, 0, 0);
            }
        }
    }

	public void onClickShare(DishContent content, GameObject canvas){
		//share
        canvas.SetActive(true);
	    canvas.GetComponent<ReviewControlMenuList>().init(content);
	}

    public void ViewMenuItem(DishContent content)
    {
        //set content for MenuDetail
        menuinfo = GameObject.Find("MenuDetail");
        menuinfo.GetComponent<MenuDetailControl>().setContent(content);

        //Move MenuDetail
        posx = menuinfo.transform.position.x;
        nextx = Screen.width / 2;
        v = -vx;
        viewinfo = true;

        //referencing quantity and requirements input fields to set blank when users viewMenuList()
        Transform menuinfoTransform = menuinfo.transform.Find("ScrollView_5/ScrollRect/Content");
        quantityInput = menuinfoTransform.Find("Amount").GetComponent<InputField>();
        requirementsInput = menuinfoTransform.Find("AdditionalInfo").GetComponent<InputField>();
    }

    public void ViewMenuList()
    {
        menuinfo = GameObject.Find("MenuDetail");
        posx = menuinfo.transform.position.x;
        nextx = Screen.width / 2 * 3;
        v = vx;
        viewlist = true;

        quantityInput.text = "1";
        requirementsInput.text = "";
    }

    public void PostInsideOrderButtonClicked()
    {
        menuinfo = GameObject.Find("MenuDetail");
        posx = menuinfo.transform.position.x;
        nextx = Screen.width / 2 * 3;
        v = vx;
        viewlist = true;
    }

    public void onBackButtonClicked() {
    	SceneManager.LoadScene("HomeScreen");
    }
}