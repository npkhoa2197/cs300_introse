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
        InitCustomerSession(12);
    }

    void Start() {
        //Load first game scene (probably main menu)
        //SceneManager.LoadScene("MenuScreen");
        // for (int i = 0; i < 3; ++i) {
        //     orderList.Add("Entry #" + i, new OrderEntry("Entry #" + i, 1, 16.9, 16.9));
        // }
        SceneManager.LoadScene("CameraScreen");
        //SceneManager.LoadScene("LoginScreenV2");
    }

    void OnDestroy() {
        if (manListeners != null) {
            manListeners.Clear();
        }
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
        currentFoodManager = null;
        manListeners = new List<FoodManagerListener>();
    }

    public bool SetCurrentFoodManager(FoodTargetManager man) {
        if (currentFoodManager == null) {
            currentFoodManager = man;
                
            foreach(FoodManagerListener listeners in manListeners) {
                listeners.OnNewFoodManager();
            }

            return true;
        }

        return false;
    }

    public bool RemoveCurrentFoodManager(FoodTargetManager man) {
        if (man == currentFoodManager) {
            currentFoodManager = null;
            
            foreach(FoodManagerListener listeners in manListeners) {
                listeners.OnFoodManagerLost();
            }

            return true;
        }

        return false;
    }

    public FoodTargetManager GetCurrentFoodManager() {
        return currentFoodManager;
    }

    public void AddManagerListener(FoodManagerListener listener) {
        manListeners.Add(listener);
    }

    public void RemoveManagerListener(FoodManagerListener listener) {
        manListeners.Remove(listener);
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
    // Store current FoodManager to use with Details, Review and Order scene
    private FoodTargetManager currentFoodManager;
    // Store selection circle
    public GameObject selectionCircle;
    // Listeners for FoodManager changes
    private List<FoodManagerListener> manListeners;
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

public interface FoodManagerListener {
    void OnNewFoodManager();
    void OnFoodManagerLost();
}