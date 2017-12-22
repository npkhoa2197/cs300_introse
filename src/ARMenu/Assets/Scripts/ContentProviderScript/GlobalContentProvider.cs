using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalContentProvider : MonoBehaviour {

    // Make global
    public static GlobalContentProvider Instance {
        get;
        set;
    }

    void Awake () {
        DontDestroyOnLoad (transform.gameObject);
        Instance = this;
    }

    void Start() {
        //Load first game scene (probably main menu)
        SceneManager.LoadScene("LoginScreenV2");
    }

    // Food and variants data
	public FoodObject[] foods;
    // Store ratings information of customer
    public Dictionary<string, string> ratings;
    // Store table number
    public long tableNumber;
}

[System.Serializable]
public class FoodObject {
    public string foodName;
    public double price;
    public string description;
    public Variant[] variants;
}

[System.Serializable]
public class Variant {
    public GameObject variantModel;
    public string variantName;
}