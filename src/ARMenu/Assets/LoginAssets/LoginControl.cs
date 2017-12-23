using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginControl : MonoBehaviour {

    void wl(long s)
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
        GameObject.Find("ScriptHandler").GetComponent<LoginWithIMEI>().Login();
    }

    public void OnLoginResult(long owner) {
        wl(owner);
        if (owner != -2) {
            StartCoroutine(AccessGranted(owner));
        }
    }

    IEnumerator AccessGranted(long owner)
    {
        string sceneToLoad = "";
        //get content provider to access app state
        GlobalContentProvider contentProvider = GlobalContentProvider.Instance;
        if (owner > 0) {
            contentProvider.tableNumber = owner;
            contentProvider.ratings = new Dictionary<string, string>();
            sceneToLoad = "HomeScreen";
        }
        else if (owner == 0) {
            sceneToLoad = "ChefScreen";
        }
        else if (owner == -1) {
            sceneToLoad = "WaiterScreen";
        }

        gameObject.transform.parent.GetComponent<LoginEffect>().startEffect(owner);
        yield return new WaitForSeconds(4);
        float fadeTime = GameObject.Find("Transition").GetComponent<Transition>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(sceneToLoad);
    }
}
