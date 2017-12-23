using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;


//this script reads the device IMEI
//this IMEI acts as an identifier for each table
public class LoginWithIMEI : MonoBehaviour {

	//code snippet adapted from 
	//https://answers.unity.com/questions/1276254/how-to-get-imei-on-android.html
	
    private void verify(string imei)
    {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://armenu-2220c.firebaseio.com/");
		LoginControl loginControl = GameObject.Find("Login").GetComponent<LoginControl>();
        //check imei then return owner
		FirebaseDatabase.DefaultInstance
      	.GetReference("imei/" + imei)
      	.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				loginControl.OnLoginResult(-2);
			}
			else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				loginControl.OnLoginResult((long) snapshot.Value);
			}
		});
    }

	public void Login () {
		try {
			string imei = SystemInfo.deviceUniqueIdentifier;

			if (Application.platform == RuntimePlatform.Android)
			{
				AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
				AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
				string TELEPHONY_SERVICE = contextClass.GetStatic<string>("TELEPHONY_SERVICE");
				AndroidJavaObject telephonyService = activity.Call<AndroidJavaObject>("getSystemService", TELEPHONY_SERVICE);
				bool noPermission = false;

				try
				{
					imei = telephonyService.Call<string>("getDeviceId");
				}
				catch (System.Exception e)
				{
					noPermission = true;
				}
			}

			Debug.Log("IMEI: " + imei);
			//replace this with real imei
            //return verify(imei);
			verify("123456789");
		}
		catch(System.Exception exc)
		{
			Debug.Log(exc.ToString());
		}
	}
}
