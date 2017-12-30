using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;
using UnityEngine.UI;
public class OrderInfo : MonoBehaviour {

    private GameObject orderinfo;
    private float posx, nextx;
    private bool viewinfo;
    private bool viewlist;
    private float v;
    public float vx;
	// Use this for initialization
	void Start () {
        viewinfo = false;
        orderinfo = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (viewinfo || viewlist)
        {
            posx = orderinfo.transform.position.x;
            if ((viewinfo && (posx < nextx)) || (viewlist && (posx > nextx)))
            {
                orderinfo.transform.Translate(nextx - posx, 0, 0);
                viewinfo = false;
                viewlist = false;
            }
            else
            {
                orderinfo.transform.Translate(v, 0, 0);
            }
        }
	}

    public void ViewOrder(GameObject order, Order item)
    {
        orderinfo = GameObject.Find("OrderDetail");
        orderinfo.transform.Find("Title").Find("Text").GetComponent<Text>().text = item.meal;
        orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("Table").GetComponent<Text>().text = "Table "+ item.tableNumber.ToString();
        orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("PriceVal").GetComponent<Text>().text = "$"+(item.price/item.quantity).ToString(); 
        orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("NumberVal").GetComponent<Text>().text = item.quantity.ToString();
        orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("TotalValue").GetComponent<Text>().text = "$"+item.price.ToString();
        orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("Additioninfo").Find("Text").GetComponent<Text>().text = item.additionalRequirements;
        if (orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("Paid") != null)
        	orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("Paid").GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<OrderInfo>().paidAndViewOrderList(order));
        else
        	orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("CookDone").GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<OrderInfo>().cookedAndViewOrderList(order));
        orderinfo.transform.Find("Title").Find("Back").GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<OrderInfo>().ViewOrderList());
        
        posx = orderinfo.transform.position.x;
        nextx = Screen.width / 2;
        v = -vx;
        viewinfo = true;
    }

    public void ViewOrderList()
    {
        orderinfo = GameObject.Find("OrderDetail");
        posx = orderinfo.transform.position.x;
        nextx = Screen.width / 2 * 3;
        v = vx;
        viewlist = true;
    }
    public void paidAndViewOrderList(GameObject order) 
    {
    	orderinfo = GameObject.Find("OrderDetail");
        posx = orderinfo.transform.position.x;
        nextx = Screen.width / 2 * 3;
        v = vx;
        viewlist = true;
    	order.transform.Find("Paid").GetComponent<Button>().onClick.Invoke();
        orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("Paid").GetComponent<Button>().onClick.RemoveAllListeners();
    }
    public void cookedAndViewOrderList(GameObject order) 
    {
    	orderinfo = GameObject.Find("OrderDetail");
        posx = orderinfo.transform.position.x;
        nextx = Screen.width / 2 * 3;
        v = vx;
        viewlist = true;
    	order.transform.Find("CookDone").GetComponent<Button>().onClick.Invoke();
        orderinfo.transform.Find("ScrollView_5").Find("ScrollRect").Find("Content").Find("CookDone").GetComponent<Button>().onClick.RemoveAllListeners();
    }
}
