using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffect : MonoBehaviour {

    private float posx;
    public float vx;
    private bool ismoveleft;
	// Use this for initialization
	void Start () {
        posx = gameObject.transform.localPosition.x;
        ismoveleft = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (ismoveleft)
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x - vx, 0, 0);
            if (gameObject.transform.localPosition.x < posx - 300) ismoveleft = false;
        }
        else
        {
            gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x + vx, 0, 0);
            if (gameObject.transform.localPosition.x > posx + 300) ismoveleft = true;
        }
	}
}
