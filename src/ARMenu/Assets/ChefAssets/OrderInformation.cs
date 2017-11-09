using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInformation : MonoBehaviour {

    Camera cam;
    float currentcamposx;
    float nextcamposx;
    public float deltax = 4f;
    bool vieworder;
    bool vieworderlist;

    // Use this for initialization
    void Start () {
        cam = null;
        currentcamposx = -1;
        nextcamposx = -1;
	}
	
	// Update is called once per frame
	void Update () {
        if (cam != null)
        {
            currentcamposx = cam.transform.position.x;
        }
        if (vieworder)
        {
            if (currentcamposx < nextcamposx)
            {
                cam.transform.Translate(deltax, 0, 0);
            }
            else
            {
                cam.transform.Translate(nextcamposx - currentcamposx, 0, 0);
                vieworder = false;
            }
        }
        else
        if (vieworderlist)
        {
            if (currentcamposx > nextcamposx)
            {
                cam.transform.Translate(-deltax, 0, 0);
            }
            else
            {
                cam.transform.Translate(nextcamposx - currentcamposx, 0, 0);
                vieworder = false;
            }
        }
	}

    public void ViewOrder()
    {
        cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;
        currentcamposx = cam.transform.position.x;
        nextcamposx = currentcamposx + width;
        vieworder = true;
    }

    public void ViewOrderList()
    {
        cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;
        currentcamposx = cam.transform.position.x;
        nextcamposx = currentcamposx - width;
        vieworderlist = true;
    }
}
