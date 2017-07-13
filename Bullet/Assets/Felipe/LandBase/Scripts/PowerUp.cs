using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//Ps this GameObject script must be unactive on game start (because Script Canvas will initialize it)
public class PowerUp : MonoBehaviour {

    public int ThisItemID;

    public GameObject Shop;
    private Canvas ShopItemsScript;

    public Text PowerUpTextPrice;
    public float PowerUpPriceNumber;

    public GameObject[] powerLevelGlow;
    public int CurrentPowerLevel;
    public bool timeToChangePowerLevel;

    //true= add // false= subtract
    public bool powerLevelAddSubtract ;
    public int NotConfirmedPowerLevel;


    public int varCurentPower;
    public int Lvl_0PowerUpPriceNumber;

    // Use this for initialization
    public void Start()
    {
        ShopItemsScript = Shop.GetComponent<Canvas>();
        Lvl_0PowerUpPriceNumber = 100;
        timeToChangePowerLevel = false;
        NotConfirmedPowerLevel = 0;
        CurrentPowerLevel=ShopItemsScript.itemsOnShop[ThisItemID].GetLevel();
        //CurrentPowerLevel = testvarCurentPower;

        //PowerUpPriceNumber = testvarPowerUpPriceNumber;
        if (CurrentPowerLevel == 0)
        {
            PowerUpPriceNumber = Lvl_0PowerUpPriceNumber;
        }
        else{
            PowerUpPriceNumber = Lvl_0PowerUpPriceNumber;
            for (int x = 0; x < CurrentPowerLevel; x++)
            {
                PowerUpPriceNumber = PowerUpPriceNumber * 2;
            }
        }
            PowerUpTextPrice.text = PowerUpPriceNumber.ToString();
        for (int x = 0; x <= 10; x++)
        {
            if (x <= (CurrentPowerLevel - 1))
            {
                powerLevelGlow[x].SetActive(true);
            }else
            powerLevelGlow[x].SetActive(false);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        varCurentPower = CurrentPowerLevel;
        if (timeToChangePowerLevel)
        {
            timeToChangePowerLevel = false;
            if (powerLevelAddSubtract)
            {
                NotConfirmedPowerLevel ++;
                powerLevelGlow[(NotConfirmedPowerLevel + CurrentPowerLevel - 1)].SetActive(true);
                //ShopItemsScript.itemsOnShop[ThisItemID].GetMultiplier();
                PowerUpPriceNumber = PowerUpPriceNumber * ShopItemsScript.itemsOnShop[ThisItemID].GetMultiplier();
                PowerUpTextPrice.text = PowerUpPriceNumber.ToString("F0");
            }
            else
            {
                NotConfirmedPowerLevel --;
                powerLevelGlow[(NotConfirmedPowerLevel + CurrentPowerLevel )].SetActive(false);
                PowerUpPriceNumber = PowerUpPriceNumber / ShopItemsScript.itemsOnShop[ThisItemID].GetMultiplier();
                PowerUpTextPrice.text = PowerUpPriceNumber.ToString("F0");

            }
        }
	}
    void SavePoints()
    {   
        CurrentPowerLevel = CurrentPowerLevel + NotConfirmedPowerLevel;
        ShopItemsScript.itemsOnShop[ThisItemID].SetLevel(CurrentPowerLevel);

        ShopItemsScript.ConfirmedPlayerMoneyCanvas= ShopItemsScript.NotConfirmedPlayerMoneyCanvas;
        NotConfirmedPowerLevel = 0;
        //Add here save sistem for this power Up
    }
    public void AddLevelPowerUp()
    {
        if (CurrentPowerLevel <10 && NotConfirmedPowerLevel < 10 - CurrentPowerLevel && (ShopItemsScript.NotConfirmedPlayerMoneyCanvas- PowerUpPriceNumber>-1))
        {
            ShopItemsScript.NotConfirmedPlayerMoneyCanvas = ShopItemsScript.NotConfirmedPlayerMoneyCanvas - PowerUpPriceNumber;
            powerLevelAddSubtract = true;
            timeToChangePowerLevel = true;
        }

    }
    public void SubtractLevelPowerUp()
    {
        if (NotConfirmedPowerLevel > 0 && NotConfirmedPowerLevel > 0)
        {
            ShopItemsScript.NotConfirmedPlayerMoneyCanvas= ShopItemsScript.NotConfirmedPlayerMoneyCanvas + (PowerUpPriceNumber / ShopItemsScript.itemsOnShop[ThisItemID].GetMultiplier());

            powerLevelAddSubtract = false;
            timeToChangePowerLevel = true;
        }

    }
    void OnEnable()
    {
        Start();
    }
}
