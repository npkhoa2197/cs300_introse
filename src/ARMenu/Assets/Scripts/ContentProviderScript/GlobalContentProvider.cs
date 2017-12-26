using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class GlobalContentProvider : MonoBehaviour {

    // Make global
    public static GlobalContentProvider Instance {
        get;
        set;
    }

    //function to get meal name on db
    public static string GetMealKey(string name) {
        return name.ToLower().Replace(" ", "_");
    }

    void Awake () {
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
    }

    void Start() {
        //Load first game scene (probably main menu)
        InitCustomerSession(12);
        SceneManager.LoadScene("MenuScreen");
        //SceneManager.LoadScene("CameraScreen");
        //SceneManager.LoadScene("LoginScreenV2");
    }

    public void AddOrderEntry(Order order, double orignalPrice) {
        if (orderList.ContainsKey(order.meal)) {
            orderList[order.meal].AddItem(order.quantity, order.price);
        } 
        else {
            orderList.Add(order.meal, new OrderEntry(order.meal, order.quantity, order.price, orignalPrice));
        }

        this.totalPrice += order.price;
    }

    public void InitCustomerSession(long tableNumber) {
        ratings = new Dictionary<string, string>();
        orderList = new Dictionary<string, OrderEntry>();
        this.tableNumber = tableNumber;
        this.totalPrice = 0;
    }

    // Food and variants data
	public FoodObject[] foods;
    // Store ratings information of customer
    public Dictionary<string, string> ratings;
    // Store history of orders
    public Dictionary<string, OrderEntry> orderList;
    // Store total price of all orders
    public double totalPrice;
    // Store table number
    public long tableNumber;
}

public class OrderEntry {
    public string foodFullName;
    public long quantity;
    public double price;
    public double totalPrice;

    public OrderEntry(string foodFullName, long quantity, double price, double originalPrice) {
        this.foodFullName = foodFullName;
        this.quantity = quantity;
        this.price = originalPrice;
        this.totalPrice = price;
    }

    public void AddItem(long quantity, double price) {
        this.quantity += quantity;
        this.totalPrice += price;
    }
}

[System.Serializable]
public class FoodObject {
    public string foodName;
    public double price;
    public string description;
    public Sprite foodImage;
    public Variant[] variants;
}

[System.Serializable]
public class Variant {
    public GameObject variantModel;
    public string variantName;
}