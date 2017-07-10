using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

    public Text PowerUpTextPrice;
    public int PowerUpPriceNumber;

    public GameObject[] powerLevelGlow;
    public bool timeToChangePowerLevel;
    public static int CurrentPowerLevel; 
    //true= add // false= subtract
    public bool powerLevelAddSubtract ;
    public int NotConfirmedPowerLevel;


    public int testvarCurentPower=5;
    public int testvarPowerUpPriceNumber=0;
    public int Lvl_0PowerUpPriceNumber=10;

    // Use this for initialization
    void Start()
    {
        timeToChangePowerLevel = false;
        NotConfirmedPowerLevel = 0;
        CurrentPowerLevel = testvarCurentPower;

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
        testvarCurentPower = CurrentPowerLevel;
        if (timeToChangePowerLevel)
        {
            timeToChangePowerLevel = false;
            if (powerLevelAddSubtract)
            {
                NotConfirmedPowerLevel ++;
                powerLevelGlow[(NotConfirmedPowerLevel + CurrentPowerLevel - 1)].SetActive(true);
                PowerUpPriceNumber = PowerUpPriceNumber * 2;
                PowerUpTextPrice.text = PowerUpPriceNumber.ToString();
            }
            else
            {
                NotConfirmedPowerLevel --;
                powerLevelGlow[(NotConfirmedPowerLevel + CurrentPowerLevel )].SetActive(false);
                PowerUpPriceNumber = PowerUpPriceNumber / 2;
                PowerUpTextPrice.text = PowerUpPriceNumber.ToString();

            }
        }
	}

    public void AddLevelPowerUp()
    {
        if (CurrentPowerLevel <10 && NotConfirmedPowerLevel < 10)
        {
            powerLevelAddSubtract = true;
            timeToChangePowerLevel = true;
        }

    }
    public void SubtractLevelPowerUp()
    {
        if (NotConfirmedPowerLevel > 0 && NotConfirmedPowerLevel > 0)
        {
            powerLevelAddSubtract = false;
            timeToChangePowerLevel = true;
        }

    }
}
