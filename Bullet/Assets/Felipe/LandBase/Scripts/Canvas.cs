using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Canvas : MonoBehaviour {
    public bool CloseCanvas;
    //All Shop Items OBJ
    public GameObject[] ShopItem= new GameObject[5];
    //Copy Of Player Items
    public Item[] itemsOnShop = new Item[5];

    //Shop Canvas OBJ
    public GameObject ShopWindow;

    public Text PlayerMoneyText;
    public int PlayerMoneyCanvas;

    public GameObject player;
    private PlayerItems PlayerItemsScript; 

    public bool timeToGlowFrame;
    public GameObject FrameGlowOn;
    public GameObject FrameGlowOff;
    // Use this for initialization
    void Start() {
        PlayerItemsScript = player.GetComponent<PlayerItems>();

        for (int i = 0; i < Item.TotalNumberOfItems; i++)
        {
            ShopItem[i].SetActive(true);
            itemsOnShop[i] = PlayerItemsScript.itemsUnlocked[i];
            ShopItem[i].GetComponent<PowerUp>().ThisItemID= itemsOnShop[i].GetID();
        }
        for (int i = 0; i < Item.TotalNumberOfItems; i++)
        {
            itemsOnShop[i].printVar();
        }

        CloseCanvas = false;
        timeToGlowFrame = true;
        FrameGlowOn.SetActive(false);
        FrameGlowOff.SetActive(true);

    }
    // Update is called once per frame
    void Update() {

        //Check Money 
        PlayerMoneyCanvas = PlayerItemsScript.playerMoneyNumber;
        PlayerMoneyText.text="$ "+PlayerMoneyCanvas.ToString();
        //Make shure all resets defolt position on disactivating script
        if (CloseCanvas)
        {
            timeToGlowFrame = true;
            Cursor.visible = false;
            CloseCanvas = false;
            ShopWindow.SetActive(false);
        } else
        if (timeToGlowFrame) {
            StartCoroutine(OnOffFrame());
        }
    }
    //light Flick shop frame
    IEnumerator OnOffFrame() {
        timeToGlowFrame = false;
        yield return new WaitForSeconds(3f);
        if (FrameGlowOn.activeInHierarchy)
        {
            FrameGlowOn.SetActive(false);
            FrameGlowOff.SetActive(true);
        }
        else if (FrameGlowOff.activeInHierarchy)
        {
            FrameGlowOn.SetActive(true);
            FrameGlowOff.SetActive(false);
        }
        timeToGlowFrame = true;
    }
    public void CloseCanvasFunction()
    {
        //ShopPowerUp1.GetComponent<PowerUp>().Start();
        CloseCanvas = true;
    }
/*
    void SaveAllPoints() {
        ShopPowerUp1.GetComponent<PowerUp>().Start();
    }
*/
}