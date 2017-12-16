using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginControl : MonoBehaviour {

    void wl(string s)
    {
        Debug.Log(s, gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClickLogin()
    {
        string owner = GameObject.Find("ScriptHandler").GetComponent<LoginWithIMEI>().Login();
        wl(owner);
        if (owner != "")
        {
            StartCoroutine(AccessGranted(owner));

        }
    }

    IEnumerator AccessGranted(string owner)
    {
        gameObject.transform.parent.GetComponent<LoginEffect>().startEffect(owner);
        yield return new WaitForSeconds(4);
        float fadeTime = GameObject.Find("Transition").GetComponent<Transition>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene("HomeScreen");
    }
}
