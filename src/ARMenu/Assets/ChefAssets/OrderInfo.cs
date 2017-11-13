using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInfo : MonoBehaviour {

    private GameObject orderinfo;
    private float posx, nextx;
    private bool viewinfo;
    private bool viewlist;
    private float v;
    public float vx;
	// Use this for initialization
	void Start () {
        viewinfo = false;
        orderinfo = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (viewinfo || viewlist)
        {
            posx = orderinfo.transform.position.x;
            if ((viewinfo && (posx < nextx)) || (viewlist && (posx > nextx)))
            {
                orderinfo.transform.Translate(nextx - posx, 0, 0);
                viewinfo = false;
                viewlist = false;
            }
            else
            {
                orderinfo.transform.Translate(v, 0, 0);
            }
        }
	}

    public void ViewOrder()
    {
        orderinfo = GameObject.Find("OrderDetail");
        posx = orderinfo.transform.position.x;
        nextx = Screen.width / 2;
        v = -vx;
        viewinfo = true;
    }

    public void ViewOrderList()
    {
        orderinfo = GameObject.Find("OrderDetail");
        posx = orderinfo.transform.position.x;
        nextx = Screen.width / 2 * 3;
        v = vx;
        viewlist = true;
    }
}
