using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial : MonoBehaviour
{

    public bool istochingPlayer;
    // Use this for initialization
    void Start()
    {
        istochingPlayer = false;

    }

    // Update is called once per frame
    void Update()
    {
    }
    /*
    void OnCollisionEnter(Collider theCollision) // C#, type first, name in second
    {
        if (theCollision.gameObject.tag == "Player")

        {
            istochingPlayer = true;
            Debug.Log("pooOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOp");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            istochingPlayer = true;
            Debug.Log("pooOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOp");
        }
    }
    */
}