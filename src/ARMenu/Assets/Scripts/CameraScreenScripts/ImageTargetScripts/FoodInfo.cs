using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInfo : MonoBehaviour {

	public string foodName;
	public float price;
	public string description;

	public string getFoodName () {
		return foodName;
	}

	public float getPrice () {
		return price;
	}

	public string getDescription () {
		return description;
	}
	
}
