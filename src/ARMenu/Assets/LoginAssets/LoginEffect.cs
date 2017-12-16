using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginEffect : MonoBehaviour {

    private float posy;
    private float fadeo, fadea;
    public float fadev, t;
    private bool isStart;
    private bool isStart2;
    private string owner;

    private float fadeo2, fadev2;

    void wl(string s)
    {
        Debug.Log(s, gameObject);
    }

    // Use this for initialization
    void Start () {
        posy = gameObject.transform.localPosition.y;
        fadeo = 100;
        fadea = (-2*fadeo + 2*t*fadev)/(t*(t + 1));
        //wl(fadea.ToString());
        isStart = false;
        isStart2 = false;

        fadeo2 = 0;
        fadev2 = 2;
	}
	
    void setTransparent(float transvalue)
    {
        Color c = gameObject.transform.Find("RestaurantIcon").GetComponent<Image>().color;
        gameObject.transform.Find("RestaurantIcon").GetComponent<Image>().color = new Color(c.r, c.g, c.b, transvalue/100);
        c = gameObject.transform.Find("RestaurantName").GetComponent<Text>().color;
        gameObject.transform.Find("RestaurantName").GetComponent<Text>().color = new Color(c.r, c.g, c.b, transvalue / 100);
        c = gameObject.transform.Find("Caption").GetComponent<Text>().color;
        gameObject.transform.Find("Caption").GetComponent<Text>().color = new Color(c.r, c.g, c.b, transvalue / 100);
        c = gameObject.transform.Find("Login").GetComponent<Image>().color;
        gameObject.transform.Find("Login").GetComponent<Image>().color = new Color(c.r, c.g, c.b, transvalue / 100);
        c = gameObject.transform.Find("Login").Find("Text").GetComponent<Text>().color;
        gameObject.transform.Find("Login").Find("Text").GetComponent<Text>().color = new Color(c.r, c.g, c.b, transvalue / 100);
    }

	// Update is called once per frame
	void Update () {
        if (isStart)
        {
            //wl(fadeo.ToString());
            fadev -= fadea;
            fadeo = fadeo - fadev;
            if (fadev < 0.01f)
            {
                fadeo = 0;
                isStart = false;
                isStart2 = true;
            }
            setTransparent(fadeo);
            Vector3 curpos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(curpos.x, curpos.y + (fadeo/100)*60, 0);
        }
        if (isStart2)
        {
            fadeo2 += fadev2;
            if (fadeo2 > 100)
            {
                if (fadeo2 > 400)
                {
                    isStart2 = false;
                    startScene();
                }
            }
            GameObject.Find("Greeting").GetComponent<Text>().color = new Color(0xFF, 0xFF, 0xFF, Math.Min(fadeo2, 100) / 100);
        }
	}

    private void startScene()
    {
        wl("finish");
       // SceneManager.LoadScene("HomeScreen");
    }

    public void startEffect(string owner)
    {
        isStart = true;
        this.owner = owner;
        if (owner == "waiter")
        {
            GameObject.Find("Greeting").GetComponent<Text>().text = "Welcome, Waiter Make sure you clean up everything";
        }
        else
        if (owner == "chef") {
            GameObject.Find("Greeting").GetComponent<Text>().text = "Welcome, Chef What to cook today ?"; 
        }
        else
        {
            GameObject.Find("Greeting").GetComponent<Text>().text = "Welcome, Guests Enjoy your day";
        }
    }
}
