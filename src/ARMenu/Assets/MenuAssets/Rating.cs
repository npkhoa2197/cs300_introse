using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rating : MonoBehaviour {

    public float scorevalue = 0.3f;
	// Use this for initialization
	void Start () {
        Slider score = this.transform.Find("Score").GetComponent<Slider>();
        score.value = scorevalue;
    }
	
	// Update is called once per frame
	void Update () {
    }
    
}
