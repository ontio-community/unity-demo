using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OntologyCSharpSDK;
using OntologyCSharpSDK.Network;
using OntologyCSharpSDK.Interface;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {
    public GameObject bullet;
    public GameObject text;
    public float speed = 5;
    private static readonly string _node = "http://polaris1.ont.io:20334"; //Ontology TestNet

    static OntologySdk OntSDK = new OntologySdk(_node, ConnectionMethodFactory.ConnectionMethod.REST);
    // Use this for initialization
    void Start () {
        InvokeRepeating("Tick", 0, 20f);
    }
    void Tick()
    {
        try
        {
              long height = OntSDK.Connection.getBlockHeight();
               Debug.Log("wait 20s,Get Block Height: " + height);

               TextMesh tt = GameObject.Find("Text").GetComponent<TextMesh>();
               tt.text = "Ontology Block Height: " + height.ToString();

        }
        catch
        {

        };
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject b = GameObject.Instantiate(bullet,transform.position,transform.rotation);
            Rigidbody rgd = b.GetComponent<Rigidbody>();
            rgd.velocity = transform.forward * speed;

        }      

    }
}
