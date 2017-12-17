using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script reads the device IMEI
//this IMEI acts as an identifier for each table
public class LoginWithIMEI : MonoBehaviour {

	//code snippet adapted from 
	//https://answers.unity.com/questions/1276254/how-to-get-imei-on-android.html
	
    private string verify(string imei)
    {
        //check imei then return owner
        return "guest"; //waiter //guest
    }

	public string Login () {
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
            return verify(imei);
		}
		catch(System.Exception exc)
		{
			Debug.Log(exc.ToString());
            return "";
		}
	}
}
