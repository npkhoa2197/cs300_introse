using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Rating : MonoBehaviour {

    public float scorevalue = 0f;
	// Use this for initialization
	void Start () {
        Slider score = this.transform.Find("Score").GetComponent<Slider>();
        score.value = scorevalue;
    }

    public void setValue(float score)
    {
        scorevalue = score;
        this.transform.Find("Score").GetComponent<Slider>().value = score;
    }
    
}
