using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject Ship;
    public GameObject PlayerOBJ;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void animationStart()
    {
        PlayerOBJ.SetActive(false);

    }
    public void animationEnd()
    {
        PlayerOBJ.SetActive(true);
        Vector3 Newposition = new Vector3(0, 0, 0);
        Newposition = new Vector3(-30,4, 0);
        Newposition = Newposition - PlayerOBJ.transform.position;
        PlayerOBJ.transform.Translate(Newposition, Space.World);
        PlayerOBJ.SetActive(true);
    }
}