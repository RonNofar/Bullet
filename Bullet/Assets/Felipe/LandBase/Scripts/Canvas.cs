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

    //BuyFeedBack
    public bool soldAnimation;
    public Text PlayerMoneyText;
    public Text ShoptotalPriceText;
    public Text PlayerMoneyLeftText;
    public GameObject BuyFxFingerPrint;

    public float totalPrice;
    public float ConfirmedPlayerMoneyCanvas;
    public float NotConfirmedPlayerMoneyCanvas;

    //public GameObject player;
    //private PlayerItems player;
    private Bullet.PlayerMaster player;


    public bool timeToGlowFrame;
    public GameObject FrameGlowOn;
    public GameObject FrameGlowOff;
    // Use this for initialization
    private void Awake()
    {
        player = Bullet.PlayerMaster.Instance;
    }

    public void Start() {
        soldAnimation = true;
        //buyFX
        ShoptotalPriceText.text = "$ " + totalPrice.ToString("F0");
        BuyFxFingerPrint.SetActive(false);
        //PlayerItemsScript = player.GetComponent<PlayerMaster>();
        ConfirmedPlayerMoneyCanvas = player.Money;
        NotConfirmedPlayerMoneyCanvas = ConfirmedPlayerMoneyCanvas;

        for (int i = 0; i < Item.TotalNumberOfItems; i++)
        {
            ShopItem[i].SetActive(true);
            itemsOnShop[i] = player.itemsUnlocked[i];
            ShopItem[i].GetComponent<PowerUp>().ThisItemID= itemsOnShop[i].GetID();
        }

        CloseCanvas = false;
        timeToGlowFrame = true;
        FrameGlowOn.SetActive(false);
        FrameGlowOff.SetActive(true);

    }
    // Update is called once per frame
    void Update() {
        //Check Money 
        totalPrice = ConfirmedPlayerMoneyCanvas - NotConfirmedPlayerMoneyCanvas;
        PlayerMoneyLeftText.text = "$ " + NotConfirmedPlayerMoneyCanvas.ToString("F0");
        if (totalPrice>0)
        {
            if (BuyFxFingerPrint.activeInHierarchy)
            {
                BuyFxFingerPrint.SetActive(false);
            }
            ShoptotalPriceText.text = "$ " + totalPrice.ToString("F0");
        }
        PlayerMoneyText.text = "$ " + ConfirmedPlayerMoneyCanvas.ToString("F0");
        //Make shure all resets defolt position on disactivating script
        if (CloseCanvas)
        {
            for (int i = 0; i < Item.TotalNumberOfItems; i++)
            {
                ShopItem[i].SetActive(false);
            }

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
        NotConfirmedPlayerMoneyCanvas = ConfirmedPlayerMoneyCanvas;
        player.Money = ConfirmedPlayerMoneyCanvas;
        CloseCanvas = true;
    }

    public void BuyFeedBack() {
        soldAnimation = false;
        ShoptotalPriceText.text = "";
        BuyFxFingerPrint.SetActive(true);
        StartCoroutine(OffFX(2f));
    }
    IEnumerator OffFX(float time)
    {
        if (!soldAnimation)
        {
            yield return new WaitForSeconds(time);
            if(!soldAnimation) {
                ShoptotalPriceText.text = "SOLD";
                soldAnimation = true;
            }
        }
    }
}
