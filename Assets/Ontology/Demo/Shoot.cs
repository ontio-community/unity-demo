using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OntologyCSharpSDK;
using OntologyCSharpSDK.Network;
using OntologyCSharpSDK.Core;
using OntologyCSharpSDK.Interface;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

//https://blog.csdn.net/qwe25878/article/details/85051911
public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    //public GameObject text;
    public float speed = 5;
    private static readonly string _node = "http://polaris1.ont.io:20334"; //Ontology TestNet

    static OntologySdk OntSDK = new OntologySdk(_node, ConnectionMethodFactory.ConnectionMethod.REST);
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("Tick", 0, 20f);
    }
    void Tick()
    {
        StartCoroutine(Get());

    }
    IEnumerator Get()
    {

        /* 
          long height = OntSDK.Connection.getBlockHeight();
           Debug.Log("wait 20s,Get Block Height: " + height);
        */

        /*   follow this demo: https://github.com/ontio-community/ontology-csharp-sdk
         *  Transaction tx = TransactionBuilder.MakeInvokeTransaction(action);
         *   OntSDK.sign(tx, privatekeys);
         *  var jsonObject = new JObject
         *   {
         *           ["Action"] = "sendrawtransaction",
         *           ["Version"] = "1.0.0",
         *           ["Data"] = tx.toHexString()
         *   };
         *   UnityWebRequest.Post(Constants.REST_sendRawTransactionPreExec, JsonConvert.SerializeObject(jsonObject));         
         *
         */

        UnityWebRequest webRequest = UnityWebRequest.Get(_node + Constants.REST_getBlockHeight);
        yield return webRequest.SendWebRequest();
        if (webRequest.isHttpError || webRequest.isNetworkError)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            Debug.Log("response:" + webRequest.downloadHandler.text);
        }
        TextMesh tt = GameObject.Find("Text").GetComponent<TextMesh>();

        tt.text = "Ontology Block Height: " + JObject.Parse(webRequest.downloadHandler.text)["Result"].ToString();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject b = GameObject.Instantiate(bullet, transform.position, transform.rotation);
            Rigidbody rgd = b.GetComponent<Rigidbody>();
            rgd.velocity = transform.forward * speed;

        }
    }
}
