using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInfo : MonoBehaviour {

    private GameObject menuinfo;
    private float posx, nextx;
    private bool viewinfo;
    private bool viewlist;
    private float v;
    public float vx;

    //DEBUG
    void wl(string s)
    {
        Debug.Log(s, gameObject);
    }
	// Use this for initialization
	void Start () {
        viewinfo = false;
        menuinfo = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (viewinfo || viewlist)
        {
            posx = menuinfo.transform.position.x;
            if ((viewinfo && (posx < nextx)) || (viewlist && (posx > nextx)))
            {
                menuinfo.transform.Translate(nextx - posx, 0, 0);
                viewinfo = false;
                viewlist = false;
            }
            else
            {
                menuinfo.transform.Translate(v, 0, 0);
            }
        }
	}

	private bool updateMenuItemDetail(){
		wl("updateMenuItemDetail");
		return(true);
	}

    public void ViewMenuItem()
    {
    	if (updateMenuItemDetail()){
        	menuinfo = GameObject.Find("MenuDetail");
        	posx = menuinfo.transform.position.x;
        	nextx = Screen.width / 2;
        	v = -vx;
        	viewinfo = true;
    	}
    }

    public void ViewMenuList()
    {
        menuinfo = GameObject.Find("MenuDetail");
        posx = menuinfo.transform.position.x;
        nextx = Screen.width / 2 * 3;
        v = vx;
        viewlist = true;
    }
}
