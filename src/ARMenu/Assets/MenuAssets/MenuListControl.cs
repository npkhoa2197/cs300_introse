using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuListControl : MonoBehaviour {

	private List<GameObject> MenuItems;
    public GameObject menuPrefab;
	private List<float> moveDisplacement;
    private GameObject Content;
	public float vy;
    private float offset;
    private float menuHeight;

    //Debug
    void wl(string s)
    {
        Debug.Log(s, gameObject);
    }

	// Use this for initialization
	void Start () {
        MenuItems = new List<GameObject>();
        moveDisplacement = new List<float>();
        
        Content = GameObject.Find("/Menulist/Background/ScrollView_1/ScrollRect/Content");
        
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            GameObject Menuitem = Content.transform.GetChild(i).gameObject;
            MenuItems.Add(Menuitem);
            moveDisplacement.Add(0);
        }

        offset = ((RectTransform)menuPrefab.transform).rect.height * 0.03f;
        menuHeight = ((RectTransform)menuPrefab.transform).rect.height * 0.65f + offset;
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, menuHeight * MenuItems.Count + 10);
        //wl(MenuItems[0].transform.localScale.x.ToString());
    }

    private void addMenuItem(Sprite image, string name, string info1, string info2)
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
        menuitem.transform.Find("Order").GetComponent<Button>().onClick.AddListener(() => onClickOrder());
        menuitem.transform.Find("Share").GetComponent<Button>().onClick.AddListener(() => onClickShare());
      	menuitem.GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<MenuInfo>().ViewMenuItem());
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)Content.transform).rect.height + menuHeight);

        //add to MenuItems list
        MenuItems.Add(menuitem);
        moveDisplacement.Add(0);
    }

    // Update is called once per frame
    void Update () {

        wl(MenuItems[0].transform.localPosition.x.ToString());
        //update new position for MenuItems
        for (int i = 0; i < MenuItems.Count; i++){
			if (moveDisplacement[i] != 0){
                if (Math.Abs(moveDisplacement[i]) <= vy)
                {
                    MenuItems[i].transform.localPosition += new Vector3(0, moveDisplacement[i], 0);
                    moveDisplacement[i] = 0;
                }
                else
                {
                    MenuItems[i].transform.localPosition += new Vector3(0, vy, 0);
                    moveDisplacement[i] -= vy;
                }
			}
		}
	}

	public void onClickOrder(){
		//order
	}

	public void onClickShare(){
		//share
	}

    public void onClickAddMenuItem()
    {
        addMenuItem(null, "Please give name", "Table? Amount? Price? Option?", "Additional information");
    }
}
