using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class invoke : MonoBehaviour, IPointerDownHandler
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
        Debug.Log("invoke press down");

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
        var jarray = new JArray();
        var arg1 = new JObject {
                { "name", "arg0-list" },
                { "value", new JArray { true, new JArray { true ,100}, 100, "Long:100000000000", "Address:AUr5QUfeBADq6BMY6Tp5yuMsUNGpsD7nLZ", "ByteArray:aabb", "String:hello" }}
            };
        var arg2 = new JObject {
                { "name", "arg1-str" },
                { "value", "hello" }
            };
        var arg3 = new JObject {
                { "name", "arg2-int" },
                { "value", 100 }
            };
        jarray.Add(arg1);
        jarray.Add(arg2);
        jarray.Add(arg3);

        JObject function = new JObject { { "operation", "methodname" }, { "args", jarray } };
        JObject invokeConfig = new JObject {
                { "contractHash", "16edbe366d1337eb510c2ff61099424c94aeef02" },
                { "functions", new JArray() { function } },
                { "payer", "AUr5QUfeBADq6BMY6Tp5yuMsUNGpsD7nLZ"},
                { "gasLimit", 20000 },
                { "gasPrice", 500 }
            };

        JObject code = new JObject {
                { "action", "invoke" },
                { "version", "v1.0.0" },
                { "id", "1234" },
                { "params", new JObject { { "invokeConfig", invokeConfig } } }
            };

        string encode = "";
        byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(code.ToString());
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
