using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.EventSystems;
using System;

public class login : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("login press down");

        /*
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");       
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"); 
        //jo.Call("startMyActivity");
        AndroidJavaObject joPackageManager = jo.Call<AndroidJavaObject>("getPackageManager");

        AndroidJavaObject joIntent = joPackageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.github.ont.cyanowallet");
        */

        /*

        if (null != joIntent)
        {
            jo.Call("startActivity", joIntent);
        }
        
        object number = 18616627871;
        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaClass Intent = new AndroidJavaClass("android.content.Intent");
        AndroidJavaClass Uri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", Intent.GetStatic<AndroidJavaObject>("ACTION_SENDTO"), Uri.CallStatic<AndroidJavaObject>("parse", "smsto:" + number.ToString()));
        intent.Call<AndroidJavaObject>("putExtra", "sms_body", "");
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("startActivity", intent);
       
        */
        //http://www.manew.com/blog-50742-37586.html
        JObject code = new JObject {
            { "action", "login"},
            {"version", "v1.0.0"},
            {"id", "10ba038e-48da-487b-96e8-8d3b99b6d18a"},
            {"params", new JObject {
                {"type", "account"},
                {"dappName", "dapp Name"},
                {"dappIcon", "dapp Icon"},
                {"message", "helloworld"},
                {"callback", "http://101.132.193.149:4027/blockchain/v1/common/test-onto-login"}
            }}
        };

        string encode = "";
        string uriEncode = System.Uri.EscapeUriString(code.ToString());
        byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(uriEncode);
        try
        {
            encode = Convert.ToBase64String(bytes);
        }
        catch
        {
            encode = code.ToString();
        }

        AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaClass Intent = new AndroidJavaClass("android.content.Intent");
        AndroidJavaClass Uri = new AndroidJavaClass("android.net.Uri");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.intent.action.VIEW", Uri.CallStatic<AndroidJavaObject>("parse", "ontprovider://ont.io?param=" + encode));

        //intent.Call<AndroidJavaObject>("addCategory", "android.intent.category.DEFAULT");
        //intent.Call<AndroidJavaObject>("setData", Uri.CallStatic<AndroidJavaObject>("parse", "ontprovider://ont.io?param=" + encode));
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("startActivity", intent);


    }
}
