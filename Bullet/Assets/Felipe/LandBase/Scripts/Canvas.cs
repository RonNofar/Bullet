using UnityEngine;
using System.Collections;

public class Canvas : MonoBehaviour {
    public bool CloseCanvas;


    public GameObject ShopWindow;

    public bool timeToGlowFrame;
    public GameObject FrameGlowOn;
    public GameObject FrameGlowOff;
    // Use this for initialization
    void Start () {
        CloseCanvas = false;
        timeToGlowFrame = true;
        FrameGlowOn.SetActive(false);
        FrameGlowOff.SetActive(true);

    }
	// Update is called once per frame
	void Update () {
        //Make shure all resets defolt position on disactivating script
        if (CloseCanvas)
        {
            timeToGlowFrame = true;
            Cursor.visible = false;
            CloseCanvas = false;
            ShopWindow.SetActive(false);
        }
        if (timeToGlowFrame){
            StartCoroutine(OnOffFrame());
        }
	}
    //light Flick shop frame
    IEnumerator OnOffFrame(){
        timeToGlowFrame = false;
        yield return new WaitForSeconds(3f);
        if (FrameGlowOn.activeInHierarchy)
        {
            FrameGlowOn.SetActive(false);
            FrameGlowOff.SetActive(true);
        }
        else if(FrameGlowOff.activeInHierarchy)
        {
            FrameGlowOn.SetActive(true);
            FrameGlowOff.SetActive(false);
        }
        timeToGlowFrame = true;
    }
    public void CloseCanvasFunction()
    {
        CloseCanvas = true;
    }


}
