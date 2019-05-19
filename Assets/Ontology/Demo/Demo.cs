using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OntologyCSharpSDK;
using OntologyCSharpSDK.Interface;
using System;
using OntologyCSharpSDK.Common;
using OntologyCSharpSDK.Network;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class Demo : MonoBehaviour
{
    private static readonly string _node = "http://polaris1.ont.io:20334"; //Ontology TestNet

    static OntologySdk OntSDK = new OntologySdk(_node, ConnectionMethodFactory.ConnectionMethod.REST);
    static string TxHash = "c792613e705b89ea9c751a15d92368aef053293ba696f7fa96504f802af4ce7f";
    static string BlockHash = "fd55ac95e16706ac95bd5edb04709cf2396a4d795a4313a838031b1435243a68";
    static string privatekey = "2d3747c9b5eba66e3b4f5b1491aa08720503fffca167b2b405af6f07c8eb108b";
    static byte[] publickey = Helper.GetPublicKeyFromPrivateKey(Helper.HexString2Bytes(privatekey));
    static string from = Helper.GetAddressFromPublicKey(publickey);
    static string to = "APa7uMYqdqpFK2chwwmeE7SrQAWZukuGbX";
    static string payer = from;

    void Start()
    {
        // Console.WriteLine("random create private key: "+ Helper.Bytes2HexString(Helper.CreatePrivateKey()));

        Console.WriteLine("privatekey:" + privatekey);
        Console.WriteLine("pubkey:" + Helper.Bytes2HexString(publickey) + "\n" + "address: " + from + ",payer: " + payer);
        Console.WriteLine();
        //try { Console.WriteLine("Block Hex (int): " + OntSDK.Connection.getBlockHex(15)); } catch { };
        //try { Console.WriteLine("getSmartCodeEvent: " + OntSDK.Connection.getSmartCodeEvent(0)); } catch { };
        //Console.ReadKey();

        //QueryBlockchain();

        //transfer();
        //Console.ReadKey();
        //invokeSmartcontract();
         Debug.Log("\r\n\r\nPress any key..");
    }

    //Query blockchain using chosen connection method (RPC, REST or Websocket)
    public static void QueryBlockchain()
    {
        try { Console.WriteLine("Connecting to blockchain via: " + OntSDK.Connection.GetType()); } catch { };
        try { Console.WriteLine("Block Height: " + OntSDK.Connection.getBlockHeight()); } catch { };
        try { Console.WriteLine("ONT Balance: " + OntSDK.Connection.getAddressBalance(to)); } catch { };
        try { Console.WriteLine("Node Count: " + OntSDK.Connection.getNodeCount()); } catch { };
        try { Console.WriteLine("Block Height by Tx Hash: " + OntSDK.Connection.getBlockHeightByTxHash(TxHash)); } catch { };
        try { Console.WriteLine("Block Hex (int): " + OntSDK.Connection.getBlockHex(15)); } catch { };
        try { Console.WriteLine("Block Hex (hash): " + OntSDK.Connection.getBlockHex(BlockHash)); } catch { };
        try { Console.WriteLine("Block Json (int): " + OntSDK.Connection.getBlockJson(15)); } catch { };
        try { Console.WriteLine("Block Json (hash): " + OntSDK.Connection.getBlockJson(BlockHash)); } catch { };
        try { Console.WriteLine("Transaction Hex by Tx Hash: " + OntSDK.Connection.getRawTransactionHex(TxHash)); } catch { };
        try { Console.WriteLine("Transaction Json by Tx Hash: " + OntSDK.Connection.getRawTransactionJson(TxHash)); } catch { };
        try { Console.WriteLine("Contract: " + OntSDK.Connection.getContractJson("49f0908f08b3ebce1e71dc5083cb9a8a54cc4a24")); } catch { };
        try { Console.WriteLine("getSmartCodeEvent: " + OntSDK.Connection.getSmartCodeEvent(TxHash)); } catch { };
        try { Console.WriteLine("getSmartCodeEvent: " + OntSDK.Connection.getSmartCodeEvent(0)); } catch { };
    }

    // transfer fund to another address, signed by privatekey
    public static void transfer()
    {
        NetworkResponse result = OntSDK.transfer("ONG", from, to, 5, payer, 20000, 500, new List<byte[]>() { Helper.HexString2Bytes(privatekey) }, true);
        Console.WriteLine(result.JobjectResponse);
    }
    public static void invokeSmartcontract()
    {
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

        JObject function = new JObject { { "operation", "method name" }, { "args", jarray } };
        JObject invokeConfig = new JObject {
                { "contractHash", "16edbe366d1337eb510c2ff61099424c94aeef02" },
                { "functions", new JArray() { function } },
                { "payer", "AUr5QUfeBADq6BMY6Tp5yuMsUNGpsD7nLZ"},
                { "gasLimit", 20000 },
                { "gasPrice", 500 }
            };

        JObject invokeAction = new JObject {
                { "action", "invoke" },
                { "version", "v1.0.0" },
                { "id", "1234" },
                { "params", new JObject { { "invokeConfig", invokeConfig } } }
            };
        Console.WriteLine(invokeAction);
        Console.WriteLine();
        // Console.ReadKey();

        NetworkResponse result = OntSDK.invokeTransaction(invokeAction, new List<byte[]>() { Helper.HexString2Bytes(privatekey) }, true);
        Console.ReadKey();
        Console.WriteLine("result:{0}", result.JobjectResponse);

        Console.ReadKey();
    }

    static void ProgressUpdate(string progress)
    {
        Debug.Log(progress);
    }

}
