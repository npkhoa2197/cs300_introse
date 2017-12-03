using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ChefOrderControl : MonoBehaviour {

	private List<GameObject> Orders;
    public GameObject orderPrefab;
	private List<float> moveDisplacement;
    private GameObject Content;
	public float vy;
    private float offset;
    private float orderHeight;
    public Text title;
    public List<orderItem> orderList;
    public orderItem itemOr;
    

	// Use this for initialization
	void Start () {
        orderList = new List<orderItem>();
        
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance.GetReference("Order").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted) {
              // Handle the error...
            }
            else if (task.IsCompleted) {
                DataSnapshot snapshot = task.Result; 
                foreach ( DataSnapshot item in snapshot.Children) {
                    
                    IDictionary dictOrder = (IDictionary) item.Value;
                    
                    itemOr = new orderItem (Convert.ToString(dictOrder["meal"]), Convert.ToString(dictOrder["additionalRequirements"]),
                        Convert.ToInt32(dictOrder["quantity"]), Convert.ToInt32(dictOrder["tableNumber"]), Convert.ToSingle(dictOrder["price"]), 
                        (bool) dictOrder["finished"], (bool) dictOrder["paid"]);
                    orderList.Add(itemOr);   
                }   
            }
        });
        
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
        
    }
	/*var ref = FirebaseDatabase.DefaultInstance.GetReference("Order");
        ref.ChildAdded += handleChildAdded;
        void handleChildAdded(object sender, ChildChangedEventArgs args) {
            if (args.DatabaseError != null) {
                Debug.LogError(args.DatabaseError.Message);
                return;
            }
            Debug.Log(args.Snapshot.Value);
        }    */    
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

    private void addOrder(Sprite image, List<orderItem> orderList)
    {
        foreach (Transform child in Content.transform) {
            Destroy(child.gameObject);
        }
        Orders = new List<GameObject>();
        moveDisplacement = new List<float>();
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, orderHeight * Orders.Count + 10);
        foreach (orderItem item in orderList) {


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
            if (image != null)
            order.transform.Find("Image").GetComponent<Image>().sprite = image;
            order.transform.Find("Dishname").GetComponent<Text>().text = item.meal;
            order.transform.Find("Info1").GetComponent<Text>().text = "Table "+ item.tableNumber.ToString() + " Amount: "+ item.quantity.ToString() + " Price: " +item.price.ToString()+"vnd";
            order.transform.Find("Info2").GetComponent<Text>().text = "Additional requirement: "+ item.additionalRequirements;
            order.transform.Find("CookDone").GetComponent<Button>().onClick.AddListener(() => onClickCooked());
            order.GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<OrderInfo>().ViewOrder());
            Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)Content.transform).rect.height + orderHeight);

            //add to orders list
            Orders.Add(order);
            moveDisplacement.Add(0);
        }
    }

    // Update is called once per frame
    void Update () {
		if (getNewOrder() != null){
			// add new order to list	
		}

        //wl(Orders[0].transform.localPosition.x.ToString());
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
        addOrder(null, orderList);
    }
    public class orderItem {
        public string meal, additionalRequirements;
        public int quantity, tableNumber;
        public float price;
        public bool finished, paid;
        public orderItem (string meal, string additionalRequirements, int quantity, int tableNumber, float price, bool finished, bool paid) {
            this.meal = meal;
            this.additionalRequirements = additionalRequirements;
            this.quantity = quantity;
            this.tableNumber = tableNumber;
            this.price = price;
            this.finished = finished;
            this.paid = paid;
        }
    }
}
