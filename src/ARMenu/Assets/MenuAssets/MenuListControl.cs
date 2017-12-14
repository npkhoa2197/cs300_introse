using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuListControl : MonoBehaviour {

	private List<GameObject> MenuItems;
    public GameObject menuPrefab;
    private GameObject Content;
    private float offset;
    private float menuHeight;
    private GameObject menuinfo;
    //private GameObject commentinfo;
    private float posx, nextx;
    private bool viewinfo;
    private bool viewlist;
    private float v;
    public float vx;

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
        
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            GameObject Menuitem = Content.transform.GetChild(i).gameObject;
            MenuItems.Add(Menuitem);
        }

        offset = ((RectTransform)menuPrefab.transform).rect.height * 0.03f;
        menuHeight = ((RectTransform)menuPrefab.transform).rect.height * 0.65f + offset;
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, menuHeight * MenuItems.Count + 10);
        //wl(MenuItems[0].transform.localScale.x.ToString());
    }

    private void addMenuItem(Sprite image, string name, string info1, float score)
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
        if (image != null)
        menuitem.transform.Find("Image").GetComponent<Image>().sprite = image;
        menuitem.transform.Find("Dishname").GetComponent<Text>().text = name;
        menuitem.transform.Find("Info").GetComponent<Text>().text = info1;
        menuitem.transform.Find("Rating").GetComponent<Rating>().scorevalue = score;
        menuitem.transform.Find("Order").GetComponent<Button>().onClick.AddListener(() => onClickOrder());
        menuitem.transform.Find("Share").GetComponent<Button>().onClick.AddListener(() => onClickShare());
        menuitem.transform.Find("ItemClick").GetComponent<Button>().onClick.AddListener(() => ViewMenuItem());
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)Content.transform).rect.height + menuHeight);

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

	public void onClickOrder(){
		//order
		wl("Order");
	}

	public void onClickShare(){
		//share
		wl("Share");
	}

    public void onClickAddMenuItem()
    {
        addMenuItem(null, "Please give name", "Table? Amount? Price? Option?", 0.4f);
    }

    private DishContent GetMenuItemContent()
    {
        return new DishContent();
    }

    public void ViewMenuItem()
    {
        //set content for MenuDetail
        DishContent content = GetMenuItemContent();
        menuinfo = GameObject.Find("MenuDetail");
        menuinfo.GetComponent<MenuDetailControl>().setContent(content);

        //Move MenuDetail
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
