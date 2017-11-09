using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script is attached as component to the 3D models
//to input detail info of each meal
public class GetFoodInfo : MonoBehaviour {

	public string foodName;
	public float price;
	public string description;
	public string variant;

	public string getFoodName () {
		return foodName;
	}

	public float getPrice () {
		return price;
	}

	public string getDescription () {
		return description;
	}

	//variant means different version of a meal (small, large, w/o sauce, etc.)
	public string getVariant () {
		return variant;
	}

}
