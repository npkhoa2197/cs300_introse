using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using AssemblyCSharp;

public class OrderHistoryControl : MonoBehaviour {

	private List<GameObject> Orders;
    public GameObject orderPrefab;
	private List<float> moveDisplacement;
    private GameObject Content;
	public float vy;
    private float offset;
    private float orderHeight;
    public Text title;
    public List<OrderEntry> orderList;

    private Button backBtn;
	private GlobalContentProvider provider;
	private Text totalPrice;
	private GameObject nothingToShow;

	// Use this for initialization
	void Start () {
        orderList = new List<OrderEntry>();
        Orders = new List<GameObject>();
        moveDisplacement = new List<float>();
        Content = transform.Find("Background/ScrollView_1/ScrollRect/Content").gameObject;
		totalPrice = transform.Find("OrderTotal/MainTotalPrice").GetComponent<Text>();
		nothingToShow = transform.Find("Background/NothingText").gameObject;
		backBtn = transform.Find("Title/BackBtn").gameObject.GetComponent<Button>();
        backBtn.onClick.AddListener(OnBackClick);
        provider = GlobalContentProvider.Instance;
        gameObject.SetActive(false);
    }

    void OnBackClick() {
        gameObject.SetActive(false);
    }

	void OnEnable() {
		if (Content != null) {
			GetOrderEntry();
		}
	}

	void OnDisable() {
		if (Orders != null && Orders.Count > 0) {
			foreach(GameObject go in Orders) {
				Destroy(go);
			}
            Orders.Clear();
		}
	}

	void GetOrderEntry() {
		if (provider.orderList.Count == 0) {
			nothingToShow.SetActive(true);
		}
		else {
			nothingToShow.SetActive(false);
		}

		totalPrice.text = "$" + provider.totalPrice.ToString();

        offset = ((RectTransform)orderPrefab.transform).rect.height * 0.03f;
        orderHeight = ((RectTransform)orderPrefab.transform).rect.height * 0.65f + offset;
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, orderHeight * provider.orderList.Count + 10);
		
        foreach (OrderEntry entry in provider.orderList.Values)
        {
			addOrder(entry);
        }
	}

    private void addOrder(OrderEntry item)
    {
        //Intantiate object
        GameObject order = (GameObject)GameObject.Instantiate(orderPrefab);

        //set position
        order.transform.SetParent(Content.transform);
        order.transform.localScale = new Vector3(0.65f, 0.65f, 1);
        if (Orders.Count != 0)
        {
            //wl("set local position");
            order.transform.localPosition = new Vector3(Orders[Orders.Count - 1].transform.localPosition.x, Orders[Orders.Count - 1].transform.localPosition.y - orderHeight, 0);
        }
        else
        {
            order.transform.localPosition = new Vector3(401.015f, -orderHeight / 2, 0);
        }

        //set content
        order.transform.Find("Dishname").GetComponent<Text>().text = item.foodFullName;
        order.transform.Find("Price").GetComponent<Text>().text = "$" + item.price.ToString();
		order.transform.Find("TotalPrice").GetComponent<Text>().text = "$" + item.totalPrice.ToString();
        order.transform.Find("Quantity").GetComponent<Text>().text = item.quantity.ToString();
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)Content.transform).rect.height + orderHeight);

        //add to orders list
        Orders.Add(order);
        moveDisplacement.Add(0);      
    }

    // Update is called once per frame
    void Update () {
        //update new position for orders
        for (int i = 0; i < Orders.Count; i++){
			if (moveDisplacement[i] != 0){
                if (Math.Abs(moveDisplacement[i]) <= vy)
                {
                    Orders[i].transform.localPosition += new Vector3(0, moveDisplacement[i], 0);
                    moveDisplacement[i] = 0;
                }
                else
                {
                    Orders[i].transform.localPosition += new Vector3(0, vy, 0);
                    moveDisplacement[i] -= vy;
                }
			}
		}
	}
}