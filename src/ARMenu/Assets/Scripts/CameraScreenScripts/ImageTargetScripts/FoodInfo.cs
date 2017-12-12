using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodInfo : MonoBehaviour {

	public string foodName;
	public double price;
	public string description;

	public string getFoodName () {
		return foodName;
	}

	public double getPrice () {
		return price;
	}

	public string getDescription () {
		return description;
	}
	
}
