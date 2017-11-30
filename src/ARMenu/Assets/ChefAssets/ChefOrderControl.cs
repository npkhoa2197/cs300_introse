using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChefOrderControl : MonoBehaviour {

	private List<GameObject> Orders;
    public GameObject orderPrefab;
	private List<float> moveDisplacement;
    private GameObject Content;
	public float vy;
    private float offset;
    private float orderHeight;

    //Debug
    void wl(string s)
    {
        Debug.Log(s, gameObject);
    }

	// Use this for initialization
	void Start () {
        Orders = new List<GameObject>();
        moveDisplacement = new List<float>();
        
        Content = GameObject.Find("/Orderlist/Background/ScrollView_1/ScrollRect/Content");
        
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            GameObject Orderitem = Content.transform.GetChild(i).gameObject;
            Orders.Add(Orderitem);
            moveDisplacement.Add(0);
        }

        offset = ((RectTransform)orderPrefab.transform).rect.height * 0.03f;
        orderHeight = ((RectTransform)orderPrefab.transform).rect.height * 0.65f + offset;
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, orderHeight * Orders.Count + 10);
        //wl(Orders[0].transform.localScale.x.ToString());
    }
	
	private GameObject getNewOrder(){
		return null;
	}

    private bool removeFromDatabase(GameObject order)
    {
        //Remove from database, send notification to waiters

        //Destroy object, update scroll view
        Destroy(order);
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)Content.transform).rect.height - orderHeight);
        return true;
    }

    private void addOrder(Sprite image, string name, string info1, string info2)
    {
        //Intantiate object
        GameObject order = (GameObject)GameObject.Instantiate(orderPrefab);

        //set position
        order.transform.SetParent(Content.transform);
        order.transform.localScale = new Vector3(0.65f, 0.65f, 1);
        if (Orders.Count != 0)
        {
            wl("set local position");
            order.transform.localPosition = new Vector3(Orders[Orders.Count - 1].transform.localPosition.x, Orders[Orders.Count - 1].transform.localPosition.y - orderHeight, 0);
        }
        else
        {
            order.transform.localPosition = new Vector3(401.015f, -orderHeight / 2, 0);
        }

        //set content
        if (image != null)
        order.transform.Find("Image").GetComponent<Image>().sprite = image;
        order.transform.Find("Dishname").GetComponent<Text>().text = name;
        order.transform.Find("Info1").GetComponent<Text>().text = info1;
        order.transform.Find("Info2").GetComponent<Text>().text = info2;
        order.transform.Find("CookDone").GetComponent<Button>().onClick.AddListener(() => onClickCooked());
        order.GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<OrderInfo>().ViewOrder());
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)Content.transform).rect.height + orderHeight);

        //add to orders list
        Orders.Add(order);
        moveDisplacement.Add(0);
    }

    // Update is called once per frame
    void Update () {
		if (getNewOrder() != null){
			// add new order to list	
		}

        wl(Orders[0].transform.localPosition.x.ToString());
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

    public void onClickCooked()
    {
        GameObject button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        GameObject order = button.transform.parent.gameObject;

        //remove object in order list
        int pivot = -1;
        for (int i = 0; i < Orders.Count; i++)
        {
            if (Orders[i] == order)
            {
                Orders.RemoveAt(i);
                moveDisplacement.RemoveAt(i);
                pivot = i;
                break;
            }
        }

        //set displacement to update position for order list
        for (int i = pivot; i < Orders.Count; i++)
        {
            moveDisplacement[i] += orderHeight;
        }

        // remove from database
        removeFromDatabase(order);
    }

    public void onClickAddOrder()
    {
        addOrder(null, "Please give name", "Table? Amount? Price? Option?", "Additional information");
    }
}
