using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using AssemblyCSharp;
public class WaiterOrderControl : MonoBehaviour {

    private List<GameObject> Orders;
    public GameObject orderPrefab;
    private List<float> moveDisplacement;
    private GameObject Content;
    public float vy;
    private float offset;
    private float orderHeight;
    public Text title;
    public List<Order> orderList;
    public Order itemOr;

    private GameObject confirmationCanvas;

    // Use this for initialization
    void Start () {
        confirmationCanvas = GameObject.Find("ConfirmationCanvas");
        confirmationCanvas.SetActive(false);

        orderList = new List<Order>();
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseDatabase.DefaultInstance.GetReference("FinishedOrder").ChildAdded += handleChildAdded;
        
        Orders = new List<GameObject>();
        moveDisplacement = new List<float>();
        
        Content = GameObject.Find("/Orderlist/Background/ScrollView_1/ScrollRect/Content");
        
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            GameObject Order = Content.transform.GetChild(i).gameObject;
            Orders.Add(Order);
            moveDisplacement.Add(0);
        }
        offset = ((RectTransform)orderPrefab.transform).rect.height * 0.03f;
        orderHeight = ((RectTransform)orderPrefab.transform).rect.height * 0.65f + offset;
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, orderHeight * Orders.Count + 10);
    }
     
    
    void handleChildAdded(object sender, ChildChangedEventArgs args) {
        if (args.DatabaseError != null) {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        
        IDictionary dictOrder = (IDictionary) args.Snapshot.Value;   
        
        itemOr = new Order (
            Convert.ToString(args.Snapshot.Key), 
            Convert.ToString(dictOrder["additionalRequirements"]),
            (bool) dictOrder["finished"], 
            Convert.ToString(dictOrder["meal"]),  
            (bool) dictOrder["paid"],
            Convert.ToDouble(dictOrder["price"]), 
            Convert.ToInt64(dictOrder["quantity"]), 
            Convert.ToInt64(dictOrder["tableNumber"])
            );
        addOrder(null, itemOr);
          
    }       

    private bool removeFromDatabase(GameObject order, string key)
    {
        //Remove from database, send notification to waiters
        FirebaseDatabase.DefaultInstance.GetReference("FinishedOrder").Child(key).Child("paid").SetValueAsync(true);
        //Destroy object, update scroll view
        Destroy(order);
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ((RectTransform)Content.transform).rect.height - orderHeight);
        return true;
    }

    private void addOrder(Sprite image, Order item)
    {
        
        Content.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, orderHeight * Orders.Count + 10);
    
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
        order.transform.Find("Paid").GetComponent<Button>().onClick.AddListener(() => onClickPaid(order, item.key));
        order.GetComponent<Button>().onClick.AddListener(() => gameObject.GetComponent<OrderInfo>().ViewOrder(order, item));
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

    public void onClickPaid(GameObject order, string key) {
        confirmationCanvas.SetActive(true);
        confirmationCanvas.transform.Find("Confirmation/Image/Yes").GetComponent<Button>().onClick.AddListener(
            () => onConfirmYesClicked(order, key));
        confirmationCanvas.transform.Find("Confirmation/Image/No").GetComponent<Button>().onClick.AddListener(
            () => onConfirmNoClicked());
    }

    public void onConfirmYesClicked(GameObject order, string key) {
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
        removeFromDatabase(order, key);

        //hide the confirmation canvas after confirming
        confirmationCanvas.SetActive(false);

        confirmationCanvas.transform.Find("Confirmation/Image/Yes").GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void onConfirmNoClicked() {
        confirmationCanvas.SetActive(false);
    }
}
