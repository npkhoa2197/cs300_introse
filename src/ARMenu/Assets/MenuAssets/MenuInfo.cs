using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInfo : MonoBehaviour {

    public GameObject optionprefab;
    public GameObject commentprefab;
    private GameObject menuinfo;
    //private GameObject commentinfo;
    private float posx, nextx;
    private bool viewinfo;
    private bool viewlist;
    private float v;
    public float vx;
    
    public class MenuItemContent
    {
        public string dishname = "Dish Name";
        public Sprite image = null;
        public float score = 0.7f;
        public string description = "Lorem ipsum dolor sit amet, sapien etiam, nunc amet dolor ac odio mauris justo. Luctus arcu, urna praesent at id quisque ac. Arcu es massa vestibulum malesuada, integer vivamus elit eu mauris eus, cum eros quis aliquam wisi.";
        public List<string> options = new List<string> { "Option 1", "Option 2", "Option 3", "Option 4", "Option 5" };
        public float price = 100;
        public int amount = 1;
        public string additionalinfo = "";
        public List<Tuple<string,string> > comments = new List<Tuple<string, string>> { new Tuple<string, string>("User1", "Delicious!"), new Tuple<string, string>("User2", "Brilliant!"), new Tuple<string, string>("User3", "Creative!!") };
        //_________________________________________________________________________________________________
        
        private List<GameObject> optionlist;
        private List<GameObject> commentlist;
        public void setOptions(GameObject optionprefab, GameObject DishContent)
        {
            GameObject optionsContent = DishContent.transform.Find("OptionList/ScrollRect/Content").gameObject;
            for (int i = 0; i < optionsContent.transform.childCount; i++)
            {
                Destroy(optionsContent.transform.GetChild(i).gameObject);
            }
            optionlist = new List<GameObject>();
            optionsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            for (int i = 0; i < options.Count; i++)
            {
                GameObject option = GameObject.Instantiate(optionprefab);
                option.transform.SetParent(optionsContent.transform);
                option.transform.localScale = new Vector3(1, 1, 1);
                option.transform.localPosition = new Vector3(i*360 + 60, -50, 0);
                option.transform.Find("Text").GetComponent<Text>().text = options[i];
                option.GetComponent<Toggle>().isOn = false;
                optionsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)optionsContent.transform).rect.width + 360);
                optionlist.Add(option);
            }
        }
        public void setComments(GameObject commentprefab, GameObject DishContent)
        {
            GameObject commentsContent = DishContent.transform.Find("CommentList/ScrollRect/Content").gameObject;
            for (int i = 0; i < commentsContent.transform.childCount; i++)
            {
                Destroy(commentsContent.transform.GetChild(i).gameObject);
            }
            commentlist = new List<GameObject>();
            commentsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
            for (int i = 0; i < comments.Count; i++)
            {
                GameObject comment = GameObject.Instantiate(commentprefab);
                comment.transform.SetParent(commentsContent.transform);
                comment.transform.localScale = new Vector3(1, 1, 1);
                comment.transform.localPosition = new Vector3(658.7f * i + 311, -174.4f, 0);
                comment.transform.Find("Writer").GetComponent<Text>().text = comments[i].Item1;
                comment.transform.Find("Text").GetComponent<Text>().text = comments[i].Item2;
                commentsContent.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ((RectTransform)commentsContent.transform).rect.width + 658.7f);
                commentlist.Add(comment);
            }
        }
    };

    //DEBUG
    void wl(string s)
    {
        Debug.Log(s, gameObject);
    }
	// Use this for initialization
	void Start () {
        viewinfo = false;
        menuinfo = null;
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

    private bool updateMenuItemDetail(MenuItemContent content)
    {
		wl("updateMenuItemDetail");
        menuinfo = GameObject.Find("MenuDetail");
        Transform Content = menuinfo.transform.Find("ScrollView_5/ScrollRect/Content");
        Content.localPosition = new Vector3(Content.localPosition.x, 0, Content.localPosition.z);
        menuinfo.transform.Find("Title/Text").GetComponent<Text>().text = content.dishname;
        if (content.image == null) Content.Find("Image").GetComponent<Image>().color = Color.black;
        else Content.Find("Image").GetComponent<Image>().sprite = content.image;
        Content.Find("Rating").GetComponent<Rating>().scorevalue = content.score;
        Content.Find("Description").GetComponent<Text>().text = content.description;
        //Options content
        content.setOptions(optionprefab, Content.gameObject);
        Content.Find("Price").GetComponent<Text>().text = content.price.ToString() + "$";
        Content.Find("Amount").GetComponent<Text>().text = content.amount.ToString();
        Content.Find("Total").GetComponent<Text>().text = (content.price * content.amount).ToString() + "$";
        //wl(content.additionalinfo);
        Content.Find("AdditionalInfo").GetComponent<InputField>().text = content.additionalinfo;
        //Comments content
        content.setComments(commentprefab, Content.gameObject);
        return (true);
	}

    private MenuItemContent GetMenuItemContent()
    {
        return new MenuItemContent();
    }

    public void ViewMenuItem()
    {
        MenuItemContent content = GetMenuItemContent();
        updateMenuItemDetail(content);
        posx = menuinfo.transform.position.x;
        nextx = Screen.width / 2;
        v = -vx;
        viewinfo = true;
    }

    public void ViewMenuList()
    {
        menuinfo = GameObject.Find("MenuDetail");
        posx = menuinfo.transform.position.x;
        nextx = Screen.width / 2 * 3;
        v = vx;
        viewlist = true;
    }
}
